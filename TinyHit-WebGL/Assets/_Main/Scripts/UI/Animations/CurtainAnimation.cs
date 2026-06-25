using PrimeTween;
using System;
using UnityEngine;

public class CurtainAnimation
{
    private readonly Transform _chunk1;
    private readonly Transform _chunk2;

    private readonly Vector2 _openPosition1;
    private readonly Vector2 _closePosition1;

    private readonly Vector2 _openPosition2;
    private readonly Vector2 _closePosition2;

    private readonly float _openAnimationDuration;
    private readonly float _closeAnimationDuration;

    private readonly Ease _openEase;
    private readonly Ease _closeEase;

    private readonly BlackScreenAnimation _blackScreenAnimation;

    private Sequence _currentSequence;

    public CurtainAnimation(Transform chunk1, Transform chunk2, Vector2 openPosition1, Vector2 closePosition1, Vector2 openPosition2, Vector2 closePosition2, float openAnimationDuration, float closeAnimationDuration, Ease openEase = Ease.Default, Ease closeEase = Ease.Default, BlackScreenAnimation blackScreenAnimation = null)
    {
        _chunk1 = chunk1;
        _chunk2 = chunk2;
        _openPosition1 = openPosition1;
        _closePosition1 = closePosition1;
        _openPosition2 = openPosition2;
        _closePosition2 = closePosition2;
        _openAnimationDuration = openAnimationDuration;
        _closeAnimationDuration = closeAnimationDuration;
        _openEase = openEase;
        _closeEase = closeEase;
        _blackScreenAnimation = blackScreenAnimation;
    }

    public void StopAnimation()
    {
        if (_currentSequence.isAlive == false) return;

        _currentSequence.Stop();
    }

    public void Open(Action onComplete = null)
    {
        StopAnimation();

        _chunk1.localPosition = _closePosition1;
        _chunk2.localPosition = _closePosition2;

        _blackScreenAnimation?.ShowAnimation();

        _currentSequence = Sequence.Create()
            .Group(Tween.LocalPosition(_chunk1, _openPosition1, _openAnimationDuration, _openEase))
            .Group(Tween.LocalPosition(_chunk2, _openPosition2, _openAnimationDuration, _openEase))
            .OnComplete(onComplete);
    }

    public void Close(Action onComplete = null)
    {
        StopAnimation();

        _chunk1.localPosition = _openPosition1;
        _chunk2.localPosition = _openPosition2;

        _blackScreenAnimation?.HideAnimation();

        _currentSequence = Sequence.Create()
            .Group(Tween.LocalPosition(_chunk1, _closePosition1, _closeAnimationDuration, _closeEase))
            .Group(Tween.LocalPosition(_chunk2, _closePosition2, _closeAnimationDuration, _closeEase))
            .OnComplete(onComplete);
    }
}