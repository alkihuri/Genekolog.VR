using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TVP
{
    public enum TypeOfTool
    {
        prezervativ,
        gel

    }

    public class AdditionalToolForUzi : MonoBehaviour
    {
        public UnityEvent<TypeOfTool, Transform> OnStartInteract = new UnityEvent<TypeOfTool, Transform>();
        public UnityEvent<TypeOfTool, Transform> OnStopInteract = new UnityEvent<TypeOfTool, Transform>();
        public UnityEvent<TypeOfTool, Transform> OnStaytInteract = new UnityEvent<TypeOfTool, Transform>();
        [SerializeField] private TypeOfTool _typeOfTool;
        [SerializeField] private UziScannerDeviceController _uziScannerDeviceController;
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private void Awake()
        {
            _uziScannerDeviceController = GameObject.FindObjectOfType<UziScannerDeviceController>();
            _startPosition = transform.position;
            _startRotation = transform.rotation;
            OnStartInteract.AddListener(_uziScannerDeviceController.AddAditionaltool);
        }

        public void RestorePosition()
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation ;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<UziScannerDeviceController>())
            {
                OnStartInteract?.Invoke(_typeOfTool, transform);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<UziScannerDeviceController>())
            {
                OnStopInteract?.Invoke(_typeOfTool, transform);
                RestorePosition();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<UziScannerDeviceController>())
            {
                OnStaytInteract?.Invoke(_typeOfTool, transform);
            }
        }
    }
}