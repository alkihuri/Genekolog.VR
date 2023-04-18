using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Linq;
using System.Runtime.InteropServices;
using System;

namespace TVP
{
    public class DisplayController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _textOnDisplay;
        [SerializeField] private List<string> _quey = new List<string>();

        public bool IsTyping { get; private set; }
        private void Awake()
        {
            IsTyping = false; 
            StartCoroutine(DelayedSetText(0.3f));
        }
        public void SetText(string text, float duration = 0)
        {
            if (!_textOnDisplay.gameObject.activeInHierarchy)
                return;

            Debug.Log("ВЫВЕДЕНО НА ДИСПЛЕЙ:" + text); 
                _quey.Add(text);

            _quey = _quey.Distinct().ToList();
        }


        private IEnumerator DelayedSetText(float duration)
        {
            while (true)
            {
                yield return new WaitUntil(() => _quey.Count > 0);
                yield return new WaitUntil(() => !IsTyping);
                IsTyping = true;
                _textOnDisplay.text = "";
                var msg = _quey[0];
                _quey.Remove(msg);
                foreach (char line in msg)
                {
                    yield return new WaitForSeconds(duration / msg.Length);
                    _textOnDisplay.text += line;

                }
                IsTyping = false; 
            }
        }
    }
}