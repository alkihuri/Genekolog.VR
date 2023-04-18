using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using Autohand;
using System.Linq.Expressions;

namespace TVP
{
    public class BiopsyAdapterController : MonoSinglethon<BiopsyAdapterController>
    {
        [SerializeField] NiddleController _niddleController;
        [SerializeField] PompController _pompController;


        [SerializeField, Range(0, 100)] private float _fluidVolume = 0;
        [SerializeField, Range(0, 100)] private float _fluidMaxVolume = 15;

        [SerializeField] GameObject _fluidVolumeShape;

        public float FluidVolume { get => _fluidVolume; set { _fluidVolume = value; } }

        private void ViewFluidVolumeupdate(float changedValue)
        {
            //some shit
            var value = Mathf.Lerp(0, 0.06f, _fluidVolume / _fluidMaxVolume);
            _fluidVolumeShape.transform.DOScaleY(value, 0.1f / changedValue);
        }

        public float FluidMaxVolume { get => _fluidMaxVolume; set => _fluidMaxVolume = value; }
        public bool IsNiddleMounted { get; private set; }
        public bool IsDispensing { get; private set; }
        public bool IsAspirating { get; private set; }

        public UnityEvent<float> OnAspirate = new UnityEvent<float>();
        public UnityEvent<float> OnDispanse = new UnityEvent<float>();


        private void Awake()
        {
            _pompController = _pompController == null ? GameObject.FindAnyObjectByType<PompController>() : _pompController;

            OnAspirate.AddListener(Aspirate);
            OnDispanse.AddListener(Dispense);

            OnAspirate.AddListener(ViewFluidVolumeupdate);
            OnDispanse.AddListener(ViewFluidVolumeupdate);

            IsNiddleMounted = false;
            FluidMaxVolume = 100;
            FluidVolume = 0;
        }
        private void Start()
        {
        }
        // Start is called before the first frame update
        public void MountNiddle()
        {
            _niddleController.Innit(this);
            IsNiddleMounted = true;
        }
        public void DemountNiddle()
        {
            _niddleController.Disapose(this);
            IsNiddleMounted = false;
        }

        public void Aspirate(float aspiratedVolume)
        {



            aspiratedVolume *= Mathf.Clamp01(_pompController.Pressuare * _pompController.PressaureMultiplayer);

            if (_niddleController == null)
                return;

            if (!_niddleController.CanDispanseOrAspirate)
                return;


            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.AspiratingTestIsDone_06_04_02);
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.NiddleIsMountedToAdapter_06_04_03);
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsDone_06_04);

            if (FluidVolume <= FluidMaxVolume && (_niddleController.Fluid.Volume - aspiratedVolume) >= 0)
            {

                _niddleController.Fluid.Volume -= aspiratedVolume;
                FluidVolume = Mathf.Clamp(FluidVolume + aspiratedVolume, 0, FluidMaxVolume);

            }
        }

        public void Dispense(float dispensedVolume)
        {

            dispensedVolume *= Mathf.Clamp01(_pompController.Pressuare * _pompController.PressaureMultiplayer);

            if (_niddleController == null)
                return;

            if (!_niddleController.CanDispanseOrAspirate)
                return;

            if (FluidVolume > 0 && (_niddleController.Fluid.Volume + dispensedVolume) <= _niddleController.Fluid.MaxVolume)
            {
                _niddleController.Fluid.Volume += dispensedVolume;
                FluidVolume = Mathf.Clamp(FluidVolume - dispensedVolume, 0, FluidMaxVolume);
            }
        }

        public void AspirateInvoker()
        {
            StartCoroutine(StartAspirating());
        }
        public void DispenseInvoker()
        {
            StartCoroutine(StartDispensing());
        }


        private IEnumerator StartAspirating()
        {
            IsAspirating = true;
            while (IsAspirating)
            {
                OnAspirate?.Invoke(1);
                yield return new WaitForSeconds(0.2f);
            }
        }
        private IEnumerator StartDispensing()
        {
            IsDispensing = true;
            while (IsDispensing)
            {
                OnDispanse?.Invoke(1);
                yield return new WaitForSeconds(0.2f);
            }
        }

        public void StopDispenseASpirate()
        {
            IsDispensing = false;
            IsAspirating = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<NiddleController>())
            {
                _niddleController = other.gameObject.GetComponent<NiddleController>();
                MountNiddle();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<NiddleController>())
            {
                _niddleController = other.gameObject.GetComponent<NiddleController>();
                DemountNiddle();
            }
        }


    }
}