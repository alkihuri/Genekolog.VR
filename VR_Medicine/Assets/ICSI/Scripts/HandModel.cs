using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Autohand.Hand))]
public class HandModel : MonoBehaviour
{
    private readonly int KEY_COLOR_PROPERTIES = Shader.PropertyToID("_BaseColor"); 
    private readonly int KEY_BASE_TEXTURE_PROPERTIES = Shader.PropertyToID("_BaseMap"); 
    private readonly int KEY_BUMP_TEXTURE_PROPERTIES = Shader.PropertyToID("_BumpMap"); 
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Color glovesColor;

    public void SetGlovesColor()
    {
        meshRenderer.material.SetColor(KEY_COLOR_PROPERTIES, glovesColor);
        meshRenderer.material.SetTexture(KEY_BASE_TEXTURE_PROPERTIES, null);
        meshRenderer.material.SetTexture(KEY_BUMP_TEXTURE_PROPERTIES, null);
    }

    public void SetDefaultColor()
    {
        meshRenderer.material.SetColor(KEY_COLOR_PROPERTIES, Color.white);
    }
}