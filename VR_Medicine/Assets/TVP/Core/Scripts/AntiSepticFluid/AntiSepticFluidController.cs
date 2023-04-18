using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVP.View
{
    public class AntiSepticFluidController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<RagController>())
            {

            }

            if(other.gameObject.GetComponent<NiddleController>())
            { 
                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.PutNiddleAtAntisepticFluid_08_01_03);
                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.WashNiddle_08_01_04);
            }
        }
    }
}