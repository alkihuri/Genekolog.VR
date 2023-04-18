using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System;

namespace Common.UI
{
    public class CancasPositionController : MonoBehaviour
    {

        [SerializeField] Transform _playerCamera;
        [SerializeField] Transform _canvas;
        [SerializeField] Transform _target;
        [SerializeField] float _yTopLimit;
        [SerializeField] float _yBottomLimit;

        public float InverserPlayerCameraXRotation { get; private set; }
        public float InverserPlayerCameraYRotation { get; private set; }


        private void Start()
        {
            StartCoroutine(LiteUpdate());
        }
        private void OnEnable()
        {
            StopAllCoroutines();
            StartCoroutine(LiteUpdate());
        }
        private void OnDisable()
        {
            StopAllCoroutines(); 
        }


        IEnumerator LiteUpdate()
        {
            while(gameObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(1);
                FoolowTaget();
            }
        }
        private void SmoothCanvas()
        {

            var xAngle =  _playerCamera.localEulerAngles.x;

            xAngle = xAngle > 270 ? xAngle - 360 : xAngle;
            xAngle = Mathf.Clamp(xAngle, -30, 30);
            print(xAngle);

            InverserPlayerCameraXRotation = Mathf.InverseLerp(30, -30, xAngle);
            InverserPlayerCameraYRotation = Mathf.InverseLerp(-20, 20, _playerCamera.localEulerAngles.y);

            var y = Mathf.Lerp(_yBottomLimit, _yTopLimit, InverserPlayerCameraXRotation);
            
            
            
            var x = Mathf.Lerp(1.5f, -1.5f, InverserPlayerCameraYRotation);

            _canvas.DOLocalMoveY(x, 1);
        }

        public void FoolowTaget()
        {
            _canvas.DOMove(_target.position,1);
            _canvas.DORotate(_target.eulerAngles, 1);

        }
    }
}