using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class OlympusPrimaryButtonListener : MonoBehaviour, IOculusControllerButtonDownListener, IOculusControllerButtonUpListener
{
    [SerializeField] private CommonButton button;
    [SerializeField] private UnityEvent[] onHold;
    private int counter;
    private Coroutine primaryHold;

    public void SetCounter(int index) => counter = index;

    public void OnClickPrimaryButtonUp(Hand hand, CommonButton button)
    {
        if (button != this.button) return;
        StopCoroutine(primaryHold);
    }

    public void OnClickPrimaryButtonDown(Hand hand, CommonButton button)
    {
        if (button != this.button) return;
        Debug.Log("Hold primary with index " + counter);
        primaryHold = StartCoroutine(VacuumSperm());
    }

    private IEnumerator VacuumSperm()
    {
        while (true)
        {
            yield return null;
            onHold[counter].Invoke();
        }
    }
}
