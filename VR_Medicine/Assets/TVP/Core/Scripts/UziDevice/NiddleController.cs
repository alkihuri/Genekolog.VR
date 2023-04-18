using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace TVP
{
    public enum NiddleType
    {
        G17,
        G18,
        G19,
        G20,
        G21,
        G22
    }


    public class NiddleController : MonoBehaviour
    {
        [SerializeField] private bool _isMounted;
        [SerializeField] private NiddleType _type;
        private BiopsyAdapterController _adapter;
        [SerializeField] private bool canDispanseOrAspirate;
        [SerializeField] GameObject _otsOkayPressaure;

        public Dictionary<NiddleType, int> PressuareRules = new Dictionary<NiddleType, int>();


        public bool IsMounted
        {
            get => _isMounted;
            set
            {
                _isMounted = value;
                if (!_isMounted)
                {
                    _adapter = null;
                }
            }
        }

        public bool CanDispanseOrAspirate { get => canDispanseOrAspirate; set => canDispanseOrAspirate = value; }
        public FluidBase Fluid;



        private void Awake()
        {
            PressuareRules.Add(NiddleType.G17, 150);
            PressuareRules.Add(NiddleType.G18, 170);
            PressuareRules.Add(NiddleType.G19, 190);
            PressuareRules.Add(NiddleType.G20, 90);
            PressuareRules.Add(NiddleType.G21, 90);
            PressuareRules.Add(NiddleType.G22, 90);
            PompController.CurrentSimulation.OnPressuareChanged.AddListener(CheckCorrectPressuareOnDevice);

        }

        public void InnitNiddle(string niddle)
        {
            Enum.TryParse(niddle, out _type);
            CheckCorrectPressuareOnDevice();

        }

        public void CheckCorrectPressuareOnDevice(PompController device)
        {

            if (device.Pressuare >= PressuareRules[_type])
            {
                _otsOkayPressaure.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                _otsOkayPressaure.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        public void CheckCorrectPressuareOnDevice( )
        {

            if (PompController.CurrentSimulation.Pressuare >= PressuareRules[_type])
            {
                _otsOkayPressaure.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                _otsOkayPressaure.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            IsMounted = false;
        }

        public void MountOnDevice()
        {
            IsMounted = true;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (IsMounted)
            {
                if (other.gameObject.GetComponent<FluidBase>())
                {
                    CanDispanseOrAspirate = true;
                    Fluid = other.gameObject.GetComponent<FluidBase>();

                    SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SurgeryIsStart_07_01_01);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (IsMounted)
            {
                if (other.gameObject.GetComponent<FluidBase>())
                {
                    CanDispanseOrAspirate = false;
                    SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SurgeryIsDone_07_01_03);
                    SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsDone_07_01);
                    Fluid = null;
                }
            }
        }


        public void Aspirate()
        {

            _adapter.OnAspirate.Invoke(1);
        }

        public void Dispanse()
        {
            _adapter.OnAspirate.Invoke(1);
        }

        internal void Innit(BiopsyAdapterController biopsyAdapterController)
        {
            _adapter = biopsyAdapterController;
        }

        internal void Disapose(BiopsyAdapterController biopsyAdapterController)
        {
            _adapter = null;
        }
    }
}