using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using UnityEngine;
using UnityEngine.Events;

public class OlympusJoystickController : ActionInteractableObject
{
    [SerializeField] private MicroInstrumentsMoveController microInstrumentsMove;
    [SerializeField] private OlympusJoystick rightJoystick;
    [SerializeField] private OlympusJoystick leftJoystick;
    [SerializeField] private UnityEvent onGrabbEvent;
    [SerializeField] private UnityEvent onReleaseEvent;
    private int handGrabCounter;

    private void Start()
    {
        rightJoystick.Initialize().SubscribeActions(OnGrabb, OnRelease);
        leftJoystick.Initialize().SubscribeActions(OnGrabb, OnRelease);
    }

    private void Update()
    {
        if (handGrabCounter > 0)
        {
            microInstrumentsMove.MoveHolder(leftJoystick.GetValue());
            microInstrumentsMove.MoveStinger(rightJoystick.GetValue());
        }
    }

    private void OnGrabb()
    {
        if (handGrabCounter == 0) onGrabbEvent.Invoke();
        handGrabCounter++;
        
        if (handGrabCounter == 2) InvokeEndAction();
    }

    private void OnRelease()
    {
        handGrabCounter--;
        if (handGrabCounter == 0) onReleaseEvent.Invoke();
    }
}

[Serializable]
public class OlympusJoystick
{
    public Grabbable Grabbable;
    public XRHandControllerLink HandController;
    public MeshOutline Outline;
    private Action onGrab, onRelease;
    private bool isGrabb;

    public OlympusJoystick Initialize()
    {
        Grabbable.OnGrabEvent += (_, _) => OnGrabbable();
        Grabbable.OnReleaseEvent += (_, _) => OnRelease();
        Outline.enabled = true;
        return this;
    }

    public OlympusJoystick SubscribeActions(Action onGrab, Action onRelease)
    {
        this.onGrab = onGrab;
        this.onRelease = onRelease;

        return this;
    }

    private void OnGrabbable()
    {
        Outline.enabled = false;
        onGrab?.Invoke();
        isGrabb = true;
    }

    private void OnRelease()
    {
        onRelease?.Invoke();
        isGrabb = false;
    }

    public Vector2 GetValue()
    {
        if (!isGrabb) return Vector2.zero;
        return HandController.GetAxis2D(Common2DAxis.primaryAxis);
    }
}