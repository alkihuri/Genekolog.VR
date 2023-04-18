using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sperm : MonoBehaviour
{
    [SerializeField] private Transform mesh;
    public delegate Vector3 GetTargetPoint();

    private SpermSettings settings;
    private Tween shake;
    private Tween action;
    private GetTargetPoint getTargetPoint;

    public void Initialize(SpermSettings settings, GetTargetPoint getTarget)
    {
        this.settings = settings;
        getTargetPoint = getTarget;
        OnEndAction();
    }

    public void SetNewTarget(GetTargetPoint getTarget)
    {
        getTargetPoint = getTarget;
    }

    public void StopMove()
    {
        shake.Kill();
        action.Complete();
    }

    private void OnEndAction()
    {
        shake?.Kill();
        mesh.localRotation = Quaternion.identity;
        
        if (Random.value < .5f)
            StartMove();
        else
            StartSleep();
    }
    
    private void StartMove()
    {
        var targetPoint = getTargetPoint();
        var duration = (targetPoint - transform.position).magnitude;
        transform.LookAt(targetPoint);
        shake = mesh.DOShakeRotation(3, Vector3.up * settings.AngleRotate).SetLoops(-1);
        action = transform.DOMove(targetPoint, duration / settings.Speed).OnComplete(OnEndAction);
    }

    private void StartSleep()
    {
        action = DOVirtual.DelayedCall(Random.Range(1.5f,2.5f), OnEndAction);
    }
}

[Serializable]
public class SpermSettings
{
    public float Speed;

    public float AngleRotate;
    //public float SpeedRotate;
}