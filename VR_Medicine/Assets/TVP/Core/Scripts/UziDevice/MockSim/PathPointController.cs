using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TVP
{
    public class PathPointController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<DeviceToVirtualNiddle>())
            {
                var niddle = other.gameObject.GetComponent<DeviceToVirtualNiddle>();
                //niddle.gameObject.GetComponent<Rigidbody>().DOMove(transform.position,3);

                //niddle.gameObject.GetComponent<Rigidbody>().DORotate(transform.eulerAngles, 3);
            }
        }
    }
}