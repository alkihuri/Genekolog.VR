using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ActionInteractableObject : MonoBehaviour
{
    public AnimatorInstrumentController animator;
    [SerializeField] private UnityEvent OnStartAction;
    public UnityEvent OnEndAction;
    private Action onActionEnd;

    public void OpenObject() => animator.SetOpen();
    public void CloseObject() => animator.SetClose();

    [Button]
    public void InvokeEndAction()
    {
        OnEndAction.Invoke();
        onActionEnd?.Invoke();
    }
    
    public ActionInteractableObject Initialize()
    {
        OnStartAction.Invoke();
        return this;
    }

    public ActionInteractableObject ClearAction()
    {
        onActionEnd = null;
        return this;
    }

    public ActionInteractableObject OnComplete(Action action)
    {
        onActionEnd = action;
        return this;
    }
}

[Serializable]
public class AnimatorInstrumentController
{
    private readonly int TRIGGER_KEY_OPEN_ANIM = Animator.StringToHash("Open");
    private readonly int TRIGGER_KEY_CLOSE_ANIM = Animator.StringToHash("Close");
    
    public Animator Animator;

    [Button]
    public void SetOpen()
    {
        if (Animator == null) return;
        
        ResetTriggers();
        Animator.SetTrigger(TRIGGER_KEY_OPEN_ANIM);
    }

    [Button]
    public void SetClose()
    {
        if (Animator == null) return;

        ResetTriggers();
        Animator.SetTrigger(TRIGGER_KEY_CLOSE_ANIM);
    }

    private void ResetTriggers()
    {
        Animator.ResetTrigger(TRIGGER_KEY_CLOSE_ANIM);
        Animator.ResetTrigger(TRIGGER_KEY_OPEN_ANIM);
    }
}