using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace TVP
{


    public class PompUIController : MonoSinglethon<PompUIController>
    {

        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] Image _image;
        [SerializeField] private int _pressuare;
        [SerializeField] GameObject _button;

        private void Start()
        {
            _pressuare = 0;
        }
        public void UpdatePressuareUI(PompController device)
        {
            if (!device.IsActive)
            {
                _text.text = "Включите помпу";
                return;
            }


            _pressuare = device.Pressuare;  
            _text.text = "Давление помпы : " + _pressuare + "мм. рт. ст.";
            var imageFilling = Mathf.InverseLerp(device.MIN_PREASSURE, device.MAX_PRESURE, _pressuare); ;
            _image.fillAmount = imageFilling;
        }



        internal void UpdatePompStateUI(PompController device)
        {
            _button.GetComponent<Renderer>().material.color = !device.IsActive ? Color.red : Color.green;
            UpdatePressuareUI(device);
        }

    }
}