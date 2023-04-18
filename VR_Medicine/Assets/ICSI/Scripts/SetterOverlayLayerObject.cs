using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetterOverlayLayerObject : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private string layer;
    
    IEnumerator Start()
    {
        yield return null;
        yield return null;

        var layerUI = LayerMask.NameToLayer(layer);
        foreach (var o in objects)
        {
            o.layer = layerUI;
        }
    }
}