using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVP
{
    public class NiddleSocketController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var entered = other.gameObject; 
            if (entered.GetComponent<NiddleController>()) 
            {
                entered.GetComponent<NiddleController>().IsMounted = true; 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var entered = other.gameObject;

            if (entered.GetComponent<NiddleController>())
            { 
                entered.GetComponent<NiddleController>().IsMounted = false;  
            }
        }

    }
}