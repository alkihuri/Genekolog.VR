using System.Collections;
using System.Collections.Generic;
using Autohand;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TrayPlaceInstrument : TrayInstrument
{
    [SerializeField] private Renderer[] meshes;
    [SerializeField] private UnityEvent onSetInstrument;
    private bool isHoldObject;
    private Grabbable holdObject;

    public bool IsHoldObject => isHoldObject;
    public Grabbable HoldObject => holdObject;
    
    public void SetHoldObject(Grabbable grabbable)
    {
        isHoldObject = true;
        holdObject = grabbable;
        holdObject.transform.SetParent(transform);
        holdObject.transform.DOLocalMove(Vector3.zero, .2f);
        holdObject.transform.DOLocalRotate(Vector3.zero, .2f);
        holdObject.body.isKinematic = true;
        
        onSetInstrument.Invoke();
    }

    public void RemoveHoldObject()
    {
        isHoldObject = false;
        holdObject.body.isKinematic = false;
        //holdObject.transform.SetParent(null);
        holdObject = null;
    }
    
    public void SetEnable()
    {
        foreach (var mesh in meshes)
        {
            mesh.enabled = true;
        }
    }
    public void SetDisable() 
    {
        foreach (var mesh in meshes)
        {
            mesh.enabled = false;
        }
    }
    
    [Button]
    private void FindMeshes()
    {
        meshes = GetComponentsInChildren<Renderer>();
    }
}

public enum TrayInstrumentsType
{
    Cup, Pipette, PVP, Dispenser1_10, DispTip1_10, Dispenser10_100, DispTip10_100, Gumma, MineralOil
}