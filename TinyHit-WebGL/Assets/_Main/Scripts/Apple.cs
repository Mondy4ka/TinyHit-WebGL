using PrimeTween;
using UnityEngine;

public class Apple : MonoBehaviour, IResetable
{
    public int Worth => _worth;

    [SerializeField] private int _worth;

    private Sequence _currentSequence;

    public void PickUp()
    {
        StopAnimation();

        _currentSequence = Sequence.Create().Group(Tween.Scale(transform, Vector3.zero, 0.1f, Ease.InBack))
            .OnComplete(() => gameObject.SetActive(false));
    }

    public void StopAnimation()
    {
        if (_currentSequence.isAlive == false) return;

        _currentSequence.Stop();
    }

    public void ResetItem()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector2.one;
    }
}