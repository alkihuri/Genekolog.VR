using System;
using UnityEngine;
using UnityEngine.Events;

public class ICSITriggerCollision : MonoBehaviour
{
    [SerializeField] private PointDropDispenserColliderTriggerAction triggerEnterName;
    private bool isLastTrueTrigger;

    private void OnDisable()
    {
        if (isLastTrueTrigger)
        {
            isLastTrueTrigger = false;
            triggerEnterName.onTriggerExit.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains(triggerEnterName.nameTrigger))
        {
            triggerEnterName.onTriggerEnter.Invoke();
            isLastTrueTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains(triggerEnterName.nameTrigger))
        {
            isLastTrueTrigger = false;
            triggerEnterName.onTriggerExit.Invoke();
        }
    }
}

[Serializable]
public class PointDropDispenserColliderTriggerAction
{
    public string nameTrigger;
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
}