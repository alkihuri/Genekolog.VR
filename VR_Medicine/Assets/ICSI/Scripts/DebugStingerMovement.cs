using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStingerMovement : MonoBehaviour
{
    [SerializeField] private MicroInstrumentsMoveController instrument;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            instrument.MoveStinger(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }

        if (Input.GetMouseButton(1))
        {
            instrument.MoveHolder(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }
    }
}