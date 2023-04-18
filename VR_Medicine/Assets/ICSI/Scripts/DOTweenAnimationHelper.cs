using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class DOTweenAnimationHelper
{
    private static Tweener DOCircleMove(this Transform transform, Vector3 startPoint, Vector3 endPoint, float duration)
    {
        var path = GetWayToObjectEndPoint(startPoint, endPoint);
        return transform.DOPath(path, duration).SetEase(Ease.Linear);
    }
    
    public static Tweener DOCircleMove(this Transform transform, Vector3 endPoint, float duration)
    {
        return DOCircleMove(transform, transform.position, endPoint, duration);
    }
    

    private static Vector3[] GetWayToObjectEndPoint(Vector3 startPoint, Vector3 endPoint)
    {
        var defaultEndPoint = endPoint;
        endPoint += Vector3.up * (startPoint.y - endPoint.y);
        var countPoint = 9;
        var way = new List<Vector3>();
        var centerPoint = (startPoint + endPoint) / 2;
        var defaultDirection = (startPoint - centerPoint);
        var stepAngle = 180 / countPoint;

        var vectorRotate = Quaternion.Euler(Vector3.up * -90) * defaultDirection.normalized;
        var quaternion = Quaternion.Euler(vectorRotate * stepAngle);
        way.Add(defaultDirection + centerPoint);
        for (int i = 0; i < countPoint; i++)
        {
            var direction = quaternion * (way[way.Count - 1] - centerPoint);
            way.Add(centerPoint + direction);
        }

        way.Add(defaultEndPoint);
        return way.ToArray();
    }
}