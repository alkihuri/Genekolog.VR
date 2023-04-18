using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TVP.UI
{
    public class TimerProgressUIController : MonoBehaviour
    {
        [SerializeField] Image _outer;
        [SerializeField] TMPro.TextMeshProUGUI _text;


        public void UpdateState(float a ,float b)
        {
            _outer.fillAmount = a / b;
            _text.text = a.ToString();
        }
        public void Reset()
        {
            UpdateState(1, 1);
            _text.text = "!";
        }
        #region Test

        [ContextMenu("Test")]
        public void Test()
        {
            //StartCoroutine(TestTime());
        }

        IEnumerator TestTime()
        {
            for(int x =0;x < 10;x++)
            {
                yield return new WaitForSeconds(1);
                UpdateState(x, 10);
            }
        }
        #endregion
    }
}