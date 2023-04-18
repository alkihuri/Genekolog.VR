using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class GlovesBox : ActionInteractableObject
{
    [SerializeField] private HandTriggerAreaEvents trigger;
    
    public void OnHandGrab(Hand hand)
    {
        if (hand.TryGetComponent(out HandModel handModel))
        {
            handModel.SetGlovesColor();
            hand.PlayHapticVibration(.1f);
            InvokeEndAction();
        }
    }

    private void Start()
    {
        trigger.HandGrabEvent += OnHandGrab;
    }
}