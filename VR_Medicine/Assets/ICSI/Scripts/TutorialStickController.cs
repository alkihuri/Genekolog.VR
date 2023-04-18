using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialStickController : MonoBehaviour, ITutorialDisabler
{
    [SerializeField] private bool isHorizontalTutorial;
    [SerializeField] private UIPanelAnimation UIPanelAnimation;
    [SerializeField] private Image stickImage;
    [SerializeField] private Image arrow;
    [SerializeField] private Image leftWave;
    [SerializeField] private Image rightWave;
    [SerializeField] private int offsetAxisPosition;
    [SerializeField] private XRHandControllerLink targetHand;
    [SerializeField] private UnityEvent onTutorialEndEvent;
    private Action onTutorialIsEnd;
    private TargetValueListener tutorialState;
    private float targetValue = 0.5f;
    private bool isCompleted;
    private bool isStarted;
    private Tween animation;

    public void DisablePanel()
    {
        UIPanelAnimation.DisablePanel();
        isStarted = false;
        animation.Kill();
    }

    public Action OnTutorialIsEnd
    {
        get => onTutorialIsEnd;
        set => onTutorialIsEnd = value;
    }

    [Button]
    public void StartTutorial()
    {
        isStarted = true;
        isCompleted = false;
        UIPanelAnimation.EnablePanel();
        tutorialState = TargetValueListener.Start;
        StartAnimation();
    }

    private void StartAnimation()
    {
        if (isCompleted) return;
        leftWave.color = Color.clear;
        leftWave.transform.localScale = Vector3.one;
        rightWave.color = Color.clear;
        rightWave.transform.localScale = Vector3.one;

        var directionMove = (isHorizontalTutorial ? Vector3.right : Vector3.up);
        animation = DOVirtual.DelayedCall(.5f, () =>
        {
            animation = arrow.transform.DOScale(1.2f, .25f).OnComplete(() =>
            {
                animation = arrow.transform.DOScale(1, .25f).OnComplete(() =>
                {
                    animation = stickImage.transform.DOLocalMove(directionMove * offsetAxisPosition,
                        .5f).OnComplete(() =>
                    {
                        rightWave.color = Color.white;
                        rightWave.transform.DOScale(1.5f, .25f);
                        rightWave.DOFade(0, .25f);

                        animation = stickImage.transform.DOLocalMove(directionMove * -offsetAxisPosition, 1).OnComplete(
                            () =>
                            {
                                leftWave.color = Color.white;
                                leftWave.transform.DOScale(1.5f, .25f);
                                leftWave.DOFade(0, .25f);

                                animation = stickImage.transform.DOLocalMove(Vector3.zero, 0.5f)
                                    .OnComplete(StartAnimation);
                            });
                    });
                });
            });
        });
    }

    private enum TargetValueListener
    {
        Start,
        Revers,
        End
    }

    private void Update()
    {
        if (!isStarted) return;

        switch (tutorialState)
        {
            case TargetValueListener.Start:
                WaitingStickValue(TargetValueListener.Revers);

                break;
            case TargetValueListener.Revers:
                WaitingStickValue(TargetValueListener.End);
                break;
            case TargetValueListener.End:
                isCompleted = true;
                isStarted = false;
                UIPanelAnimation.DisablePanel();
                onTutorialEndEvent.Invoke();
                OnTutorialIsEnd?.Invoke();
                OnTutorialIsEnd = null;
                break;
        }
    }

    private void WaitingStickValue(TargetValueListener nextTutorialState)
    {
        var axisValue = isHorizontalTutorial
            ? targetHand.GetAxis2D(Common2DAxis.primaryAxis).x
            : targetHand.GetAxis2D(Common2DAxis.primaryAxis).y;

        if (axisValue > targetValue || axisValue < -targetValue)
        {
            tutorialState = nextTutorialState;
            targetValue = -axisValue;
        }
    }
}