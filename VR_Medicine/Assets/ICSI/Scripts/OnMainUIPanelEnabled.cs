using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMainUIPanelEnabled : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    private readonly Dictionary<GameObject, int> defaultLayers = new();

    public void SetDefaultLayer()
    {
        foreach (var obj in objects)
        {
            obj.layer = defaultLayers[obj];
        }
    }

    public void SetOverlayLayer()
    {
        var layerUI = LayerMask.NameToLayer("OverlayObjects");
        foreach (var obj in objects)
        {
            obj.layer = layerUI;
        }
    }
    
    private void Start()
    {
        foreach (var obj in objects)
        {
            defaultLayers.Add(obj, obj.layer);
        }
    }
}