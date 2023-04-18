using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaminarQuestionController : ActionInteractableObject
{
    [SerializeField] private QuestionData[] question;
    [SerializeField] private QuestionPanelController questionPanel;
    private int questionCount;
        
    public void StartQuestion()
    {
        questionPanel.InitializeQuestion(question[questionCount], OnClickTrueAnswer, OnClickFalseAnswer);
    }

    private void OnClickTrueAnswer()
    {
        InvokeEndAction();
        questionCount++;
    }

    private void OnClickFalseAnswer()
    {
        // TODO подсчет неправильных ответов
    }
}
