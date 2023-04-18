using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupDrop : MonoBehaviour
{
    [SerializeField] private DropMeshType type;
    
}

public enum DropMeshType
{
    GBuffer, LongPVP, LongPVPFlushing  
}