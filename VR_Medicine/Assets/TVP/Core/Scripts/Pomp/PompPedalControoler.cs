using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVP
{


    public class PompPedalControoler : MonoBehaviour
    {
        public bool IsLeft;
        private PompController _pompController;

        private void Awake()
        {
            _pompController = GetComponentInParent<PompController>();
        } 

        public void Decrease() => _pompController?.Decrese();
        public void Increse()
        {
             
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.PompPedalIsWorking_06_01_05);
            _pompController?.Increse();
        }
    }
}