using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Autohand;
using Autohand.Demo;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OlympusAngleSetter : ActionInteractableObject
{
    [SerializeField] private OlympysValueDTO values;
    [SerializeField] private OlympusHand right;
    [SerializeField] private OlympusHand left;
    [SerializeField] private UnityEvent onGrabbEvent;
    [SerializeField] private UnityEvent onReleaseEvent;
    private int counter = 2;
    private readonly List<OlympusHand> handGrabbes = new();

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(right.Components.OlympusNeedleHandl.position,
            right.Components.OlympusNeedleHandl.position + Quaternion.Euler(Vector3.forward * values.MinAngleValue) * Vector3.up);
        Gizmos.DrawLine(left.Components.OlympusNeedleHandl.position,
            left.Components.OlympusNeedleHandl.position + Quaternion.Euler(Vector3.forward * -values.MinAngleValue) * Vector3.up);

        Gizmos.DrawLine(right.Components.OlympusNeedleHandl.position,
            right.Components.OlympusNeedleHandl.position + Quaternion.Euler(Vector3.forward * values.MaxAngleValue) * Vector3.up);
        Gizmos.DrawLine(left.Components.OlympusNeedleHandl.position,
            left.Components.OlympusNeedleHandl.position + Quaternion.Euler(Vector3.forward * -values.MaxAngleValue) * Vector3.up);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(right.Components.OlympusNeedleHandl.position,
            right.Components.OlympusNeedleHandl.position + Quaternion.Euler(Vector3.forward * values.TargetValueAngle) * Vector3.up);
        Gizmos.DrawLine(left.Components.OlympusNeedleHandl.position,
            left.Components.OlympusNeedleHandl.position + Quaternion.Euler(Vector3.forward * -values.TargetValueAngle) * Vector3.up);
    }

    private void Start()
    {
        right.Initialize(this, values)
            .SubscribeActions(OnCompleteRotateHand, OnGrabb, OnRelease);
        
        left.Initialize(this, values)
            .SubscribeActions(OnCompleteRotateHand, OnGrabb, OnRelease);
    }

    private void OnCompleteRotateHand()
    {
        counter--;
        if (counter == 0)
        {
            InvokeEndAction();
        }
    }

    private void OnGrabb(OlympusHand hand)
    {
        if (handGrabbes.Contains(hand)) return;
        
        if (handGrabbes.Count == 0) onGrabbEvent.Invoke();
        handGrabbes.Add(hand);
    }

    private void OnRelease(OlympusHand hand)
    {
        if (!handGrabbes.Contains(hand)) return;

        handGrabbes.Remove(hand);
        if (handGrabbes.Count == 0) onReleaseEvent.Invoke();
    }
}

[Serializable]
public class OlympysValueDTO
{
    public float MinAngleValue;
    public float TargetValueAngle;
    public float MaxAngleValue;
    public float RotateMultiply;
    public Vector3 axisRotate;
}

[Serializable]
public class RotatorHandComponentsDTO
{
    public XRHandControllerLink HandController;
    public Grabbable Grabbable;
    public Transform OlympusNeedleHandl;
}
[Serializable]
public class OlympusHand
{
    public RotatorHandComponentsDTO Components;
    [Space] public TMP_Text Angle;
    public MeshOutline MeshOutline;
    [SerializeField] private float AngleMultiply;
    
    private Coroutine GrabbableListener;
    private MonoBehaviour attachGO;
    private OlympysValueDTO olympysValue;
    private Action onCompleted;
    private Action<OlympusHand> onGrab, onRelease;
    private Tween textAnimation;

    private float angle;
    //private float lastHandlAngle;

    public OlympusHand Initialize(MonoBehaviour parent, OlympysValueDTO olympysValueDTO)//, Action onCompleted)
    {
        olympysValue = olympysValueDTO;

        Angle.DOFade(0, 0);
        Components.Grabbable.body.isKinematic = false;
        Components.Grabbable.OnGrabEvent += (_, _) => OnGrabbHand();
        Components.Grabbable.OnReleaseEvent += (_, _) => OnReleaseHand();
        attachGO = parent;
        
        var eulerAnglesZ = Components.OlympusNeedleHandl.localRotation.eulerAngles.z;
        eulerAnglesZ = eulerAnglesZ > 180 ? eulerAnglesZ - 360 : eulerAnglesZ;
        angle = Mathf.Clamp(AngleMultiply * eulerAnglesZ, olympysValue.MinAngleValue, olympysValue.MaxAngleValue);

        return this;
    }

    public OlympusHand SubscribeActions(Action onCompleted, Action<OlympusHand> onGrab, Action<OlympusHand> onRelease)
    {
        this.onCompleted = onCompleted;
        this.onGrab = onGrab;
        this.onRelease = onRelease;

        return this;
    }

    private void UpdateText(double angle)
    {
        Angle.text = $"{angle}°";
    }

    private void OnReleaseHand()
    {
        attachGO.StopCoroutine(GrabbableListener);
        onRelease?.Invoke(this);

        //if (textAnimation != null) textAnimation.Kill();
        textAnimation = DOVirtual.DelayedCall(Angle.color.a == 1 ? 2 : 0, () =>
        {
            textAnimation = Angle.DOFade(0, 0.5f);
        });
    }

    private void OnGrabbHand()
    {
        GrabbableListener = attachGO.StartCoroutine(ListenHand());
        onGrab?.Invoke(this);
        
        if (textAnimation != null) textAnimation.Kill();
        textAnimation = Angle.DOFade(1, 0.5f);
    }

    private IEnumerator ListenHand()
    {
        while (true)
        {
            yield return null;
            //var newAngleRotate = Components.PhysicsGadget.GetValue();
            angle += olympysValue.RotateMultiply * Components.HandController.GetAxis2D(Common2DAxis.primaryAxis).y;
                
            //lastHandlAngle = newAngleRotate;
            angle = Mathf.Clamp(angle, olympysValue.MinAngleValue, olympysValue.MaxAngleValue);
            Components.OlympusNeedleHandl.localRotation = Quaternion.Euler(olympysValue.axisRotate * (AngleMultiply * angle));
            
            UpdateText(Math.Round(Mathf.Abs(angle),1));

            if (Mathf.Abs(olympysValue.TargetValueAngle - angle) < 0.1f)
                break;
        }

        Angle.DOColor(Color.green, .5f);
        onCompleted?.Invoke();
        Components.Grabbable.enabled = false;
        Components.Grabbable.body.isKinematic = true;
        MeshOutline.enabled = false;
        OnReleaseHand();
    }
}