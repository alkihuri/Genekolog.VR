using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

namespace TVP
{
    public class FoikulController : MonoBehaviour
    {

        [SerializeField] private float _volume;

        public float Volume 
        { 
            get => _volume; 
            set
            {
                _volume = Mathf.Clamp01(value);
                transform.DOScale(_volume, 1);
            }
        
        
        }


        private void Awake()
        {
            _volume = transform.localScale.x ;
        }
        internal void Decrese()
        {
            float pressuare = (float)PompController.CurrentSimulation.Pressuare / 10000;
            print(pressuare);
            Volume -= pressuare;

        }

         

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}