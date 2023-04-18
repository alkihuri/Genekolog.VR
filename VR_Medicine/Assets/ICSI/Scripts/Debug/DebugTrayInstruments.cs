using System.Collections;
using System.Collections.Generic;
using Autohand;
using Sirenix.OdinInspector;
using UnityEngine;

public class DebugTrayInstruments : MonoBehaviour
{
    public TrayTriggerController tray;
    public Instrument[] Instruments;

    [Button]
    private void FindInstruments()
    {
        Instruments = GetComponentsInChildren<Instrument>();
    }

    [Button]
    public void SetInstrumentsToTray()
    {
        foreach (var instrument in Instruments)
        {
            tray.DebugSetInstruments(instrument);
            instrument.GetComponent<Grabbable>().onGrab.Invoke(null, null);
        }
    }
}