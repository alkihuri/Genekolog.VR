using System;
using Autohand;
using Autohand.Demo;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class Dispenser : MonoBehaviour, IOculusControllerButtonDownListener, IOculusControllerButtonUpListener
{
    private const float DURATION_PUSH_DROP_ANIMATION = .2f;
    [SerializeField] private Grabbable grabbable;
    [SerializeField] private DispenserDropController dropController;
    [SerializeField] private DispencerFingerValve fingerValve;
    [SerializeField] private GrabbablePoseAdvanced[] grabbablePoses;
    [SerializeField] private GameObject tip;
    
    private Hand currentHand;
    private bool isInitializeTip;
    private bool isDispenserTipInTrigger;
    private bool isButtonDown;

    public void SetInTrigger() => isDispenserTipInTrigger = true;
    public void UnsetInTrigger() => isDispenserTipInTrigger = false;
    
    public void SetTipToDispenser()
    {
        tip.gameObject.SetActive(true);
        isInitializeTip = true;
    }

    public void OnClickPrimaryButtonUp(Hand hand, CommonButton button)
    {
        if (button != CommonButton.primaryButton) return;

        isButtonDown = false;
    }
 
    public void OnClickPrimaryButtonDown(Hand hand, CommonButton button)
    {
        if (button != CommonButton.primaryButton) return;
        if (!isInitializeTip) return;
        if (!isDispenserTipInTrigger) return;

        isButtonDown = true;
        
        hand.PlayHapticVibration(.1f);
        GetComponent<ActionInteractableObject>().InvokeEndAction();
        
        dropController.FingerValveMove(-1);
        MoveValve();
    }

    private void Update()
    {
        if (!isButtonDown)
        {
            dropController.FingerValveMove(1);
            MoveValve();
        }
    }

    private void Start()
    {
        dropController.Initialize();
        grabbable.OnGrabEvent += OnGrabThis;
        grabbable.OnReleaseEvent += OnReleaseThis;
        enabled = false;
    }

    private void OnGrabThis(Hand hand, Grabbable grab)
    {
        currentHand = hand;
        var percent = dropController.GetPercent;
        UpdatePose(percent);
        enabled = true;
    }

    private void OnReleaseThis(Hand hand, Grabbable grab)
    {
        enabled = false;
    }
    
#if UNITY_EDITOR
    [Range(0, 1), SerializeField] private float valveValue;

    private void OnValidate()
    {
        fingerValve.OnValidate(valveValue);
    }

    /*private void OnDrawGizmosSelected() { fingerValve.DrawGizmos(); }*/
#endif
    
    private void MoveValve()
    {
        var percent = dropController.GetPercent;
        UpdatePose(percent);
        fingerValve.MoveValve(DURATION_PUSH_DROP_ANIMATION, percent);
    }

    private void UpdatePose(float percent)
    {
        var lerpPose = HandPoseData.LerpPose(grabbablePoses[0].GetHandPoseData(currentHand),
            grabbablePoses[1].GetHandPoseData(currentHand), percent);
        currentHand.UpdatePose(lerpPose, DURATION_PUSH_DROP_ANIMATION);
    }
}