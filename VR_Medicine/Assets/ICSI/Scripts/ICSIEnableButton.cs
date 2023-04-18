using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ICSIEnableButton : ActionInteractableObject
{
    [SerializeField] private UnityEvent onTapButton;

    public void OnTapButton()
    {
        onTapButton.Invoke();
        InvokeEndAction();
    }
}