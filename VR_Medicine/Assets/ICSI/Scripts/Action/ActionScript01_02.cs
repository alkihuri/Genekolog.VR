using System.Collections;
using System.Collections.Generic;
using Autohand;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActionScript01_02 : ActionScript
{
    [SerializeField] private Instrument[] grabbableInstruments;
    [SerializeField] private TrayTriggerController trayTrigger;
    private readonly List<Instrument> listInstrument = new();
    //private List

    [Button]
    private void FindInstruments()
    {
        grabbableInstruments = GetComponentsInChildren<Instrument>();
    }

    public override ActionScript Initialize()
    {
        TraySubscribeActions();
        return base.Initialize();
    }

    /*protected override void OnInteractableObjectEndAction(ActionInteractableObject obj)
    {
        listenedObjects.Remove(obj);

        if (listenedObjects.Count == 0)
        {
            //onActionEnd?.Invoke();
            OnStartSecondPart();
        }
    }*/

    private void TraySubscribeActions()
    {
        trayTrigger.OnSetInstrument += OnTraySetInstrument;
        foreach (var instrument in grabbableInstruments)
        {
            listInstrument.Add(instrument);
            //instrument.onStartAction?.Invoke();
        }
    }

    private void OnTraySetInstrument(Instrument instrument)
    {
        listInstrument.Remove(instrument);
        OnInteractableObjectEndAction(instrument.GetComponent<ActionInteractableObject>());
        /*if (listInstrument.Count == 0)
        {
            onActionPartEnd?.Invoke();
        }*/
    }
}