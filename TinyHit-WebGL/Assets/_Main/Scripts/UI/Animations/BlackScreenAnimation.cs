using PrimeTween;
using System;
using UnityEngine.UI;

public class BlackScreenAnimation
{
    private readonly Image _blackScreen;

    private readonly float _showAlpha;
    private readonly float _hideAlpha;

    private readonly float _showAnimationDuration;
    private readonly float _hideAnimationDuration;

    private Tween _currentTween;

    public BlackScreenAnimation(Image blackScreen, float showAlpha, float hideAlpha, float showAnimationDuration, float hideAnimationDuration)
    {
        _blackScreen = blackScreen;
        _showAlpha = showAlpha;
        _hideAlpha = hideAlpha;
        _showAnimationDuration = showAnimationDuration;
        _hideAnimationDuration = hideAnimationDuration;
    }

    public void StopAnimation()
    {
        if (_currentTween.isAlive == false) return;

        _currentTween.Stop();
    }

    public void ShowAnimation(Action onComplete = null)
    {
        StopAnimation();

        _blackScreen.color = new(0, 0, 0, _hideAlpha);

        _currentTween = Tween.Alpha(_blackScreen, _showAlpha, _showAnimationDuration)
            .OnComplete(onComplete);
    }

    public void HideAnimation(Action onComplete = null)
    {
        StopAnimation();

        _blackScreen.color = new(0, 0, 0, _showAlpha);

        _currentTween = Tween.Alpha(_blackScreen, _hideAlpha, _hideAnimationDuration)
            .OnComplete(onComplete);
    }
}