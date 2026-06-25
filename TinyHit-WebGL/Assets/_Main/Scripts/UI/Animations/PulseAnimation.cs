using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class PulseAnimation
{
    private readonly Graphic _graphic;
    private readonly float _pulseDuration;
    private readonly Ease _ease;

    private Tween _currentTween;

    public PulseAnimation(Graphic graphic, float pulseDuration, Ease ease)
    {
        _graphic = graphic;
        _pulseDuration = pulseDuration;
        _ease = ease;
    }

    public void StopAnimation()
    {
        if (_currentTween.isAlive == false) return;

        _currentTween.Stop();
    }

    public void AlphaPulse(float alpha)
    {
        _currentTween = Tween.Alpha(_graphic, alpha, _pulseDuration, _ease, 2, CycleMode.Yoyo)
            .OnComplete(() => AlphaPulse(alpha));
    }

    public void ColorPulse(Color color)
    {
        _currentTween = Tween.Color(_graphic, color, _pulseDuration, _ease, 2, CycleMode.Yoyo)
            .OnComplete(() => ColorPulse(color));
    }
}