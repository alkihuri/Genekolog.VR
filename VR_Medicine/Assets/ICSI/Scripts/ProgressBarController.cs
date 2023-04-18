using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private ProgressItemData[] items;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private ContentSizeFitter[] contents;
    private bool isShow;
    private int counter;

    [Button]
    private void FindItems()
    {
        var findedItems = new List<ProgressBarItem>(GetComponentsInChildren<ProgressBarItem>());
        var lastItem = findedItems.First(x => x.IsLastItem);
        findedItems.Remove(lastItem);
        findedItems.Add(lastItem);
        var newItems = new ProgressItemData[findedItems.Count];

        for (var index = 0; index < findedItems.Count; index++)
        {
            findedItems[index].Initialize((index + 1).ToString());
            
            var progressItemData = items.FirstOrDefault(x => x.item == findedItems[index]);
            if (progressItemData != null)
                newItems[index] = new ProgressItemData(progressItemData);
            else 
                newItems[index] = new ProgressItemData(findedItems[index]);
        }

        items = newItems;
    }

    [Button]
    public void StartShowProgress()
    {
        if (isShow) return;

        isShow = true;
        counter = 0;
        
        InitializeItem(items[counter]);
    }

    [Button]
    public void StepCompleted()
    {
        if (!isShow) return;
        
        items[counter].item.SetCompleted();
        counter++;
        
        if (counter >= items.Length) return;

        DOVirtual.DelayedCall(0.5f, () =>
        {
            InitializeItem(items[counter]);
        });
    }

    private void InitializeItem(ProgressItemData data)
    {
        data.item.SetCurrent();
        text.text = data.text;
        RefreshContentSize();
    }
    
    private void RefreshContentSize()
    {
        IEnumerator Routine()
        {
            foreach (var contentSizeFitter in contents)
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            
            yield return null;
            
            foreach (var contentSizeFitter in contents)
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
        StartCoroutine(Routine());
    }
}

[Serializable]
public class ProgressItemData
{
    public ProgressBarItem item;
    public string text;

    public ProgressItemData(ProgressBarItem item)
    {
        this.item = item;
    }

    public ProgressItemData(ProgressItemData original)
    {
        item = original.item;
        text = original.text;
    }
}