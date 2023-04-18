using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanelController : ActionInteractableObject
{
    [SerializeField] private TMP_Text information;
    [SerializeField] private Button startButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private ContentSizeFitter[] contents;

    public void ResetPanel()
    {
        startButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
    }

    public void SetInformation(string text)
    {
        gameObject.SetActive(true);
        information.text = text.Replace("\\n", "\n");
        RefreshContentSize();
    }

    public void OnClickStartButton()
    {
        startButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        InvokeEndAction();
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