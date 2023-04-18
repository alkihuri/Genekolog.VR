using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ICSI.Scripts
{
    [RequireComponent(typeof(MeshOutline))]
    public class MeshOutlineAnimator : MonoBehaviour
    {
        [SerializeField] private MeshOutline outline;
        [SerializeField] private MeshOutlineAnimationSettings settings;

        private float timer;

        private void Start()
        {
            //timer = settings.TimeToFade * Random.value;

            //StartCoroutine(Random.value < .5f ? FadeDown() : FadeUp());
            StartCoroutine(FadeDown());
        }

        private IEnumerator FadeDown()
        {
            while (timer < settings.TimeToFade)
            {
                yield return null;
                timer += Time.deltaTime;
                var curveValue = settings.ValueCurve.Evaluate(timer / settings.TimeToFade);

                var valueOutlineWidth = settings.TopValue - (curveValue * (settings.TopValue - settings.BotValue));
                outline.OutlineWidth = valueOutlineWidth;
            }

            timer = 0;
            StartCoroutine(FadeUp());
        }

        private IEnumerator FadeUp()
        {
            while (timer < settings.TimeToFade)
            {
                yield return null;
                timer += Time.deltaTime;
                var curveValue = settings.ValueCurve.Evaluate(timer / settings.TimeToFade);

                var valueOutlineWidth = settings.BotValue + (curveValue * (settings.TopValue - settings.BotValue));
                outline.OutlineWidth = valueOutlineWidth;
            }

            timer = 0;
            StartCoroutine(FadeDown());
        }
    }
}