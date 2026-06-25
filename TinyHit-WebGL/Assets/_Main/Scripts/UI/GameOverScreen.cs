using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas _canvas;

    [Header("Text Settings")]
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _currentStageText;

    [Header("Background Settings")]
    [SerializeField] private Image _background;

    [Header("GameOver Panel Settings")]
    [SerializeField] private RectTransform _panel;

    private SlideAnimation _animation;
    private BlackScreenAnimation _blackScreenAnimation;

    public void Initialize(UISettings settings)
    {
        _blackScreenAnimation = new(
            _background,
            settings.GameOverBackgroundAlpha,
            0,
            settings.GameOverOpenDuration,
            settings.GameOverCloseDuration);

        _animation = new(
            _panel,
            settings.GameOverOpenPosition,
            settings.GameOverClosePosition,
            settings.GameOverOpenDuration,
            settings.GameOverCloseDuration,
            settings.GameOverOpenEasing,
            settings.GameOverCloseEasing,
            _blackScreenAnimation);
    }

    public void SetCanvas(bool isCanvas) => _canvas.enabled = isCanvas;

    public void SetRunResults(int score, int stage)
    {
        if (_currentStageText == null || _currentStageText == null) return;

        _currentScoreText.SetText(score.ToString());
        _currentStageText.SetText(stage.ToString());
    }

    public void Close(Action onComplete = null) => _animation.Close(onComplete);

    public void Open(Action onComplete = null) => _animation.Open(onComplete);
}
