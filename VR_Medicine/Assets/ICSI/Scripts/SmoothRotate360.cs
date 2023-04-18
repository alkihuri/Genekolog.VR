using System;
using UnityEngine;

public class SmoothRotate360 : MonoBehaviour
{
    [SerializeField] private float timeToRotate;

    private void Update()
    {
        transform.Rotate(Vector3.up * (Time.deltaTime * (360 / timeToRotate)));
    }
}