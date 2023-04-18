using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private HandControllerTutorialData leftTutorial;
    [SerializeField] private HandControllerTutorialData rightTutorial;
    [Space, SerializeField] private TutorialItemData[] listenedObjects;
    private readonly Dictionary<Grabbable, TutorialItemData> listenedObjectDictinary = new();
    private Grabbable lastLeftGrabbable;
    private ITutorialDisabler lastLeftTutorial;
    private Grabbable lastRightGrabbable;
    private ITutorialDisabler lastRightTutorial;
    
    public void ShowTriggerButton(Grabbable grabbable)
    {
        if (lastLeftGrabbable == grabbable)
            ShowTutorial(leftTutorial.TriggerButton, true);
        else if (lastRightGrabbable == grabbable)
            ShowTutorial(rightTutorial.TriggerButton, false);
    }

    public void ShowPrimaryButton(Grabbable grabbable)
    {
        if (lastLeftGrabbable == grabbable)
            ShowTutorial(leftTutorial.PrimaryButton, true);
        else if (lastRightGrabbable == grabbable)
            ShowTutorial(rightTutorial.PrimaryButton, false);
    }

    public void ShowSecondButton(Grabbable grabbable)
    {
        if (lastLeftGrabbable == grabbable)
            ShowTutorial(leftTutorial.SecondButton, true);
        else if (lastRightGrabbable == grabbable)
            ShowTutorial(rightTutorial.SecondButton, false);
    }

    public void ShowStickVertical(Grabbable grabbable)
    {
        if (lastLeftGrabbable == grabbable)
        {
            ShowTutorial(leftTutorial.VerticalStick, true);
            leftTutorial.VerticalStick.gameObject.SetActive(true);
            leftTutorial.DefaultStick.SetActive(false);
        }
        else if (lastRightGrabbable == grabbable)
        {
            ShowTutorial(rightTutorial.VerticalStick, false);
            rightTutorial.VerticalStick.gameObject.SetActive(true);
            rightTutorial.DefaultStick.SetActive(false);
        }
    }

    public void ShowStickHorizontal(Grabbable grabbable)
    {
        if (lastLeftGrabbable == grabbable)
        {
            ShowTutorial(leftTutorial.HorizontalStick, true);
            leftTutorial.HorizontalStick.gameObject.SetActive(true);
            leftTutorial.DefaultStick.SetActive(false);
        }
        else if (lastRightGrabbable == grabbable)
        {
            ShowTutorial(rightTutorial.HorizontalStick, false);
            rightTutorial.HorizontalStick.gameObject.SetActive(true);
            rightTutorial.DefaultStick.SetActive(false);
        }
    }

    private void ShowTutorial(ITutorialDisabler tutorialButton, bool isLeft/*, Action additiveOnEnd = null*/)
    {
        if (isLeft)
            lastLeftTutorial = tutorialButton;
        else
            lastRightTutorial = tutorialButton;
        tutorialButton.StartTutorial();
        tutorialButton.OnTutorialIsEnd = () => OnTutorialCompleted(isLeft);
        //tutorialButton.OnTutorialIsEnd += additiveOnEnd;
    }

    private void OnTutorialCompleted(bool isLeftHand)
    {
        if (isLeftHand)
        {
            if (lastLeftGrabbable != null)
            {
                UnfollowedListenedObject(lastLeftGrabbable);
            }

            return;
        }

        if (lastRightGrabbable != null)
        {
            UnfollowedListenedObject(lastRightGrabbable);
        }
    }

    private void UnfollowedListenedObject(Grabbable grabbable)
    {
        switch (listenedObjectDictinary[grabbable].ShowType)
        {
            case TutorialShowType.Grab:
                grabbable.OnBeforeGrabEvent -= StartShowTutorial;
                grabbable.OnReleaseEvent -= DisableTutorial;
                break;
            case TutorialShowType.Highlight:
                grabbable.OnBeforeHighlightEvent -= StartShowTutorial;
                grabbable.OnUnhighlightEvent -= DisableTutorial;
                break;
        }

        listenedObjectDictinary.Remove(grabbable);
    }


    private void Start()
    {
        foreach (var item in listenedObjects)
        {
            switch (item.ShowType)
            {
                case TutorialShowType.Grab:
                    item.Grabbable.OnBeforeGrabEvent += StartShowTutorial;
                    item.Grabbable.OnReleaseEvent += DisableTutorial;
                    break;
                case TutorialShowType.Highlight:
                    item.Grabbable.OnBeforeHighlightEvent += StartShowTutorial;
                    item.Grabbable.OnUnhighlightEvent += DisableTutorial;
                    break;
            }

            listenedObjectDictinary.Add(item.Grabbable, item);
        }
    }

    private void StartShowTutorial(Hand hand, Grabbable grabbable)
    {
        if (listenedObjectDictinary.ContainsKey(grabbable))
        {
            if (hand.left)
                lastLeftGrabbable = grabbable;
            else
                lastRightGrabbable = grabbable;
        }
    }

    private void DisableTutorial(Hand hand, Grabbable grabbable)
    {
        if (listenedObjectDictinary.ContainsKey(grabbable))
        {
            if (hand.left)
            {
                lastLeftGrabbable = null;
                lastLeftTutorial?.DisablePanel();
                lastLeftTutorial = null;
            }
            else
            {
                lastRightGrabbable = null;
                lastRightTutorial?.DisablePanel();
                lastRightTutorial = null;
            }
        }
    }
}

[Serializable]
public class HandControllerTutorialData
{
    public TutorialTriggerController TriggerButton;
    public TutorialButtonController PrimaryButton;
    public TutorialButtonController SecondButton;
    public TutorialStickController VerticalStick;
    public TutorialStickController HorizontalStick;
    public GameObject DefaultStick;
}

[Serializable]
public class TutorialItemData
{
    public Grabbable Grabbable;
    public TutorialShowType ShowType;
}

public enum TutorialShowType
{
    Grab,
    Highlight
}

public interface ITutorialDisabler
{
    Action OnTutorialIsEnd { get; set; }
    void StartTutorial();

    void DisablePanel();
    //void OnReleaseObject();
}