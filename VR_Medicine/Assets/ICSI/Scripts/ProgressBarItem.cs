using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressBarItem : MonoBehaviour
{
    [SerializeField] private Transform line;
    [SerializeField] private GameObject defaultCircle;
    [SerializeField] private GameObject currentCircle;
    [SerializeField] private GameObject completedCircle;
    [Space, SerializeField] private bool isLastItem;

    public bool IsLastItem => isLastItem;
    
    public void Initialize(string text)
    {
        foreach (var tmpText in GetComponentsInChildren<TMP_Text>(true))
        {
            tmpText.text = text;
        }   
    }

    [Button]
    public void SetDefault()
    {
        if (!isLastItem) line.localScale = Vector3.up + Vector3.forward;
        defaultCircle.SetActive(true);
        currentCircle.SetActive(false);
        completedCircle.SetActive(false);
    }

    [Button]
    public void SetCurrent()
    {
        if (!isLastItem) line.localScale = Vector3.up + Vector3.forward;
        
        SetDisable(defaultCircle);

        DOVirtual.DelayedCall(.25f, () => SetEnable(currentCircle));
    }

    [Button]
    public void SetCompleted()
    {
        SetDisable(currentCircle);

        DOVirtual.DelayedCall(.25f, () =>
        {
            SetEnable(completedCircle);
            if (!isLastItem) line.DOScale(1, .5f);
        });
    }

    private void Start()
    {
        if (isLastItem)
        {
            transform.SetSiblingIndex(transform.parent.childCount);
        }
    }

    private void SetDisable(GameObject go)
    {
        go.transform.DOScale(.9f, .25f).OnComplete(() => { go.SetActive(false); });
    }

    private void SetEnable(GameObject go)
    {
        go.transform.localScale = Vector3.one * 0.9f;
        go.SetActive(true);
        go.transform.DOScale(1.05f, .1f).OnComplete(() => { go.transform.DOScale(1f, .15f); });
    }
}