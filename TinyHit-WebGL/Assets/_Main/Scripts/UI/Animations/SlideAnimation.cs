using PrimeTween;
using System;
using UnityEngine;

public class SlideAnimation
{
    private readonly Transform _ñhunk;

    private readonly Vector2 _openPosition;
    private readonly Vector2 _closePosition;

    private readonly float _openAnimationDuration;
    private readonly float _closeAnimationDuration;

    private readonly Ease _openEase;
    private readonly Ease _closeEase;

    private readonly BlackScreenAnimation _blackScreenAnimation;

    private Tween _currentTween;

    public SlideAnimation(Transform ñhunk, Vector2 openPosition, Vector2 closePosition, float openAnimationDuration, float closeAnimationDuration, Ease openEase = Ease.Default, Ease closeEase = Ease.Default, BlackScreenAnimation blackScreenAnimation = null)
    {
        _ñhunk = ñhunk;
        _openPosition = openPosition;
        _closePosition = closePosition;
        _openAnimationDuration = openAnimationDuration;
        _closeAnimationDuration = closeAnimationDuration;
        _openEase = openEase;
        _closeEase = closeEase;
        _blackScreenAnimation = blackScreenAnimation;
    }

    public void StopAnimation()
    {
        if (_currentTween.isAlive == false) return;

        _currentTween.Stop();
    }

    public void Open(Action onComplete = null)
    {
        StopAnimation();

        _ñhunk.localPosition = _closePosition;

        _blackScreenAnimation?.ShowAnimation();

        _currentTween = Tween.LocalPosition(_ñhunk, _openPosition, _openAnimationDuration, _openEase)
            .OnComplete(onComplete);
    }

    public void Close(Action onComplete = null)
    {
        StopAnimation();

        _ñhunk.localPosition = _openPosition;

        _blackScreenAnimation?.HideAnimation();

        _currentTween = Tween.LocalPosition(_ñhunk, _closePosition, _closeAnimationDuration, _closeEase)
            .OnComplete(onComplete);
    }
}