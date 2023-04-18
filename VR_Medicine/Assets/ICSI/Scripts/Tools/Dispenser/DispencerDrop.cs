using System;
using DG.Tweening;
using UnityEngine;

public class DispencerDrop : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private MeshRenderer defaultDrop;
    [SerializeField] private MeshRenderer tableDrop;
    private bool isFalling;

    public Collider Collider => collider;
    
    public void StartAnimation(float duration)
    {
        var defaultScale = transform.localScale.x;
        rigidbody.isKinematic = true;
        transform.localScale = Vector3.zero;
        transform.DOScale(defaultScale, duration).OnComplete(OnStartAnimationIsEnd);
    }

    private void OnStartAnimationIsEnd()
    {
        rigidbody.isKinematic = false;
        transform.SetParent(null);
        DOVirtual.DelayedCall(.1f, () => isFalling = true);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!isFalling) return;
        // TODO CHECK ЧАШКА ПЕТРИ
        Destroy(gameObject);
    }

    private void SetTableDrop()
    {
        defaultDrop.gameObject.SetActive(false);
        tableDrop.gameObject.SetActive(true);
    }
}