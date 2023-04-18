using System;
using UnityEngine;
using UnityEngine.Events;

public class DispenserTipTriggerCollider : MonoBehaviour
{
    [SerializeField] private DispenserTipType type;
    [SerializeField] private UnityEvent onTipTrigger;

    private void OnTriggerEnter(Collider other)
    {
        var tip = other.GetComponentInParent<DispenserTip>();
        if (tip != null && tip.Type == type && tip.IsInteractable)
        {
            onTipTrigger?.Invoke();
            tip.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}