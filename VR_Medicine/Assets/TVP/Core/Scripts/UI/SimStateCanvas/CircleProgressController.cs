using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TVP.UI
{
    public class CircleProgressController : MonoBehaviour
    {

        [SerializeField] List<Sprite> _lightNums = new List<Sprite>();
        [SerializeField] List<Sprite> _darkNums = new List<Sprite>();

        [SerializeField] Image _numL;
        [SerializeField] Image _numD;
        [SerializeField] Image _done;
        // Start is called before the first frame update


        public void Innit(int x)
        {
            if (_lightNums.Count > x && _darkNums.Count > x)
            { 
                _numL.sprite = _lightNums[x];
                _numD.sprite = _darkNums[x];
                Dark();
            }
        }

        public void Light()
        {
            _done.gameObject.SetActive(false);
            _numL.gameObject.SetActive(true);
            _numD.gameObject.SetActive(false);
        }
        public void Dark()
        {
            _done.gameObject.SetActive(false);
            _numL.gameObject.SetActive(false);
            _numD.gameObject.SetActive(true);
        }
        public void Done()
        {
            _numL.gameObject.SetActive(false);
            _done.gameObject.SetActive(true);
        }
    }
}