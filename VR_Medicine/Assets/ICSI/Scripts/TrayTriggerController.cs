using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autohand;
using Autohand.Demo;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TrayTriggerController : ActionInteractableObject, IOculusControllerButtonDownListener
{
    [SerializeField] private TrayPlaceInstrument[] instruments;
    public Action<Instrument> OnSetInstrument;
    public UnityEvent DebugTrayInstruments;
    private readonly Dictionary<Hand, TrayHandDTO> listenHand = new();
    private bool isListenHand;
    private TrayPlaceInstrument lastNearestToHand;

    public TrayPlaceInstrument[] InstrumentsPlaces => instruments;

    private void OnTriggerEnter(Collider other)
    {
        var hand = other.GetComponentInParent<Hand>();

        if (hand == null) return;

        if (listenHand.ContainsKey(hand))
        {
            listenHand[hand].Colliders.Add(other);
            return;
        }

        var isEmptyHand = hand.holdingObj == null;
        if (!isEmptyHand)
        {
            OnEnterInstrumentHand(hand);

            //OnEnterEmptyHand(hand);
            //else

            isListenHand = true;
            listenHand[hand].Colliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var hand = other.GetComponentInParent<Hand>();
        if (hand == null) return;

        if (!listenHand.ContainsKey(hand)) return;

        listenHand[hand].Colliders.Remove(other);
        if (listenHand[hand].Colliders.Count != 0) return;

        hand.OnTriggerGrab -= OnCurrentHandGrab;
        listenHand.Remove(hand);

        var isEmptyHand = hand.holdingObj == null;
        if (isEmptyHand)
            OnExitEmptyHand(hand);
        else
        {
            OnExitInstrumentHand(hand);
            hand.holdingObj.OnReleaseEvent -= OnCurrentHandRelease;
        }

        if (listenHand.Count == 0)
            StopListenHandPosition();
    }

    private void OnEnterEmptyHand(Hand hand)
    {
        listenHand.Add(hand, new TrayHandDTO(ListenedHandType.empty));
        hand.OnTriggerGrab += OnCurrentHandGrab;
    }

    private void OnExitEmptyHand(Hand hand)
    {
        listenHand.Remove(hand);
        lastNearestToHand = null;
        hand.OnTriggerGrab -= OnCurrentHandGrab;
    }

    private void OnEnterInstrumentHand(Hand hand)
    {
        var instrument = hand.holdingObj.GetComponent<Instrument>();
        if (instrument == null)
        {
            listenHand.Add(hand, new TrayHandDTO(ListenedHandType.ignore));
            return;
        }

        var holdInstrumentType = instrument.Type;
        listenHand.Add(hand, new TrayHandDTO(ListenedHandType.instrument, hand.holdingObj));
        //hand.OnTriggerRelease += OnCurrentHandRelease;
        hand.holdingObj.OnReleaseEvent += OnCurrentHandRelease;

        instruments.First(x => x.Type == holdInstrumentType).SetEnable();
    }

    private void OnExitInstrumentHand(Hand hand)
    {
        listenHand.Remove(hand);
        //hand.OnTriggerRelease -= OnCurrentHandRelease;

        foreach (var instrumentPlace in instruments)
            instrumentPlace.SetDisable();
    }

    private void Update()
    {
        if (!isListenHand) return;

        for (var index = 0; index < listenHand.Count; index++) // (var handDto in listenHand)
        {
            var handDto = listenHand.ElementAt(index);
            switch (handDto.Value.Type)
            {
                case ListenedHandType.instrument:

                    if (handDto.Key.holdingObj == null)
                    {
                        handDto.Value.HoldObj.OnReleaseEvent -= OnCurrentHandRelease;
                        handDto.Value.HoldObj = null;
                        var colliders = handDto.Value.Colliders;
                        OnExitInstrumentHand(handDto.Key);
                        //OnEnterEmptyHand(handDto.Key);
                        //listenHand[handDto.Key].Colliders.AddRange(colliders);
                    }

                    continue;
                case ListenedHandType.empty:
                    FindNearestInstrumentPoint(handDto.Key.fingers[1].transform);

                    if (handDto.Key.holdingObj != null)
                    {
                        var colliders = handDto.Value.Colliders;
                        OnExitEmptyHand(handDto.Key);
                        OnEnterInstrumentHand(handDto.Key);
                        listenHand[handDto.Key].Colliders.AddRange(colliders);
                    }

                    break;
            }
        }
    }

    private void FindNearestInstrumentPoint(Transform finger)
    {
        var distance = float.MaxValue;
        TrayPlaceInstrument nearestPoint = null;

        foreach (var placePoint in instruments)
        {
            if (!placePoint.IsHoldObject) continue;

            var newDistance = (placePoint.HoldObject.transform.position - finger.position).sqrMagnitude;

            if (!(newDistance < distance)) continue;

            distance = newDistance;
            nearestPoint = placePoint;
        }

        if (nearestPoint == null || lastNearestToHand == nearestPoint) return;

        lastNearestToHand?.SetDisable();
        lastNearestToHand = nearestPoint;
        lastNearestToHand.SetEnable();
    }

    private void StopListenHandPosition()
    {
        listenHand.Clear();
        isListenHand = false;

        foreach (var instrumentPlace in instruments)
            instrumentPlace.SetDisable();
    }

    private void OnCurrentHandGrab(Hand hand, Grabbable grab)
    {
        if (lastNearestToHand == null || !lastNearestToHand.IsHoldObject) return;

        lastNearestToHand.HoldObject.GetComponent<Instrument>().SetCollidersEnabled(true);
        lastNearestToHand.SetDisable();
        hand.TryGrab(lastNearestToHand.HoldObject);
        lastNearestToHand.RemoveHoldObject();
    }

    private void OnCurrentHandRelease(Hand hand, Grabbable grab)
    {
        var holdInstrumentType = hand.holdingObj.GetComponent<Instrument>();
        SetInstruments(holdInstrumentType, hand.holdingObj);
    }

    public void DebugSetInstruments(Instrument instrument)
    {
        var grabbable = instrument.GetComponent<Grabbable>();
        SetInstruments(instrument, grabbable);
    }

    private void SetInstruments(Instrument instrument, Grabbable grabbable)
    {
        grabbable.enabled = false;
        instrument.SetCollidersEnabled(false);
        OnSetInstrument?.Invoke(instrument);
        var place = instruments.First(x => x.Type == instrument.Type);
        place.SetDisable();

        place.SetHoldObject(grabbable);
    }

    private void Start()
    {
        foreach (var instrument in instruments) instrument.SetDisable();
    }

    [Button]
    private void FindAllInstrumentsPlaces()
    {
        instruments = GetComponentsInChildren<TrayPlaceInstrument>();
    }

    public CommonButton ButtonFollowing => CommonButton.primaryButton;

    public void OnClickPrimaryButtonDown(Hand hand, CommonButton button)
    {
        DebugTrayInstruments.Invoke();
    }
}

public class TrayHandDTO
{
    public List<Collider> Colliders = new();
    public ListenedHandType Type;
    public Grabbable HoldObj;

    public TrayHandDTO(ListenedHandType type)
    {
        Type = type;
    }

    public TrayHandDTO(ListenedHandType type, Grabbable holdObj)
    {
        Type = type;
        HoldObj = holdObj;
    }

    public TrayHandDTO(ListenedHandType type, List<Collider> colliders)
    {
        Type = type;
        Colliders.AddRange(colliders);
    }
}

public enum ListenedHandType
{
    ignore,
    empty,
    instrument
}