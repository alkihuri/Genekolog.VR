using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace TVP
{


    public  abstract class FluidBase : MonoBehaviour
    {
        public float Volume { get; set;}

        public float MaxVolume { get; set; }
    }
}