using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestionPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text question;
    [SerializeField] private TMP_Text[] buttonAnswers;
    [SerializeField] private ContentSizeFitter[] contents;
    [SerializeField] private UnityEvent onClickTrueAnswerEvent;
    [SerializeField] private UnityEvent onClickFalseAnswerEvent;
    private uint trueAnserIndex;
    private Action onClickTrueAnswer;
    private Action onClickFalseAnswer;
    
    public void OnClickButton(int buttonIndex)
    {
        if (buttonIndex == trueAnserIndex)
        {
            onClickTrueAnswerEvent.Invoke();
            onClickTrueAnswer?.Invoke();
        }
        else
        {
            OnClickFalseButton(buttonAnswers[buttonIndex].transform);
            onClickFalseAnswerEvent.Invoke();
            onClickFalseAnswer?.Invoke();
        }
    }

    public void InitializeQuestion(QuestionData question, Action onClickTrueAnswer, Action onClickFalseAnswer)
    {
        ResetButtons();
        
        this.question.text = question.Question.Replace("\\n", "\n");;
        this.onClickTrueAnswer = onClickTrueAnswer;
        this.onClickFalseAnswer = onClickFalseAnswer;

        if (Random.value > 0.5f)
        {
            buttonAnswers[0].text = question.TrueAnswers.Replace("\\n", "\n");;
            trueAnserIndex = 0;
            buttonAnswers[1].text = question.FalseAnswers.Replace("\\n", "\n");;
        }
        else
        {
            buttonAnswers[1].text = question.TrueAnswers.Replace("\\n", "\n");;
            trueAnserIndex = 1;
            buttonAnswers[0].text = question.FalseAnswers.Replace("\\n", "\n");;
        }
        
        RefreshContentSize();
    }

    private void ResetButtons()
    {
        foreach (var tmpText in buttonAnswers)
        {
            var button = tmpText.GetComponentInParent<Button>();
            button.interactable = true;
            button.transform.localRotation = Quaternion.identity;
            button.image.color = Color.white;
            button.transform.localScale = Vector3.one;
        }
    }

    private void OnClickFalseButton(Transform buttonTransform)
    {
        var button = buttonTransform.GetComponentInParent<Button>();
        button.interactable = false;
        button.transform.DOShakeRotation(.5f, Vector3.forward * 15);
        button.image.DOColor(Color.gray, .75f);
        button.transform.DOScale(.9f, .5f);
    }

    private void RefreshContentSize()
    {
        IEnumerator Routine()
        {
            for (var i  = 0; i < 2;i++)
            {
                yield return null;
                foreach (var contentSizeFitter in contents)
                    contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

                yield return null;

                foreach (var contentSizeFitter in contents)
                    contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
        }

        StartCoroutine(Routine());
    }
}
[Serializable]
public class QuestionData
{
    public string Question;
    public string TrueAnswers;
    public string FalseAnswers;
}