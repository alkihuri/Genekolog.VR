using Autohand;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TrayController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private UnityEvent onGrab;
    private Tween ResetRotation;
    private Grabbable trayGrabbable;
    private int countHandGrab;
    
    private void Start()
    {
        trayGrabbable = GetComponent<Grabbable>();
        trayGrabbable.OnGrabEvent += OnHandGrab;
        trayGrabbable.OnReleaseEvent += OnHandRelease;
    }

    private void OnHandGrab(Hand hand, Grabbable grabbable)
    {
        countHandGrab++;
        if (countHandGrab > 1) return;

        onGrab?.Invoke();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        var rotateValue = transform.rotation.eulerAngles;
        transform.DORotate(Vector3.up * rotateValue.y, 0.2f);
    }

    private void OnHandRelease(Hand hand, Grabbable grabbable)
    {
        countHandGrab--;
        if (countHandGrab > 0) return;
        
        rigidbody.constraints = RigidbodyConstraints.None;
        
    }
}