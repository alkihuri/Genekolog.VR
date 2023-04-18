using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autohand;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class LaminarTrayPlace : ActionInteractableObject
{
    [SerializeField] private MeshFilter meshTray;
    [SerializeField] private Instrument[] instrumentModels;
    [SerializeField] private TrayPlaceInstrument[] instrumentsPlaced;
    /*[SerializeField] private MeshFilter meshCup;
    [SerializeField] private MeshFilter meshGummaBuffet;
    [SerializeField] private MeshFilter dispenser1_10;
    [SerializeField] private MeshFilter dispenser10_100;*/
    private Dictionary<TrayPlaceInstrument, Instrument> instruments = new();

    private void Start()
    {
        meshTray.gameObject.SetActive(false);
        foreach (var instrument in instrumentsPlaced)
        {
            instrument.SetDisable();
        }
    }

    public void OnPlaceTray()
    {
        foreach (var instrumentPlace in instrumentsPlaced)
        {
            var type = instrumentPlace.Type;
            var instrument = instrumentModels.Where(x => x.Type == type).ToArray()[0];
            //.HoldObject;
            instruments.Add(instrumentPlace, instrument);
            instrument.transform.SetParent(instrumentPlace.transform);
            instrument.transform.DOCircleMove(instrumentPlace.transform.position, 1);
            instrument.transform.DOLocalRotate(Vector3.zero, 1);
        }

        DOVirtual.DelayedCall(1, OnEndMoveInstruments);
    }

    public void CloseInstruments()
    {
        foreach (var instrument in instrumentModels)
        {
            instrument.GetComponent<ActionInteractableObject>().animator.SetClose();
        }
    }

    private void OnEndMoveInstruments()
    {
        foreach (var instrumentKeyPlace in instruments)
        {
            instrumentKeyPlace.Key.SetHoldObject(instrumentKeyPlace.Value.GetComponent<Grabbable>());
            instrumentKeyPlace.Value.GetComponent<ActionInteractableObject>().animator.SetOpen();
        }
        instruments.Clear();
        InvokeEndAction();
    }
}