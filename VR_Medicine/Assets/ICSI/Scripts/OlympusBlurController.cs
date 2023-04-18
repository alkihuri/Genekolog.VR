using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using UnityEngine;

public class OlympusBlurController : MonoBehaviour, IOculusControllerButtonDownListener
{
    [SerializeField] private int countButtonDown;
    [SerializeField] private MicroBlurController microBlurController;
    private int counterClick;

    public CommonButton ButtonFollowing => CommonButton.primaryButton;
    public void OnClickPrimaryButtonDown(Hand hand, CommonButton button)
    {
        if (button != CommonButton.primaryButton) return;
        
        counterClick++;
        microBlurController.SetZoomValue((float)counterClick/countButtonDown);
    }
}