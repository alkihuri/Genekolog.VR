using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using Autohand;
//using static UnityEditor.Experimental.GraphView.GraphView;

namespace TVP
{
    public class UziScannerDeviceController : InteractableBase
    {
        private Vector3 VAGINA_POSITION = new Vector3(0.59f, 0.16f, -1.42f);

        private Vector3 VAGINA_ROTATION = new Vector3(24, 44.6f, 13.5f);

        [SerializeField] GameObject _prez;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameObject _isPicked;
        [SerializeField] private DisplayController _displayController;
        [SerializeField] private RenderTexture _cameraTexture;
        [SerializeField] private Camera _uziCamera;
        [SerializeField] private Vector3 _startRotation;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] NiddleController _niddle;
        [SerializeField] private AutoHandPlayer _player;

        [Header("Events")]
        public UnityEvent OnPickedUp = new UnityEvent();
        public UnityEvent OnReleased = new UnityEvent();
        public UnityEvent OnVaginaMounted = new UnityEvent();
        public UnityEvent OnVaginaDemounted = new UnityEvent();


        [Header("Bollean fields")]
        [SerializeField] private bool _isScaning;
        [SerializeField] private bool _isBusy;
        [SerializeField] private bool _isStickedInVagina;
        private UziDevice _uziDevice;
        [SerializeField] private bool _isDeviceIsActive;

        public bool IsScaning { get => _isScaning; set => _isScaning = value; }
        public bool IsBusy { get => _isBusy; set => _isBusy = value; }
        public bool IsStickedInVagina { get => _isStickedInVagina; set => _isStickedInVagina = value; }
        public bool UziNormalCameraIsOn { get; private set; }
        public bool IsDeviceIsActive { get => _isDeviceIsActive; set => _isDeviceIsActive = value; }
        public bool IsGel { get; set; }
        public bool IsPrez { get; set; }

        private void Awake()
        {
            Cashing();
        }

        public void DeviceInnit(UziDevice device)
        {
            _uziDevice = device;
            _isDeviceIsActive = device.IsActive;
        }

        private void Cashing()
        {
            IsGel = false;
            IsPrez = false;
            _startPosition = transform.position;
            _startRotation = transform.eulerAngles;
            _rigidbody = GetComponent<Rigidbody>();
            _player = GameObject.FindObjectOfType<AutoHandPlayer>();
            _displayController = GameObject.FindObjectOfType<UziDisplayController>();
            _cameraTexture = Resources.Load("CameraImage") as RenderTexture;
            _uziCamera = GetComponentInChildren<Camera>();
            EventsSettings();
        }

        private void EventsSettings()
        {

            OnReleased.AddListener(Released);
            OnPickedUp.AddListener(PickedUp);



            OnVaginaMounted.AddListener(() => { IsStickedInVagina = true; IsScaning = true; });
            OnVaginaDemounted.AddListener(() => { IsStickedInVagina = false; IsScaning = false; });
            OnVaginaMounted.AddListener(UziCameraOn);
            OnVaginaMounted.AddListener(StickAtVagina);



            OnVaginaMounted.AddListener(() => _rigidbody.drag = 15);
            OnVaginaMounted.AddListener(() => _rigidbody.angularDrag = 1.5f);

            OnVaginaMounted.AddListener(() => _player.maxMoveSpeed = 0);
            OnVaginaMounted.AddListener(() => _player.snapTurnAngle = 0);


            OnVaginaDemounted.AddListener(DemountedFromVagine);


            //OnVaginaDemounted.AddListener(UziCameraOff);


        }
        public void StickAtVagina()
        {

            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.VaginaPenetreted_06_03_04);
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.UziEmulation_06_03_05);
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SimpleUziInvestigation_06_03_06);
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsDone_06_03); 
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.LastUziInvestigation_08_02_01);
        }



        public void DemountedFromVagine()
        {
            _rigidbody.drag = 0;
            _rigidbody.angularDrag = 0.05f;
            _player.maxMoveSpeed = 1.8f;
            _player.snapTurnAngle = 30;
        }

        private void Start()
        {

            UziCameraOff();
            //UziCameraOn();
        }


        public void PickedUPInvoker() => OnPickedUp?.Invoke();
        public void ReleasedInvoker() => OnReleased?.Invoke();

        /*
        private IEnumerator ReturnAtPlace()
        {
            yield return new WaitForSeconds(3);
            if (!IsBusy && !IsScaning && !IsStickedInVagina)
            {
                transform.DOMove(_startPosition, 3);
                transform.DORotate(_startRotation, 3).OnComplete(FixedAtPostion);
            }
            else
            {
                StopCoroutine(ReturnAtPlace());
            }
        }
        */
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<InteractableBase>())
            {
                var woman = other.gameObject.GetComponent<InteractableBase>();
                Interact(woman);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<InteractableBase>())
            {
                var woman = other.gameObject.GetComponent<InteractableBase>();
                StopInteract(woman);
            }
        }


        public override void Interact(InteractableBase interactor)
        {
            OnVaginaMounted?.Invoke();
        }
        public override void StopInteract(InteractableBase interactor)
        {
            OnVaginaDemounted?.Invoke();
        }

        public void UziCameraOn()
        {

            UziNormalCameraIsOn = true;
        }

        public void UziCameraOff()
        {

            UziNormalCameraIsOn = false;
        }

        public void UziScaningOn()
        {
            _displayController.SetText("Начинаем сканирование");
        }

        private void Update()
        {
            //CameraStabilization();
        }

        private void CameraStabilization()
        {
            // if (UziNormalCameraIsOn) _uziCamera.transform.eulerAngles = new Vector3(_uziCamera.transform.eulerAngles.x, _uziCamera.transform.eulerAngles.y, 0);
        }

        public void PickedUp()
        {
            IsBusy = true;
            _rigidbody.constraints = RigidbodyConstraints.None;
        }
        public void Released()
        {
            IsBusy = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void AddAditionaltool(TypeOfTool tool, Transform toolTransform)
        {
            if (tool == TypeOfTool.prezervativ)
            {
                IsPrez = true;
               // toolTransform.DOShakeScale(0.5f).OnComplete(() => Destroy(toolTransform.gameObject));
                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.PrezervativIsApplied_06_03_02);
                _prez.SetActive(true);
            }
            if (tool == TypeOfTool.gel)
            {
                IsGel = true;
                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.GelIsApplied_06_03_01);
               // toolTransform.DOShakeScale(0.5f);
            }
        }

    }
}