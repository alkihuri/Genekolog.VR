using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class SpermStingerController : MonoBehaviour
{
    [SerializeField] private Transform targetSperm;
    [SerializeField] private Transform stingerPoint;
    [SerializeField] private Transform stingerTargetMovePoint;
    [SerializeField] private float radiusVacuum;
    [SerializeField] private float radiusStopVacuum;
    [SerializeField] private float speedVacuum;
    [SerializeField] private UnityEvent onSpermInStinger;
    [SerializeField] private UnityEvent onSpermOutStinger;
    [SerializeField] private Transform egg;
    private float sqrRadiusVacuum;
    private float sqrRadiusStopVacuum;
    private bool isCompliteVacuum;
    private bool isOutOnStinger;
    private bool isStingerInEgg;

    public void SetStingerInEgg() => isStingerInEgg = true;
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(stingerPoint.position, radiusVacuum);
        Gizmos.DrawWireSphere(stingerPoint.position, radiusStopVacuum);
    }

    public void StartMoveToOutStinger()
    {
        if (isOutOnStinger || !isStingerInEgg) return;

        Debug.Log("Start move sperm to egg");
        targetSperm.position =
            Vector3.MoveTowards(targetSperm.position, stingerPoint.position, speedVacuum * Time.deltaTime);
        if ((targetSperm.position - stingerPoint.position).magnitude < .01f)
        {
            Debug.Log("sperm in a egg");
            targetSperm.SetParent(egg);
            onSpermOutStinger.Invoke();
            isOutOnStinger = true;
        }
        
    }

    private void Start()
    {
        sqrRadiusVacuum = radiusVacuum * radiusVacuum;
        sqrRadiusStopVacuum = radiusStopVacuum * radiusStopVacuum;
    }

    public void Vacuum()
    {
        if (isCompliteVacuum || !enabled) return;
        
        CheckToMoveToStinger();
    }

    private void CheckToMoveToStinger()
    {
        var sqrDistance = (targetSperm.position - stingerPoint.position).sqrMagnitude;
        if (sqrDistance < sqrRadiusVacuum)
            MoveSpermToStinger(sqrDistance);
    }

    private void MoveSpermToStinger(float sqrDistance)
    {
        targetSperm.position =
            Vector3.MoveTowards(targetSperm.position, stingerPoint.position, speedVacuum * Time.deltaTime);
        CheckToVacuum(sqrDistance);
    }

    private void CheckToVacuum(float sqrDistance)
    {
        if (sqrDistance < sqrRadiusStopVacuum)
            AnimationVacuum();
    }

    private void AnimationVacuum()
    {
        isCompliteVacuum = true;
        targetSperm.SetParent(stingerPoint.parent);
        var duration = (stingerPoint.position - targetSperm.position).magnitude;
        duration += (stingerPoint.position - stingerTargetMovePoint.position).magnitude;
        targetSperm.DOLocalPath(new[] { stingerPoint.localPosition, stingerTargetMovePoint.localPosition }, duration / speedVacuum).OnComplete(
            () =>
            {
                onSpermInStinger?.Invoke();
            });
    }
}