using UnityEngine;
using DG.Tweening;


namespace TVP
{
    public class DeviceToVirtualNiddle : MonoBehaviour
    {
        [SerializeField] UziScannerDeviceController _device;
        [SerializeField] BiopsyAdapterController _biopsyAdapter;
        [SerializeField] GameObject _niddleShape;
        [SerializeField] Transform _deviceTransdorm;
        [SerializeField] Transform _vaginaTrasform;
        [SerializeField] float _chairEulerYDelta;
        public float X_MAX = .2f;
        public float Z_MAX = .2f;
        public int SCALE = 1;
        private float x;
        private float y;
        private Rigidbody _rigidBody;

        private void Awake()
        {
            _device = GameObject.FindObjectOfType<UziScannerDeviceController>();
            _biopsyAdapter = GameObject.FindObjectOfType<BiopsyAdapterController>();
            _rigidBody = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            if (_device.IsStickedInVagina)
                SyncPos();

        }

        public void DoNiddle()
        { 
            _niddleShape.transform.DOLocalMoveZ(1f, 3);
        }
        public void StopNiddle()
        {
            _niddleShape.transform.DOLocalMoveZ(-1, 3);
        }

        private void RaycastMethod()
        {
            RaycastHit _nextPoint;
            if (Physics.Raycast(transform.position, transform.forward, out _nextPoint))
            {
                if (_nextPoint.transform.gameObject.GetComponent<PathPointController>())
                {
                    if (x > 0)
                    {
                        transform.DOMove(_nextPoint.transform.position, 1);
                        transform.DORotate(_nextPoint.transform.eulerAngles, 1);
                    }
                }
            }

            RaycastHit _prevPoint;
            if (Physics.Raycast(transform.position, transform.forward * -1, out _prevPoint))
            {
                if (_prevPoint.transform.gameObject.GetComponent<PathPointController>())
                {
                    if (x < 0)
                    {
                        transform.DOMove(_prevPoint.transform.position, 1);
                        transform.DORotate(_prevPoint.transform.eulerAngles, 1);
                    }
                }
            }
        }

        public void SyncPos()
        {

            _niddleShape.SetActive(_biopsyAdapter.IsNiddleMounted);



            x = transform.localEulerAngles.x + Input.GetAxis("Vertical") * 3;
            y = transform.localEulerAngles.y + Input.GetAxis("Horizontal") * 3;


            transform.DORotate(new Vector3(x, y, 0), 1);

            _niddleShape.transform.DOLocalMoveZ(InputHandlerOfDream.CurrentSimulation.Primary2DAxis.y, 1);


        }



        public void DoAspirate()
        {

            _biopsyAdapter.OnAspirate?.Invoke(10); 
        }

    }
}
