using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class DispencerFingerValve
    {
        [SerializeField] private Transform fingerValve;
        [SerializeField] private Vector3 endValue;
        [SerializeField] private Vector3 startValue;

        public void MoveValve(float duration, float percent)
        {
            fingerValve.DOLocalMove(Vector3.Lerp(startValue, endValue, percent), duration);
        }
        
        public void OnValidate(float value)
        {
            fingerValve.localPosition = Vector3.Lerp(startValue,endValue, value);
        }

        /*public void DrawGizmos()
        {
            //var localTargetEndPoint = Vector3.forward * endValue;
            //var localStartPoint = Vector3.forward * startValue;
        
            Gizmos.DrawLine(fingerValve.parent.TransformPoint(localStartPoint - Vector3.right / 6),
                fingerValve.parent.TransformPoint(localStartPoint + Vector3.right / 6));
        
            Gizmos.DrawLine(fingerValve.parent.TransformPoint(localTargetEndPoint - Vector3.right / 6),
                fingerValve.parent.TransformPoint(localTargetEndPoint + Vector3.right / 6));
        }*/
    }