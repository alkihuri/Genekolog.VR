using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TVP
{
    public class UziScannerRaycaster : MonoBehaviour
    {
        private const float DELAY_TIME = 1f;
        [SerializeField] private Image _image;
        [SerializeField, Range(10, 100)] private float _dimension;
        [SerializeField, Range(10, 8000)] private float _scaleDivider;
        [SerializeField] private List<UziHitInfo> _raycastStartDots = new List<UziHitInfo>();
        [SerializeField] private Texture2D _uziText;
        [SerializeField] UziScannerDeviceController _device;
        [SerializeField, Range(0, 100)] private float _scaningDelay;
        [SerializeField, Range(1, 100)] private int fps;

        [SerializeField] RenderTexture _cameraUZiFake;

        public float Dimension { get => _dimension; set => _dimension = value; }
        public float ScaleDivider { get => _scaleDivider; set => _scaleDivider = value; }
        public bool IsScaning { get; private set; }
        public bool IsDrawing { get; private set; }
        public int HEIGHT { get; private set; }
        public int WIDTH { get; private set; }

        private void Start()
        {

            HEIGHT = (int)_image.GetComponent<RectTransform>().rect.height;
            WIDTH = (int)_image.GetComponent<RectTransform>().rect.width;
            //_device = _device == null ? GameObject.FindAnyObjectByType<UziScannerDeviceController>() : _device;

            // _device.OnVaginaMounted.AddListener(() => StartCoroutine(LiteUpdate()));
            // _device.OnVaginaDemounted.AddListener(() => StopCoroutine(LiteUpdate()));

            // _uziText = new Texture2D((int)(Dimension * 2), (int)(Dimension * 2), TextureFormat.RG32, false);

            _uziText = new Texture2D(512, 512,TextureFormat.RG32, false);
            
            InnitRaycastStartDots();

            //   if(_device == null)
             StartCoroutine(ScaningCameraMocking());
        }
        // Update is called once per frame
        private IEnumerator ScaningMocking()
        {
            IsScaning = false;
            while (true)
            { 
                yield return new WaitForFixedUpdate();
                StartCoroutine(DoScaning());
                yield return new WaitWhile(() => IsScaning); 
                StartCoroutine(DrawPixelData());
                yield return new WaitWhile(() => IsDrawing); 
            }
        }
        private IEnumerator ScaningCameraMocking()
        {
            IsScaning = false;
            while (true)
            {
                yield return new WaitForFixedUpdate();
                _uziText = ToTexture2D(_cameraUZiFake); 
                _uziText.Apply();
                yield return new WaitUntil(()=>_uziText.isReadable);
                _image.material.mainTexture = _uziText;  
            }
        }
        private void Update()
        {

            fps = (int)(1 / Time.deltaTime);


        }
        Texture2D ToTexture2D(RenderTexture rTex)
        {
            Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false); 
            // ReadPixels looks at the active RenderTexture.
            RenderTexture.active = rTex;
            tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex.Apply();
            return tex;
        }
        private IEnumerator DoScaning()
        {
            IsScaning = true;
            int i = 0;
            foreach (UziHitInfo dot in _raycastStartDots)
            {
                i++;
                if (i % Mathf.Clamp(fps, 40, 60) == 0)
                    yield return new WaitForEndOfFrame();
                dot.dep = SendRaycast(dot);
            }
            IsScaning = false;
        }


        private IEnumerator DrawPixelData()
        {
            IsDrawing = true;
            foreach (UziHitInfo dot in _raycastStartDots)
            {
                DrawImage(dot);
            }
            _uziText.Apply();
            yield return new WaitUntil(() => _uziText.isReadable);
            _image.material.mainTexture = _uziText;
            IsDrawing = false;
        }

        private void DrawImage(UziHitInfo dot)
        {
            //StartCoroutine(FadeColor(dot));
            WithoutFade(dot);
        }

        private void WithoutFade(UziHitInfo dot)
        {
            var value = dot.dep;
            var color = new Color(value, value, value);
            if (dot.IsFake)
            {
                int leftX = (int)Mathf.Clamp(dot.pixelData.x - 1, 0, Dimension * 2);
                int rightX = (int)Mathf.Clamp(dot.pixelData.x + 1, 0, Dimension * 2);
                Color leftColor = _uziText.GetPixel(leftX, (int)dot.pixelData.y);
                Color rightColor = _uziText.GetPixel(rightX, (int)dot.pixelData.y);
                color = Color.Lerp(leftColor, rightColor, 0.5f);
            }
            _uziText.SetPixel((int)dot.pixelData.x, (int)dot.pixelData.y, color);
        }
        IEnumerator FadeColor(UziHitInfo dot)
        {
            float time = 0;
            while (time < DELAY_TIME)
            {
                yield return new WaitForSeconds(DELAY_TIME / 3);
                var value = dot.dep;
                var color = Color.Lerp(Color.black, Color.white, value);
                color = new Color((0.299f * color.r), (0.587f * color.g), (0.114f * color.b));
                var prevColor = _uziText.GetPixel((int)dot.pixelData.x, (int)dot.pixelData.y);

                var fadedColor = Color.Lerp(prevColor, color, time);
                _uziText.SetPixel((int)dot.pixelData.x, (int)dot.pixelData.y, fadedColor);
                time += DELAY_TIME / 3;
                _uziText.Apply();
                _image.material.mainTexture = _uziText;
            }

        }

        private float SendRaycast(UziHitInfo dot)
        {
            if (dot.IsFake)
                return 0;


            var direction = transform.forward + transform.right * (dot.CoeficientX / 2);

            Debug.DrawLine(dot.point + transform.position, direction, Color.yellow, DELAY_TIME);

            var hits = Physics.RaycastAll(dot.point + transform.position, direction, 1, LayerMask.GetMask("UZI_SCAN"));
            float density = 0;
            if (hits.Length > 0)
            {
                // Debug.DrawLine(dot.point + transform.position, hits[0].point, Color.white, 0.1f);
                // Debug.DrawLine(hits[0].point, hits.Last().point, Color.red, 0.5f);  
                density = hits.Sum(h => h.transform.gameObject.GetComponent<ScaningEntitySim>().DensityMultiplayer);
                //density = hits[0].transform.gameObject.GetComponent<ScaningEntitySim>().DensityMultiplayer;
                density *= Mathf.Clamp01(1 - hits[0].distance);
            }

            var min = 0;
            var max = 10;

            return Mathf.InverseLerp(min, max, density);
        }

        private void InnitRaycastStartDots()
        {
            int pixelX = 0;
            int pixelY = 0;
            for (float x = -Dimension; x < Dimension; x++)
            {
                for (float y = -Dimension; y < Dimension; y++)
                {
                    //var zDeltaByX = Mathf.InverseLerp(-Dimension, Dimension, x);
                    //zDeltaByX = zDeltaByX > 0.5 ? -zDeltaByX + 1 : zDeltaByX;

                    //var zDeltaByY = Mathf.InverseLerp(-Dimension, Dimension, y);
                    //zDeltaByY = zDeltaByY > 0.5 ? -zDeltaByY + 1 : zDeltaByY;


                    //var totalDelta = zDeltaByX * zDeltaByY / 3;

                    //totalDelta = 0;

                    var newDot = new Vector3(x / ScaleDivider, y / ScaleDivider, 0);


                    var newHit = new UziHitInfo(newDot);
                    newHit.point = newDot;
                    newHit.pixelData = new Vector2(pixelX, pixelY);


                    newHit.CoeficientX = (Mathf.InverseLerp(0, Dimension * 2, newHit.pixelData.x) * 2) - 1;

                    newHit.CoeficientY = (Mathf.InverseLerp(0, Dimension * 2, newHit.pixelData.y) * 2) - 1;



                    newHit.IsFake = (pixelX % 3 == 0 && pixelY % 3 == 0);

                    _raycastStartDots.Add(newHit);
                    pixelY++;
                }
                pixelX++;
            }
        }

        private void OnDrawGizmos()
        {
            if (_raycastStartDots.Count > 0)
            {
                foreach (UziHitInfo dot in _raycastStartDots)
                {
                    Gizmos.color = dot.IsFake ? Color.red : Color.green;
                    Gizmos.DrawSphere(dot.point + transform.position, 0.0001f);
                }
            }
        }

    }

}