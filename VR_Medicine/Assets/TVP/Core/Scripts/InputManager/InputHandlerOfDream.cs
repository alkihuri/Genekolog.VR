using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TVP;
using UnityEngine;
using UnityEngine.XR;


namespace TVP
{
    public class InputHandlerOfDream : MonoSinglethon<InputHandlerOfDream>
    {
        public InputDevice _targetDevice;
        public List<InputDevice> devices;
        public Vector2 Primary2DAxis;
        public Vector2 Secondary2DAxis;
        public bool Triger;
        public float TrigerValue;

        [SerializeField] List<string> names = new List<string>();

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GetDevices());
        }

        IEnumerator GetDevices()
        {
            InputDeviceCharacteristics characteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
            devices = new List<InputDevice>();
            while (true)
            {
                yield return new WaitForSeconds(1);
                //InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Right, devices);
                InputDevices.GetDevicesWithCharacteristics(characteristics,devices);
                names = devices.Select(d => d.characteristics.ToString()).ToList();
            }

        }
        // Update is called once per frame
        void Update()
        {

            if (devices.Count == 0)
                return;

            foreach (var devcie in devices)
            { 
                devcie.TryGetFeatureValue(CommonUsages.primary2DAxis, out Primary2DAxis);
                devcie.TryGetFeatureValue(CommonUsages.secondary2DAxis, out Secondary2DAxis);
                devcie.TryGetFeatureValue(CommonUsages.triggerButton, out Triger);
                devcie.TryGetFeatureValue(CommonUsages.trigger, out TrigerValue);

            }
        }
    }
}