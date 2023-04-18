using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class UIPanelAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform background;
    [SerializeField] private Transform[] panelParts;
    [SerializeField] private float delayTODisable;
    [SerializeField] private UnityEvent onDisable;
    [SerializeField] private UnityEvent onEnable;

    [Button]
    public void DisablePanel()
    {
        DOVirtual.DelayedCall(delayTODisable, DisableAnimation);
    }
    
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        EnableAnimation();
    }

    private void DisableAnimation()
    {
        foreach (var part in panelParts)
        {
            part.DOScale(Vector3.zero, .2f).OnComplete(() =>
            {
                part.transform.localScale = Vector3.zero;
            });
        }

        DOVirtual.DelayedCall(.2f, () =>
        {
            background.DOScaleY(0, .25f).OnComplete(() =>
            {
                onDisable.Invoke();
                gameObject.SetActive(false);
            });
        });
    }

    private void EnableAnimation()
    {
        onEnable.Invoke();
        foreach (var part in panelParts)
        {
            part.transform.localScale = Vector3.zero;
        }
        
        background.localScale = Vector3.right + Vector3.forward;
        background.DOScaleY(1, .25f).OnComplete(() =>
        {
            foreach (var part in panelParts)
            {
                part.DOScale(Vector3.one, .2f);
            }
        });
    }
}