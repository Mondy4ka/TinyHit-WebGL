using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [Header("BlackScreen Settings")]
    [SerializeField] private Image _blackScreen;

    [Header("Text Settings")]
    [SerializeField] private TMP_Text[] _stageNameTexts;

    [Header("Chunks Settings")]
    [SerializeField] private RectTransform _chunk1;
    [SerializeField] private RectTransform _chunk2;

    private CurtainAnimation _animation;
    private BlackScreenAnimation _blackScreenAnimation;

    public void Initialize(UISettings settings)
    {
        _blackScreenAnimation = new(
            _blackScreen,
            0.0f,
            1.0f,
            settings.FadeScreenOpenDuration,
            settings.FadeScreenCloseDuration);

        _animation = new(
            _chunk1,
            _chunk2,
            settings.FadeScreenOpenPosition1,
            settings.FadeScreenClosePosition1,
            settings.FadeScreenOpenPosition2,
            settings.FadeScreenClosePosition2,
            settings.FadeScreenOpenDuration,
            settings.FadeScreenCloseDuration,
            settings.FadeScreenOpenEasing,
            settings.FadeScreenCloseEasing,
            _blackScreenAnimation);
    }

    public void SetText(string text)
    {
        if (_stageNameTexts.Length <= 0) return;

        for (int i = 0; i < _stageNameTexts.Length; i++)
            _stageNameTexts[i].SetText(text);
    }

    public void SetText(int stage)
    {
        if (_stageNameTexts.Length <= 0) return;

        for (int i = 0; i <  _stageNameTexts.Length; i++)
            _stageNameTexts[i].SetText($"STAGE {stage}");
    }

    public void Open(Action onComplete = null) => _animation.Open(onComplete);

    public void Close(Action onComplete = null) => _animation.Close(onComplete);
}