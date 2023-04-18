using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class DispenserDropController
{
    [SerializeField] private int capacity;
    private int currentCapacity;
    public float GetPercent => 1f - (float)currentCapacity / capacity;

    public void Initialize()
    {
        currentCapacity = capacity;
    }

    public void FingerValveMove(int direction)
    {
        currentCapacity += direction;
        if (currentCapacity < 0) currentCapacity = 0;
        if (currentCapacity > capacity) currentCapacity = capacity;
    }
}