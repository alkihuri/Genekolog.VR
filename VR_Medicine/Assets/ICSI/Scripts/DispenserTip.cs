using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DispenserTip : MonoBehaviour
{
    [SerializeField] private DispenserTipType type;
    private bool isInteractable;
    public DispenserTipType Type => type;
    public bool IsInteractable => isInteractable; 

    public void SetInteractable() => isInteractable = true;
    public void UnsetInteractable() => isInteractable = false;

    private void OnDisable()
    {
        GetComponent<ActionInteractableObject>().InvokeEndAction();
    }
}

public enum DispenserTipType
{
    type_1_10, type_10_100 
}