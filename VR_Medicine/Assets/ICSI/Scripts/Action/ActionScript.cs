using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActionScript : MonoBehaviour
{
    [SerializeField] private UnityEvent onStartAction;
    [SerializeField] protected UnityEvent onEndAction;
    [SerializeField] protected PartActionObject[] parts;
    protected Action onActionPartEnd;
    protected int counterPartAction;
    protected List<PartActionObject> listenedPartAction = new();
    protected List<ActionInteractableObject> listenedObjects = new();

    public virtual ActionScript Initialize()
    {
        onStartAction.Invoke();

        foreach (var part in parts)
        {
            listenedPartAction.Add(part);
        }

        InitializeObjectInPart();

        return this;
    }

    protected void InitializeObjectInPart()
    {
        listenedPartAction[0].OnStartPart?.Invoke();
        foreach (var interactableObject in listenedPartAction[0].objects)
        {
            interactableObject.Initialize().OnComplete(() => OnInteractableObjectEndAction(interactableObject));
            listenedObjects.Add(interactableObject);
        }
    }

    public ActionScript OnComplete(Action action)
    {
        onActionPartEnd = action;
        return this;
    }

    protected void OnInteractableObjectEndAction(ActionInteractableObject obj)
    {
        obj.ClearAction();
        listenedObjects.Remove(obj);

        if (listenedObjects.Count == 0)
        {
            listenedPartAction[0].OnEndPart?.Invoke();
            listenedPartAction.RemoveAt(0);

            if (listenedPartAction.Count == 0)
            {
                onEndAction?.Invoke();
                onActionPartEnd?.Invoke();
            }
            else
            {
                OnStartInteractable();
                InitializeObjectInPart();
            }
        }
    }

    protected virtual void OnStartInteractable()
    {
        
    }
}

[Serializable]
public class PartActionObject
{
    public UnityEvent OnStartPart;
    public UnityEvent OnEndPart;
    public ActionInteractableObject[] objects;
}