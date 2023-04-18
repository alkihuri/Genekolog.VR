using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class FridgeModel : MonoBehaviour
{
    [SerializeField] private FridgeDoorData leftDoor;
    [SerializeField] private FridgeDoorData rightDoor;
    public UnityEvent OnCompletedOpenDoor;
    public UnityEvent OnCompletedCloseDoor;

    public void OpenAnimation()
    {
        leftDoor.DOOpenDoor();
        rightDoor.DOOpenDoor();
        DOVirtual.DelayedCall(1, () => OnCompletedOpenDoor.Invoke());
    }

    public void CloseAnimation()
    {
        leftDoor.DOCloseDoor();
        rightDoor.DOCloseDoor();
        DOVirtual.DelayedCall(1, () => OnCompletedCloseDoor.Invoke());
    }
    
    //[Button] private void SaveLeftOpen() => leftDoor.SaveOpen();
    //[Button] private void SaveLeftClose() => leftDoor.SaveClose();
    [Button] private void SetDoorOpen()
    {
        leftDoor.SetDoorOpen();
        rightDoor.SetDoorOpen();
    }
    [Button] private void SetDoorClose()
    {
        leftDoor.SetDoorClose();
        rightDoor.SetDoorClose();
    }
    
    //[Button] private void SaveRightOpen() => rightDoor.SaveOpen();
    //[Button] private void SaveRightClose() => rightDoor.SaveClose();
}
[Serializable]
public class FridgeDoorData
{
    public Transform Door;
    public TransformPosition CloseData;
    public TransformPosition OpenData;

    public void DOOpenDoor()
    {
        //Door.DOLocalMove(OpenData.LocalPosition, 1);
        Door.DOLocalRotate(OpenData.LocalRotation, 1);
    }

    public void DOCloseDoor()
    {
        //Door.DOLocalMove(CloseData.LocalPosition, 1);
        Door.DOLocalRotate(CloseData.LocalRotation, 1);
    }
    
    public void SetDoorOpen()
    {
        //Door.localPosition = OpenData.LocalPosition;
        Door.localRotation = Quaternion.Euler(OpenData.LocalRotation);
    }

    public void SetDoorClose()
    {
        //Door.localPosition = CloseData.LocalPosition;
        Door.localRotation = Quaternion.Euler(CloseData.LocalRotation);
    }

    public void SaveClose()
    {
        //CloseData.LocalPosition = Door.localPosition;
        CloseData.LocalRotation = Door.localRotation.eulerAngles;
    }

    public void SaveOpen()
    {
        //OpenData.LocalPosition = Door.localPosition;
        OpenData.LocalRotation = Door.localRotation.eulerAngles;
    }
}
[Serializable]
public class TransformPosition
{
    //public Vector3 LocalPosition;
    public Vector3 LocalRotation;
}