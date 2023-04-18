using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MicroBlurController : ActionInteractableObject
{
    public UniversalRendererData RendererData;
    //public KawaseBlur kawaseBlur;
    [SerializeField] private BlurSettings startValue;
    [SerializeField] private BlurSettings endValue;
    [Range(0, 1), SerializeField] private float value;

    public void SetZoomValue(float t)
    {
        ChangeBlurSettings(BlurSettings.Lerp(startValue, endValue, t));
        if (t > .95f)
        {
            InvokeEndAction();
            StartCoroutine(EndZooming(t));
        }
    }

    private IEnumerator EndZooming(float startPoint)
    {
        while (startPoint < 1)
        {
            yield return null;
            startPoint += Time.deltaTime;
            ChangeBlurSettings(BlurSettings.Lerp(startValue, endValue, startPoint));
        }
        InvokeEndAction();
        gameObject.SetActive(false);
    }
    
    private void OnValidate()
    {
        ChangeBlurSettings(BlurSettings.Lerp(startValue, endValue, value));
    }

    private void ChangeBlurSettings(BlurSettings settings)
    {
        var rendererDataRendererFeature = (RendererData.rendererFeatures[0] as KawaseBlur);

        rendererDataRendererFeature.settings.blurPasses = settings.BlurPasses;
        rendererDataRendererFeature.settings.downsample = settings.DownSample;
        
        rendererDataRendererFeature.Create();
    }
}

[Serializable]
public class BlurSettings
{
    public int BlurPasses;
    public int DownSample;

    public BlurSettings(int blurPasses, int downSample)
    {
        BlurPasses = blurPasses;
        DownSample = downSample;
    }

    public static BlurSettings Lerp(BlurSettings start, BlurSettings end, float t)
    {
        return new BlurSettings( (int)Mathf.Round(Mathf.Lerp(start.BlurPasses, end.BlurPasses, t)),
            (int)Mathf.Round(Mathf.Lerp(start.DownSample, end.DownSample, t)));
    }
}