using Common.DataManager;
using Common.UI;
using System.Collections;
using System.Collections.Generic;
using TVP.UI;
using UnityEngine;
using UnityEngine.Events;

namespace TVP.MockSim
{


    public class VirtualNiddleController : MonoBehaviour
    {
        [SerializeField] DeviceToVirtualNiddle _device;
        [SerializeField] bool _doesPenBloodPipe;

        public bool DoesPenBloodPipe { get => _doesPenBloodPipe; set => _doesPenBloodPipe = value; }


        private void Awake()
        {
            DoesPenBloodPipe = false;
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<FoikulController>())
            {
                var folikul = other.gameObject.GetComponent<FoikulController>();
                folikul.Decrese(); 
                _device.DoAspirate();
            }
        }

        private void OnTriggerExit(Collider other)
        {

            if (other.gameObject.GetComponent<FoikulController>())
                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.AspiratingIsDone_07_01_02);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (DoesPenBloodPipe)
                return;



            if (other.gameObject.GetComponent<BloodPipeMockController>())
            {
                DoesPenBloodPipe = true;
                UnityAction revert = () => { SimDomenStateMachine.CurrentSimulation.ResetCurrentScene(); DoesPenBloodPipe = false; };
                SimStateCanvas.CurrentSimulation.QuestionBoxController.Innit("Вы задели сосуды!", revert, null,"Еще раз", "Подолжить");
                SimulationDataManager.IncreaseError();
            }

        }
    }
}