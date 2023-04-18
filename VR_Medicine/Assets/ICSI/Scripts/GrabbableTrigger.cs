using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;
using UnityEngine.Events;

public class GrabbableTrigger : MonoBehaviour
{
    [SerializeField] private Collider targetCollider;
    [SerializeField] private Grabbable grabbable;
    [SerializeField] private UnityEvent onTrigger;
    private Hand hand;

    private void Start()
    {
        grabbable.OnGrabEvent += OnGrab;
    }

    private void OnGrab(Hand hand, Grabbable grabbable)
    {
        this.hand = hand;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hand == null || other != targetCollider) return;
        
        onTrigger.Invoke();
        hand.Release();
        Destroy(targetCollider.gameObject);
    }
}