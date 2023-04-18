using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class InvisibleInstrumentIndicator : MonoBehaviour
{
    [SerializeField] private CanvasGroup stingerIndicator;
    [SerializeField] private Transform stinger;
    [SerializeField] private CanvasGroup holderIndicator;
    [SerializeField] private Transform holder;
    [SerializeField] private Vector3 indicatorBarrier;

    public void DisableStinger() => stingerIndicator.DOFade(0, .5f);
    public void DisableHolder() => holderIndicator.DOFade(0, .5f);

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(indicatorBarrier.x, .1f, indicatorBarrier.z));
    }

    private void Update()
    {
        var directionToStinger = stinger.localPosition.normalized;
        stingerIndicator.transform.localPosition = CalculateBarrier(directionToStinger);
        
        var directionToHolder = holder.localPosition.normalized;
        holderIndicator.transform.localPosition = CalculateBarrier(directionToHolder);
    }

    private Vector3 CalculateBarrier(Vector3 direction)
    {
        var squareDirection = direction * indicatorBarrier.z;
        squareDirection.x *= indicatorBarrier.x / indicatorBarrier.z;
        return squareDirection / 2;
    }
}