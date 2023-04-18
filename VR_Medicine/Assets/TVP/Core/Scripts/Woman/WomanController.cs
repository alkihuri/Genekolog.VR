using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVP
{
    public class WomanController : InteractableBase
    {

        private bool _isProccesed;
        [SerializeField] GameObject _placePoint;

        public bool IsProccesed 
        { 
            get => _isProccesed;
            set
            {
                _isProccesed = value; 
                _placePoint.SetActive(IsProccesed); 
                if(_isProccesed)
                {

                    SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.VaginaIsProccesed_06_02_02);
                    SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsDone_06_02);
                    SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.lastProccesVagina_08_03_01);
                }
            }
        }

        private void Awake()
        {
            IsProccesed = false;
        }

        public override void Interact(InteractableBase interactor)
        {
            throw new System.NotImplementedException();
        }

        public override void StopInteract(InteractableBase interactor)
        {
            throw new System.NotImplementedException();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<RagController>())
            {
                IsProccesed= true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            return;

            if (other.gameObject.GetComponent<RagController>())
            { 
                other.gameObject.GetComponent<RagController>().RestorePosition();
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
            }
        }
    }
}