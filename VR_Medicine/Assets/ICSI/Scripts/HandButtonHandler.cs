using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(XRHandControllerLink)), RequireComponent(typeof(Hand))]
public class HandButtonHandler : MonoBehaviour
{
    [SerializeField] private XRHandControllerLink handControllerLink;
    [SerializeField] private Hand hand;
    [SerializeField] private CommonButton button;
    public Action<Hand, CommonButton> OnButtonDown;
    private bool pressed;
    private bool isHandGrabbObject;
    private IOculusControllerButtonUpListener lastUpGrabbedListener;
    private IOculusControllerButtonDownListener lastDownGrabbedListener;
    private IOculusControllerButtonHoldListener lastHoldGrabbedListener;

    public CommonButton GetHandlerButton => button;
    
    private void Start()
    {
        hand.OnGrabbed += OnGrabbed;
        hand.OnReleased += OnReleased;
    }

    private void OnReleased(Hand hand, Grabbable grab)
    {
        isHandGrabbObject = false;
        lastUpGrabbedListener = null;
        lastDownGrabbedListener = null;
        lastHoldGrabbedListener = null;
    }

    private void OnGrabbed(Hand hand, Grabbable grab)
    {
        if (grab.gameObject.TryGetComponent(out IOculusControllerButtonListener listener))// && listener.ButtonFollowing == button)
        {
            isHandGrabbObject = true;
            lastUpGrabbedListener = listener as IOculusControllerButtonUpListener;
            lastDownGrabbedListener = listener as IOculusControllerButtonDownListener;
            lastHoldGrabbedListener = listener as IOculusControllerButtonHoldListener;
        }
    }

    [Button]
    private void ButtonDown()
    {
        OnButtonDown?.Invoke(hand, button);
        lastDownGrabbedListener?.OnClickPrimaryButtonDown(hand, button);
    }

    private void Update()
    {
        //if (!isHandGrabbObject) return;
        
        if (!pressed && handControllerLink.ButtonPressed(button))
        {
            pressed = true;
            OnButtonDown?.Invoke(hand, button);
            lastDownGrabbedListener?.OnClickPrimaryButtonDown(hand, button);
            return;
        }

        if (pressed && !handControllerLink.ButtonPressed(button))
        {
            pressed = false;
            lastUpGrabbedListener?.OnClickPrimaryButtonUp(hand, button);
            return;
        }

        if (pressed)
        {
            lastHoldGrabbedListener?.OnClickPrimaryButtonHold(button);
            return;
        }
    }
}

public interface IOculusControllerButtonListener
{
    //CommonButton ButtonFollowing { get; }
} 

public interface IOculusControllerButtonUpListener : IOculusControllerButtonListener
{
    void OnClickPrimaryButtonUp(Hand hand, CommonButton button);
}

public interface IOculusControllerButtonDownListener : IOculusControllerButtonListener
{
    void OnClickPrimaryButtonDown(Hand hand, CommonButton button);
}

public interface IOculusControllerButtonHoldListener : IOculusControllerButtonListener
{
    void OnClickPrimaryButtonHold(CommonButton button);
}