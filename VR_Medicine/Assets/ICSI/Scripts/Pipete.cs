using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using UnityEngine;

public class Pipete : MonoBehaviour, IOculusControllerButtonDownListener
{
    [SerializeField] private CommonButton button;
    [SerializeField] private ActionInteractableObject interactableObject;
    private bool isTipInTrigger;

    public void SetInTrigger() => isTipInTrigger = true;
    public void UnSetInTrigger() => isTipInTrigger = false;
    
    public void OnClickPrimaryButtonDown(Hand hand, CommonButton button)
    {
        if (this.button != button) return;
        if (!isTipInTrigger) return;
            
        interactableObject.InvokeEndAction();
    }

    public CommonButton ButtonFollowing => button;
}