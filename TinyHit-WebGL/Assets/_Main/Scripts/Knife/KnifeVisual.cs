using PrimeTween;
using System;
using UnityEngine;

[Serializable]
public class KnifeVisual
{
    private readonly SpriteRenderer _spriteRenderer;
    private readonly Vector3 _standartScale;
    private readonly float _squeezeScale;

    private Tween _currentTween;

    public KnifeVisual(SpriteRenderer spriteRenderer, float squeezeScale)
    {
        _spriteRenderer = spriteRenderer;
        _squeezeScale = squeezeScale;

        _standartScale = _spriteRenderer.transform.localScale;
    }

    public void SetSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

    public void PulseAnimation()
    {
        StopAnimation();

        Vector3 newScale = _standartScale * _squeezeScale;

        _currentTween = Tween.Scale(_spriteRenderer.transform, newScale, 0.1f, Ease.Default, 2, CycleMode.Yoyo);
    }

    public void SqueezeAnimation(float squeezeTime)
    {
        StopAnimation();

        Vector3 newScale = _standartScale * _squeezeScale;

        _currentTween = Tween.ScaleX(_spriteRenderer.transform, newScale.x, squeezeTime)
            .OnComplete(() => Tween.Scale(_spriteRenderer.transform, _standartScale, 0.1f));
    }

    public void StopAnimation()
    {
        _spriteRenderer.transform.localScale = _standartScale;

        if (_currentTween.isAlive == false) return;

        _currentTween.Stop();
    }
}