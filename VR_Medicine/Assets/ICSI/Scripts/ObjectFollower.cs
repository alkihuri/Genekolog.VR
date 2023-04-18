using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField] private Transform targetObject;

    private void Update()
    {
        transform.position = targetObject.position;
    }
}