using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationListItem : MonoBehaviour
{
    [SerializeField] private Image check;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Color textDOColor;

    public void OnItemCheck()
    {
        check.gameObject.SetActive(true);
        check.color = new Color(check.color.r, check.color.g, check.color.b, 0);
        check.DOFade(1, .2f);

        text.DOColor(textDOColor, .2f);
        text.transform.DOScale(.99f, .2f);
    }
}