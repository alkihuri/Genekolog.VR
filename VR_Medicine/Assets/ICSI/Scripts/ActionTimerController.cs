using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTimerController : MonoBehaviour
{
    [SerializeField] private float timerCount;
    [SerializeField] private TimerController timer;
    [SerializeField] private UIPanelAnimation panelAnimation;

    public void StartTimer() => timer.StartTimer();
    public void StopTimer() => timer.StopTimer();
    public void PauseTimer() => timer.Pause();
    public void UnpauseTimer() => timer.Unpause();
    public void EnableTimer()
    {
        timer.InitializeTimer(timerCount);
        panelAnimation.EnablePanel();
    }
    public void DisableTimer() => panelAnimation.DisablePanel();
}
