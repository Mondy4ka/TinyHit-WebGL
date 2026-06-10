using PrimeTween;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float Damage => _damage;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _throwTime;

    private float _damage;
    private List<Effect> _effects;

    private Target _target;
    private Vector2 _positionInTarget;
    private Tween _currentTween;

    private bool _isStatic;

    public void Initialize(Target target)
    {
        _target = target;

        CalculatePositionInTarget();
    }

    public void SetStats(float damage, List<Effect> effects)
    {
        _damage = damage;
        _effects = effects;
    }

    public void CalculatePositionInTarget() => _positionInTarget.y = _target.transform.position.y - _target.KnifeDepth;

    public void Throw() => _currentTween = Tween.Position(transform, _positionInTarget, _throwTime, Ease.Linear);

    public void BreakAnimation() => _currentTween.Stop();

    public void PlaceInTarget()
    {
        transform.parent = _target.transform;
        transform.position = _positionInTarget;

        _isStatic = true;
    }

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
            _target.OnKnifeHit(this);
            PlaceInTarget();
        }
    }
}