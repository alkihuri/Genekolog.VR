using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVP
{
    public class TipsManager : MonoSinglethon<TipsManager>
    {

        SimDomenStateMachine _stateMachine;
        // Start is called before the first frame update
        void Start()
        {
            _stateMachine = SimDomenStateMachine.CurrentSimulation;

        }  


        public void DoTip()
        {

        }

    }
}