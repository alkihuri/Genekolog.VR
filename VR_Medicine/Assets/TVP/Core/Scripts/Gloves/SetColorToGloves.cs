using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace TVP
{
    public class SetColorToGloves : MonoBehaviour
    {
        [SerializeField] HandModel _right;
        [SerializeField] HandModel _left;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<HandModel>())
            {

                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.HandsIsProccesed_06_02_01);


                _right.SetGlovesColor();
                _left.SetGlovesColor();

                transform.DOShakeScale(0.5f).OnComplete(() => transform.DOScale(0, 0.5f).OnComplete(() => transform.DOScale(1, 0.5f)));

            }
        }
    }
}