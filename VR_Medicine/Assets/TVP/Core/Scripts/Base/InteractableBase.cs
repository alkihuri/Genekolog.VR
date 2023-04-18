using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVP
{
    public abstract class InteractableBase : MonoBehaviour
    {
        public abstract void Interact(InteractableBase interactor);
        public abstract void StopInteract(InteractableBase interactor);

    }
}