using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autohand
{
    public class KeyboardHand : MonoBehaviour
    {
        public Hand hand;
        public float speed = 1;
        public float flySpeed = 1;

        void Update()
        {
            float yMove = 0;

            if (Input.GetKey(KeyCode.Space))
                yMove = 1;

            if (Input.GetKey(KeyCode.LeftShift))
                yMove = -1;

            if (Input.GetKey(KeyCode.E))
                transform.Rotate(new Vector3(0,speed * 90 * Time.deltaTime, 0));

            if (Input.GetKey(KeyCode.Q))
                transform.Rotate(new Vector3(0,-speed * 90 * Time.deltaTime, 0));

            Vector3 move = transform.right * (Input.GetAxis("Vertical") * speed) +
                           transform.forward * (-Input.GetAxis("Horizontal") * speed) + Vector3.up *
                           (yMove * flySpeed);
            
            transform.position += move * Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Grab");
                hand.Grab();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Squeeze");
                hand.Squeeze();
            }

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Release");
                hand.Release();
            }
        }
    }
}