using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EggMicroInstrument : MonoBehaviour
{
    [SerializeField] private Transform stringerTargetPoint;
    [SerializeField] private EggBoneSettings firstEggBone;
    [SerializeField] private EggBoneSettings secondEggBone;
    [Space, SerializeField] private Transform holderEggBone;
    [SerializeField] private Transform holderPoint;
    [Space, SerializeField] private Vector3 localDefaultRotation;
    [SerializeField] private float speedRotate;
    [SerializeField] private UnityEvent OnEggRotation;
    [SerializeField] private UnityEvent OnEggHolder;
    [SerializeField] private UnityEvent OnEndEggAnimationEvent;
    private bool isFreezeEgg;
    private bool isHolderEgg;
    private bool isStingerEnterInCenter;
    private Coroutine rotatorEgg;
    private int countEggLayer = 2;

    public void FreezeEgg()
    {
        isFreezeEgg = true;
        OnEggRotation.Invoke();
    }

    public void StartRotation()
    {
        if (isFreezeEgg) return;
        
        rotatorEgg = StartCoroutine(RotationToDefault());
    }

    public void StopRotation()
    {
        StopCoroutine(rotatorEgg);
    }

    private IEnumerator RotationToDefault()
    {
        if (!isFreezeEgg)
        {
            while (true)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(localDefaultRotation),
                    speedRotate * Time.deltaTime);
    
                var angleDistance = (transform.localRotation.eulerAngles - localDefaultRotation).magnitude % 360;
                if (angleDistance < 5)
                {
                    FreezeEgg();
                    StopRotation();
                    break;
                }

                yield return null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        
        var firstLocalPosition = transform.TransformPoint(Vector3.right * firstEggBone.LocalBarrier);
        Gizmos.DrawLine(firstLocalPosition + Vector3.forward * 0.05f, firstLocalPosition + Vector3.back * 0.05f);
        
        var secondLocalPosition = transform.TransformPoint(Vector3.right * secondEggBone.LocalBarrier);
        Gizmos.DrawLine(secondLocalPosition + Vector3.forward * 0.05f, secondLocalPosition + Vector3.back * 0.05f);
        
        Gizmos.color = Color.red;
        
        var firstLocalPositionCotton = transform.TransformPoint(Vector3.right * firstEggBone.LocalCotton);
        Gizmos.DrawLine(firstLocalPositionCotton + Vector3.forward * 0.05f, firstLocalPositionCotton + Vector3.back * 0.05f);
        
        var secondLocalPositionCotton = transform.TransformPoint(Vector3.right * secondEggBone.LocalCotton);
        Gizmos.DrawLine(secondLocalPositionCotton + Vector3.forward * 0.05f, secondLocalPositionCotton + Vector3.back * 0.05f);
    }

    private void Start()
    {
        firstEggBone.Initialize(OnEndEggAnimation);
        secondEggBone.Initialize(OnEndEggAnimation);
    }

    private void OnEndEggAnimation()
    {
        countEggLayer--;
        if (countEggLayer == 0)
        {
            OnEndEggAnimationEvent.Invoke();
        }
    }

    private void Update()
    {
        if (!isFreezeEgg) return;

        if (!isHolderEgg)
        {
            var localHolderPosition = transform.InverseTransformPoint(holderPoint.position);
            var distanceToHolder = (localHolderPosition - holderEggBone.localPosition).magnitude;
            if (distanceToHolder < 0.05f)
            {
                isHolderEgg = true;
                OnEggHolder.Invoke();
            }
            return;
        }
        
        var localStringerPosition = transform.InverseTransformPoint(stringerTargetPoint.position);
        
        if (!isStingerEnterInCenter)
        {
            if ((firstEggBone.Bone.localPosition - localStringerPosition).magnitude < 0.1f)
            {
                isStingerEnterInCenter = true;
                //OnStartEggMechanic.Invoke();
            }
            return;
        }
        

        firstEggBone.Update(localStringerPosition.x);
        secondEggBone.Update(localStringerPosition.x);
    }
}

[Serializable]
public class EggBoneSettings
{
    public Transform Bone;
    public float LocalBarrier;
    public float LocalCotton;
    public float TimeToCottonReturn;
    private Vector3 startLocalPosition;
    private bool isListenStringer;
    private bool isCompleted;
    private float offset;
    private Action onEndAnimation;

    public void Initialize(Action onEnd)
    {
        onEndAnimation = onEnd;
        startLocalPosition = Bone.localPosition;
        offset = LocalBarrier - startLocalPosition.y;
    }
    
    public void Update(float stringerPosition)
    {
        if (isCompleted) return;
        
        if (stringerPosition > LocalBarrier)  
        {
            Bone.localPosition = Vector3.up * (stringerPosition - offset);
            isListenStringer = true;

            if (stringerPosition > LocalCotton)
            {
                Bone.DOLocalMove(startLocalPosition, TimeToCottonReturn).OnComplete(() => onEndAnimation.Invoke());
                isCompleted = true;
            }
            
            return;
        }

        if (isListenStringer)
        {
            isListenStringer = false;
            Bone.DOLocalMove(startLocalPosition, 1f);
        }
    }
}