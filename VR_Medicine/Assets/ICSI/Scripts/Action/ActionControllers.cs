using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class ActionControllers : MonoBehaviour
{
    [SerializeField] private ActionScript[] actions;
    private int counter;
    
    [Button]
    public void FindActions()
    {
        actions = GetComponentsInChildren<ActionScript>();
    }

    private IEnumerator Start()
    {
        counter = 0;
        yield return new WaitForSeconds(2.5f);
        StartAction();
    }

    private void StartAction()
    {
        actions[counter].Initialize()
            .OnComplete(OnActionEnded);
    }

    private void OnActionEnded()
    {
        counter++;
        StartAction();
    }
}