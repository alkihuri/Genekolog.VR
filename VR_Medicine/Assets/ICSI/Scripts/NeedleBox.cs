using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using UnityEngine;
using UnityEngine.Events;

public class NeedleBox : ActionInteractableObject, IOculusControllerButtonDownListener
{
    [SerializeField] private CommonButton button;
    [SerializeField] private UnityEvent onBoxOpen;
    private bool isOpen;
    
    public void OnClickPrimaryButtonDown(Hand hand, CommonButton button)
    {
        if (isOpen) return;
        
        isOpen = true;
        animator.SetOpen();
        onBoxOpen.Invoke();
    }
}