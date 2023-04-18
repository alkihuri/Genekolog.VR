using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHandInteractable : MonoBehaviour
{
    [SerializeField] private bool isDebug;
    [SerializeField] private GameObject vrHands;
    [SerializeField] private GameObject simulatorHand;
    //[SerializeField] private ActionControllers actionControllers;

    private void Awake()
    {
        #if UNITY_EDITOR
        if (!isDebug) return;
        vrHands.gameObject.SetActive(false);
        simulatorHand.gameObject.SetActive(true);
        return;
        #endif
        
        //actionControllers.FindActions();
        vrHands.gameObject.SetActive(true);
        simulatorHand.gameObject.SetActive(false);
    }
}