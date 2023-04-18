using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPointerColorController : MonoBehaviour
{
    [SerializeField] private PointerUIData pointer;
    [SerializeField] private Material selectMaterial;
    [SerializeField] private Material deselectMaterial;

    public void StartSelect()
    {
        pointer.SetMaterial(selectMaterial);
    }

    public void StopSelect()
    {
        pointer.SetMaterial(deselectMaterial);
    }
}

[Serializable]
public class PointerUIData
{
    private const string COLOR_MATERIAL = "_Color";
    public LineRenderer LineRenderer;
    public Image Pointer;

    public void SetMaterial(Material material)
    {
        LineRenderer.material = material;
        Pointer.color = material.GetColor(COLOR_MATERIAL);
    }
}