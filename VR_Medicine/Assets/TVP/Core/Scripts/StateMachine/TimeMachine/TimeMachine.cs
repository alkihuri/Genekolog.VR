using System;
using System.Collections;
using System.Collections.Generic;
using TVP.UI;
using UnityEngine;
using UnityEngine.Events;

namespace TVP
{
    public class TimeMachine : MonoBehaviour
    {
        [SerializeField] private TimerProgressUIController _UItimer;

        public UnityEvent<float, float> OnTimerChangedValue = new UnityEvent<float, float>();
        public UnityEvent OnTimerStart = new UnityEvent();
        public UnityEvent OnStateNotFinishedAtTime = new UnityEvent();
        public UnityEvent OnTimerFinihsh = new UnityEvent();
        public UnityEvent OnStateFinihsh = new UnityEvent();

        [field: SerializeField] public bool TimerIsOn { get; private set; }
        [field: SerializeField] public int CurrentSec { get; private set; }
        [field: SerializeField] public int TimerForState { get; private set; }

        [field: SerializeField]
        public bool IsDoneAtTime { get; private set; } 

        [field: SerializeField] public bool CanCountSecond { get; private set; }

        private void Awake()
        {
            EventSettings();
            TimerIsOn = false;
        }

        public void SetCountMode()
        {
            CanCountSecond = true;
        }
        private void EventSettings()
        {
            OnTimerChangedValue.AddListener(_UItimer.UpdateState);

            OnTimerStart.AddListener(() => TimerIsOn = true);

            OnTimerFinihsh.AddListener(() => TimerIsOn = false);
            OnTimerFinihsh.AddListener(() => CanCountSecond = false);

            OnStateFinihsh.AddListener(() => TimerIsOn = false);
            OnStateFinihsh.AddListener(() => CanCountSecond = false);
            OnStateFinihsh.AddListener(Stoptomer);

        }

        public void StartTimer(int amount)
        {
            CanCountSecond = false;
            StopAllCoroutines();
            StartCoroutine(Timer(amount));
        }

        IEnumerator Timer(int amount)
        {
            yield return new WaitUntil(() => CanCountSecond);
            CurrentSec = 0;
            TimerForState = amount;
            OnTimerStart?.Invoke();
            for (int x = 0; x < amount && TimerIsOn; x++)
            {
                yield return new WaitForSeconds(1);
                CurrentSec += 1;
                OnTimerChangedValue?.Invoke(CurrentSec, amount);
                IsDoneAtTime = CurrentSec < TimerForState;
            }
            OnTimerFinihsh?.Invoke();



            if (!IsDoneAtTime)
                OnStateNotFinishedAtTime?.Invoke();
        }

        public void Stoptomer()
        {
            _UItimer.Reset();
        }

        public void Reset()
        {
            _UItimer.Reset();
        }
    }
}