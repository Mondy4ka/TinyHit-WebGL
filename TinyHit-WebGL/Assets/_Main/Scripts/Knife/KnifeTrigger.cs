using System;
using UnityEngine;

public class KnifeTrigger : MonoBehaviour
{
    public event Action OnHitFail;

    [SerializeField] private Collider2D _staticTrigger;
    [SerializeField] private Collider2D _throwTrigger;

    private bool _isStatic;

    public void SetStatic(bool isStatic)
    {
        _isStatic = isStatic;

        SetStaticTriggerActive(isStatic);
        SetThrowTriggerActive(!isStatic);

        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void SetStaticTriggerActive(bool isActive) => _staticTrigger.enabled = isActive;

    public void SetThrowTriggerActive(bool isActive) => _throwTrigger.enabled = isActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isStatic) return;

        if (collision.CompareTag("Knife"))
            OnHitFail?.Invoke();
    }
}