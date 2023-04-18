using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MicroInstrumentsPlace : MonoBehaviour
{
    [SerializeField] private MicroInstrumentsMoveController moveController;
    [SerializeField] private float radiusToSet;
    [SerializeField] private InstrumentPlace stinger;
    [SerializeField] private InstrumentPlace holder;
    [SerializeField] private UnityEvent onSetPlace;
    private int count = 2;

    private void Start()
    {
        var sqrRadiusToSet = radiusToSet * radiusToSet;
        
        stinger.Initialize(sqrRadiusToSet, () =>
        {
            moveController.FreezeStinger();
            OnSetToPlace();
        });
        
        holder.Initialize(sqrRadiusToSet, () =>
        {
            moveController.FreezeHolder();
            OnSetToPlace();
        });
    }

    private void OnSetToPlace()
    {
        count--;
        if (count == 0)
        {
            onSetPlace.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        stinger.Draw(radiusToSet);
        holder.Draw(radiusToSet);
    }

    private void Update()
    {
        stinger.Update();
        holder.Update();
    }
}

[Serializable]
public class InstrumentPlace
{
    [SerializeField] private Transform instrument;
    [SerializeField] private Transform instrumentPlace;
    private float sqrRadiusToSet;
    private Action onSetPlace;
    private bool isSet;

    public void Draw(float radius)
    {
        Gizmos.DrawWireSphere(instrumentPlace.position, radius);
    }

    public void Initialize(float sqrRadiusToSet, Action onEnd)
    {
        this.sqrRadiusToSet = sqrRadiusToSet;
        onSetPlace = onEnd;
    }

    public void Update()
    {
        if (isSet) return;
        
        var holderDirection = instrumentPlace.position - instrument.position;
        if (holderDirection.sqrMagnitude < sqrRadiusToSet)
        {
            onSetPlace?.Invoke();
            instrument.DOMove(instrumentPlace.position, holderDirection.magnitude);
            instrumentPlace.gameObject.SetActive(false);
            isSet = true;
        }
    }
}