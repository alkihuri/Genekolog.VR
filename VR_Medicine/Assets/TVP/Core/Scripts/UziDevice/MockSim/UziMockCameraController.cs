using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVP.MockSim
{

    [ExecuteAlways]
    public class UziMockCameraController : MonoSinglethon<UziMockCameraController>
    {
        private Camera _thisCamera;

        [SerializeField] private GameObject _biopsyGuidelLine;

        public float MAX_ZOOM = 0.1f;
        public float MIN_ZOOM = 0.01f;

        public float MAX_X = 0.1f;
        public float MIN_X = -0.1f;

        public float MAX_Z = 0.1f;
        public float MIN_Z = -0.1f;


        private void Awake()
        {
            _thisCamera = GetComponent<Camera>();

            if (_biopsyGuidelLine.activeInHierarchy)
                BiopsyGuideLineSwotcher();
        }

        [ContextMenu("Move Left")]
        public void MoveLeft()
        {
            MoveX(0.01f);
        }
        [ContextMenu("Move Right")]
        public void MoveRight()
        {
            MoveX(-0.01f);
        }
        [ContextMenu("Move Up")]
        public void MoveUp()
        {
            MoveZ(-0.01f);
        }
        [ContextMenu("Move Down")]
        public void MoveDown()
        {
            MoveZ(0.01f);
        }

        [ContextMenu("Zoom In")]
        public void ZoomIn()
        {
            Zoom(-0.01f);
        }
        [ContextMenu("Zoom Out")]
        public void ZoomOut()
        {
            Zoom(0.01f);
        }


        public void BiopsyGuideLineSwotcher()
        {
            _biopsyGuidelLine.SetActive(!_biopsyGuidelLine.activeInHierarchy);
        }

        private void MoveX(float value)
        {
            var currentX = transform.position.x;
            var newX = currentX + value;
            //newX = Mathf.Clamp(newX,MIN_X,MAX_X);
            transform.DOMoveX(newX, 1);
        }
        private void MoveZ(float value)
        {
            var currentZ = transform.position.z;
            var newZ = currentZ + value;
            //newZ = Mathf.Clamp(newZ, MIN_Z, MAX_Z);
            transform.DOMoveZ(newZ, 1);
        }

        private void Zoom(float value)
        {
            var currentZoom = _thisCamera.orthographicSize;
            var newZoom = currentZoom + value;
            newZoom = Mathf.Clamp(newZoom, MIN_ZOOM, MAX_ZOOM);
            _thisCamera.DOOrthoSize(newZoom, 1);
        }

    }
}