using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorTOObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    private void FixedUpdate()
    {
        transform.LookAt(target);
    }
}