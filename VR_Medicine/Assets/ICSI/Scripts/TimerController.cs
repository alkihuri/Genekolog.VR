using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text counter;
    [SerializeField] private Image clockImage;
    private float timer;
    private float timerCounter;
    private Coroutine timerCalculate;
    private bool isPause;

    public void InitializeTimer(float timer)
    {
        this.timer = timer;
        timerCounter = timer;
        SetTimerCounter();
    }
    
    public void StartTimer()
    {
        counter.color = Color.white;
        
        timerCalculate = StartCoroutine(TimerCounter());
    }

    public void Pause() => isPause = true;
    public void Unpause() => isPause = false;

    public void StopTimer()
    {
        StopCoroutine(timerCalculate);
        counter.DOColor(Color.green, .5f);
    }

    private IEnumerator TimerCounter()
    {
        var timer = timerCounter;
        while (timerCounter > 0)
        {
            yield return null;
            if (!isPause)
            {
                timerCounter -= Time.deltaTime;
                timerCounter = (float)Math.Round(timerCounter, 2);
                clockImage.fillAmount = timerCounter / timer;
                SetTimerCounter();
            }
        }

        counter.DOColor(Color.red, .5f);
        timerCounter = 0;
        SetTimerCounter();
    }

    private void SetTimerCounter()
    {
        counter.text = ToTimeFormat(timerCounter);
    }

    private static string ToTimeFormat(float value)
    {
        int seconds = (int)value;
        int mSeconds = (int)(100 * value) - 100 * ((int)value);
        return string.Format($"{seconds}.{mSeconds}");
    }
}