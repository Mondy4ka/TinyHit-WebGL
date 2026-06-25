using PrimeTween;
using System;
using UnityEngine;

[Serializable]
public class TargetVisual
{
    private readonly SpriteRenderer _spriteRenderer;
    private readonly float _damageScale;
    private readonly Vector2 _standartScale;

    private Tween _currentTween;

    public TargetVisual(SpriteRenderer spriteRenderer, float damageScale)
    {
        _spriteRenderer = spriteRenderer;
        _damageScale = damageScale;

        _standartScale = _spriteRenderer.transform.localScale;
    }

    public void SetSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

    public void AppearanceAnimation(Action onComplete = null)
    {
        StopAnimation();

        _spriteRenderer.transform.localScale = Vector2.zero;

        _currentTween = Tween.Scale(_spriteRenderer.transform, _standartScale, 0.5f)
            .OnComplete(onComplete);
    }

    public void DeathAnimation(Action onComplete = null)
    {
        StopAnimation();

        _currentTween = Tween.Scale(_spriteRenderer.transform, Vector2.zero, 0.25f)
            .OnComplete(onComplete);
    }

    public void DamageAnimation(Action onComplete = null)
    {
        StopAnimation();

        Vector2 damageScale = _standartScale * _damageScale;
        _currentTween = Tween.Scale(_spriteRenderer.transform, damageScale, 0.1f, Ease.OutCubic, 2, CycleMode.Yoyo)
            .OnComplete(onComplete);
    }

    public void StopAnimation()
    {
        _spriteRenderer.transform.localScale = _standartScale;

        if (_currentTween.isAlive == false) return;

        _currentTween.Stop();
    }
}