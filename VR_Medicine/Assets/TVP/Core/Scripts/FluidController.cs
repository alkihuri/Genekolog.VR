using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TVP
{
    public class FluidController : FluidBase
    {

        [SerializeField] GameObject _shape;
        [SerializeField] float _startVolume;

        private void Start()
        {
            MaxVolume = 100;
            Volume = _startVolume;
        }

        private void Update()
        {
            var lerped = Mathf.Lerp(0, 1, Volume / MaxVolume);
            var scale = Mathf.Clamp(lerped, 0, 1);
            View(scale);

        }

        private void View(float scale)
        {
            if (_shape != null)
                _shape.transform.DOScaleY(scale, 0.1f);
        }
    }
}