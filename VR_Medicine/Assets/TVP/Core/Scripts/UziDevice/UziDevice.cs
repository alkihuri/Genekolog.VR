using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace TVP
{
    public class UziDevice : MonoSinglethon<UziDevice>
    {


        [Header("HARDCODE")]

        [SerializeField] Transform _uziDeviceAdapterPivot;
        [SerializeField] Transform _adapter;
        [SerializeField] Transform _niddle;


        [SerializeField] private UziScannerDeviceController _uziController;
        [SerializeField] private bool _isActive;
        [SerializeField] private DisplayController _displayController;

        [SerializeField] GameObject _hintDisplayObject;
        [SerializeField] GameObject _uziImage;

        [SerializeField] GameObject _display;

        public UnityEvent<UziDevice> OnDeviceCahngeState = new UnityEvent<UziDevice>();

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                _display.SetActive(_isActive);
                OnDeviceCahngeState.Invoke(this);

            }
        }

        public bool IsNiddleAttached { get; private set; }

        private void Awake()
        {
            _displayController = GameObject.FindObjectOfType<DisplayController>();
            _uziController = GetComponentInChildren<UziScannerDeviceController>();
            OnDeviceCahngeState.AddListener(_uziController.DeviceInnit);



            _uziController.OnVaginaMounted.AddListener(ShowHintDisplay);
            _uziController.OnVaginaDemounted.AddListener(HideHintDisplay);
        }

        private void HideHintDisplay()
        {
            _uziImage.SetActive(false);
            _hintDisplayObject.SetActive(false);
            //_hintDisplayObject.transform.DOScale(0, 1).OnComplete(() => _hintDisplayObject.SetActive(false));
        }

        private void ShowHintDisplay()
        {
            _uziImage.SetActive(true);
            _hintDisplayObject.SetActive(true);
           // _hintDisplayObject.transform.DOScale(1, 1);
        }

        private void Start()
        {
            IsActive = false; 
            // IsActive = true;
        }
        public void TurnOn()
        {
            IsActive = true;
        }

        public void TurnOff()
        {
            IsActive = false;
        }

        public void Invert()
        {
            IsActive = !IsActive;
            _display.SetActive(IsActive);

            if(IsActive)
            {
                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.UziOsOn_06_01_01);
            }
            if(_display.activeInHierarchy)
            {

                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.UziDispalySettedUp_06_01_02);
            }

        }
        public void ShowInfo(string text)
        {
            _displayController.SetText(text);
        }


        public void AttachAdapter()
        {
            if (_adapter == null)
                return;
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.BiopsyAdapterIsMounted_06_03_03);

            IsNiddleAttached = true;
            _adapter.SetParent(_uziDeviceAdapterPivot); 
            _adapter.DOShakeScale(1); 
        }


        public void AttachNiddle()
        {
            if (_niddle == null)
                return;

            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.NiddleIsMountedToAdapter_06_04_03);
            IsNiddleAttached = true;
            _niddle.SetParent(_adapter); 
            _niddle.DOShakeScale(1); 
        }
        public void DeattachAdapter()
        {
            if (_adapter == null)
                return;


            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.DeattachAdapterFromDevice_08_01_06);   
            IsNiddleAttached = false;
            _adapter.SetParent(null);
            _adapter.DOShakeScale(1);
        }


        public void DeattachNiddle()
        {
            if (_niddle == null)
                return;

            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.DeattachNiddleFromAdapter_08_01_02); 
            IsNiddleAttached = false;
            _niddle.SetParent(null);
            _niddle.DOShakeScale(1);
        }


    }
}