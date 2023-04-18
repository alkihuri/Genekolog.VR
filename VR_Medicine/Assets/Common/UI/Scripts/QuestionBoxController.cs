using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Common.UI
{
    public class QuestionBoxController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] Button _positive;
        [SerializeField] Button _negative;

        public void Innit(string text, UnityAction positive, UnityAction negative, string postiveText = "Ok", string negativeText = "Отмена")
        {
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);

            _positive.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = postiveText;
            _negative.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = negativeText;


            _positive.onClick.RemoveAllListeners();
            _negative.onClick.RemoveAllListeners();
            _text.text = text;

            if (positive != null)
                _positive.onClick.AddListener(positive);
            if (negative != null)
                _negative.onClick.AddListener(negative);

            _positive.onClick.AddListener(HideThis);
            _negative.onClick.AddListener(HideThis);


            _negative.gameObject.SetActive(false);
            _positive.gameObject.SetActive(false);
            Invoke("ShowButtons", 2);
        }


        public void HideThis()
        {
            gameObject.SetActive(false);
        }

        public void ShowButtons()
        {
            _negative.gameObject.SetActive(true);
            _positive.gameObject.SetActive(true);
        }
    }
}