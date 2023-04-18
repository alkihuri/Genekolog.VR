using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;

namespace TVP
{
    public class PompController : MonoSinglethon<PompController>
    {

        public UnityEvent<PompController> OnPressuareChanged = new UnityEvent<PompController>();

        [SerializeField] private bool _isActive;

        public UnityEvent<PompController> OnDeviceCahngeState = new UnityEvent<PompController>();

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnDeviceCahngeState.Invoke(this);

                if (value)
                {
                    SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.PompIsOn_06_01_03);
                }
                else
                {
                    SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.PompIsWorking_06_01_06);
                    SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsDone_06_01);
                }
            }
        }
        [SerializeField] int _pressuare;
        private int _pressaureMultiplayer;

        public int Pressuare
        {
            get
            {
                return _pressuare;
            }
            set
            {
                _pressuare = Mathf.Clamp(value, MIN_PREASSURE, MAX_PRESURE);
                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.PressuarePompLevelCheck_06_01_04);
                OnPressuareChanged?.Invoke(this);
            }
        }

        public int PressaureMultiplayer { get => _pressaureMultiplayer; set => _pressaureMultiplayer = value; }
        public int MIN_PREASSURE { get; internal set; }
        public int MAX_PRESURE { get; internal set; }

        private void Start()
        {
            MIN_PREASSURE = 90;
            MAX_PRESURE = 200;

            OnPressuareChanged.AddListener(PompUIController.CurrentSimulation.UpdatePressuareUI);
            OnDeviceCahngeState.AddListener(PompUIController.CurrentSimulation.UpdatePompStateUI);
            PressaureMultiplayer = 0;
            Pressuare = 0;
            IsActive = false;
        }
        public void Increse()
        {
            Pressuare += 10;
        }

        public void Decrese()
        {
            Pressuare -= 10;
        }
        public void TurnOn()
        {
            Pressuare = 0;
            IsActive = true;
        }

        public void TurnOff()
        {
            Pressuare = 0;
            IsActive = false;
        }

        public void Invert()
        {
            IsActive = !IsActive; 
            Pressuare = 0; 
        }


        public void DoPressaure()
        {
            PressaureMultiplayer = 1;
        }

        public void StopPressaure()
        {
            PressaureMultiplayer = 0;
        }
    }
}