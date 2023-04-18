using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private float defaultAngle;
    [SerializeField] private float openAngle;
    [SerializeField] private Vector3 axis = Vector3.up;
    [Range(0,1),SerializeField] private float value;

    private void Start()
    {
        value = 0;
    }

    private void OnValidate()
    {
        door.localRotation = Quaternion.Euler(axis * (Mathf.Lerp(defaultAngle, openAngle, value)));
    }

    public void OpenDoor()
    {
        door.DOLocalRotate(axis * openAngle, 1f);
    }

    public void CloseDoor()
    {
        door.DOLocalRotate(axis * defaultAngle, 1f);
    }
}