using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Autohand;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TrayPlacePoint : PlacePoint
{
    public TrayInstrumentsType Type;
    public TrayPlaceInstrument place;
    private PlacePointEvent onEnterInstrumentEvent;
    private PlacePointEvent onExitInstrumentEvent;
    private Dictionary<Grabbable, Coroutine> coroutines = new();

    [Button] private void Initialize()
    {
        if (place != null)
        {
            Type = place.Type;
            placedOffset = place.transform;
        }
    }

    protected override void Start()
    {
        base.Start();
        OnPlaceEvent += OnPlaceObject;
        OnRemoveEvent += OnRemoveObject;
        
        //onEnterInstrumentEvent += (_, _) => place.SetEnable();
        //onExitInstrumentEvent += (_, _) => place.SetDisable();
    }

    private void OnRemoveObject(PlacePoint point, Grabbable grabbable)
    {
        //grabbable.GetComponent<Instrument>().SetCollidersEnabled(true);
    }

    private void OnPlaceObject(PlacePoint point, Grabbable grabbable)
    {
        //place.SetDisable();

        coroutines.Add(grabbable, StartCoroutine(RepeatSetParent(point, grabbable)));
        grabbable.OnGrabEvent += OnGrabObject;

        //grabbable.GetComponent<Instrument>().SetCollidersEnabled(false);
    }

    private void OnGrabObject(Hand hand, Grabbable grabbable)
    {
        if (coroutines.ContainsKey(grabbable))
        {
            StopCoroutine(coroutines[grabbable]);
            coroutines.Remove(grabbable);
        }
        gameObject.GetComponent<Collider>().enabled = true;
    }

    private IEnumerator RepeatSetParent(PlacePoint point, Grabbable grabbable)
    {
        var timer = .5f;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            grabbable.transform.SetParent(point.transform);
            yield return null;
        }
        
        grabbable.OnGrabEvent -= OnGrabObject;
        gameObject.GetComponent<Collider>().enabled = false;
        coroutines.Remove(grabbable);
    }

    public override bool CanPlace(Grabbable placeObj)
    {
        if (!base.CanPlace(placeObj)) return false;
        
        if (placeObj.TryGetComponent(out Instrument instrument) && instrument.Type == Type)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var instrument = other.GetComponentInParent<Instrument>();
        if (instrument != null)
        {
            if (CanPlace(instrument.Grabbable))
            {
                onEnterInstrumentEvent.Invoke(this, instrument.Grabbable);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var instrument = other.GetComponentInParent<Instrument>();
        if (instrument != null)
        {
            onExitInstrumentEvent.Invoke(this, instrument.Grabbable);
        }
    }
}
