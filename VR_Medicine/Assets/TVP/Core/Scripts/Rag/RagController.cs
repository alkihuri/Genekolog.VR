using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVP
{
    public class RagController : InteractableBase
    {
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private void Awake()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }
        public void RestorePosition()
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }
        public override void Interact(InteractableBase interactor)
        {  
        }

        public override void StopInteract(InteractableBase interactor)
        {  
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<InteractableBase>())
            {
                var woman = other.gameObject.GetComponent<InteractableBase>();
                StopInteract(woman);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<InteractableBase>())
            {
                var woman = other.gameObject.GetComponent<InteractableBase>();
                Interact(woman);
            }
        }

    }
}