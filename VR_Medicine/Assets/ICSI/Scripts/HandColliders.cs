using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HandColliders : MonoBehaviour
{
    [SerializeField] private Collider[] collider;

    public Collider[] Colliders => collider;
    
    [Button]
    private void FindColliders()
    {
        collider = GetComponentsInChildren<Collider>();
    }
}