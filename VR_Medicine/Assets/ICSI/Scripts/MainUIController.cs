using System;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private float speedRotate;
    [SerializeField] private float angleNotRotate;
    [SerializeField] private Transform followObject;
    [SerializeField] private float distance;
    private Vector3 lastDirection;
    private float multiplySpeed;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(followObject.position,
            followObject.position + Quaternion.Euler(Vector3.up * angleNotRotate) * followObject.forward * 5);
        Gizmos.DrawLine(followObject.position,
            followObject.position + Quaternion.Euler(Vector3.up * -angleNotRotate) * followObject.forward * 5);
    }

    private void Start()
    {
        lastDirection = followObject.forward;
        transform.position = lastDirection * distance;
    }

    private void FixedUpdate()
    {
        var horizontalRotate = Quaternion.Euler(0, followObject.rotation.eulerAngles.y, 0) * Vector3.forward;

        transform.LookAt(transform.position + lastDirection);
        var angleRotate = SignedAngleBetween(horizontalRotate, lastDirection, transform.up);
        
        multiplySpeed = Mathf.Clamp01(multiplySpeed + (MathF.Abs(angleRotate) > angleNotRotate ? 2 : -1) * Time.deltaTime);

        lastDirection = Vector3.Lerp(lastDirection, horizontalRotate, multiplySpeed * speedRotate * Time.deltaTime);
        transform.position = lastDirection.normalized * distance;
    }

    private float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n)
    {
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));
        return angle * sign;
    }
}