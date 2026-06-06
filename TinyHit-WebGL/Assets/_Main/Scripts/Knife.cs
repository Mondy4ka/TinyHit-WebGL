using PrimeTween;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _throwTime;
    [SerializeField] private float _throwDistanceY;

    private Target _target;
    private Vector2 _positionInTarget;
    private Tween _currentTween;

    private bool _isStatic;

    public void Initialize(Target target)
    {
        _target = target;

        CalculatePositionInTarget();
    }

    public void CalculatePositionInTarget() => _positionInTarget = -_target.transform.position.normalized / _target.KnifeDepth;

    public void PlaceInTarget()
    {
        transform.parent = _target.transform;
        transform.position = _positionInTarget;
    }

    public void Throw() => _currentTween = Tween.LocalPositionY(transform, transform.position.y + _throwDistanceY, _throwTime, Ease.InBack);

    public void BreakAnimation() => _currentTween.Stop();

    public void MoveTo(Vector3 newPosition) => transform.position = newPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isStatic) return;

        BreakAnimation();

        if (collision.CompareTag("Knife"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Target"))
        {
            PlaceInTarget();
            _isStatic = true;
        }
    }
}
