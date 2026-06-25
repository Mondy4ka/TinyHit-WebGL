using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas _canvas; 

    [Header("Text Settings")]
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _bestStageText;
    [SerializeField] private TMP_Text _startTipText;

    [Header("BlackScreen Settings")]
    [SerializeField] private Image _blackScreen;

    [Header("Chunks Settings")]
    [SerializeField] private RectTransform _chunk1;
    [SerializeField] private RectTransform _chunk2;

    private CurtainAnimation _animation;
    private BlackScreenAnimation _blackScreenAnimation;
    private PulseAnimation _pulseAnimation;
    private ScoreService _scoreService;

    private float _startTipAlpha;

    public void Initialize(UISettings settings, ScoreService scoreService)
    {
        _scoreService = scoreService;

        _pulseAnimation = new(
            _startTipText,
            settings.MenuScreenCloseDuration,
            settings.MenuScreenOpenEasing);

        _blackScreenAnimation = new(
            _blackScreen,
            1.0f,
            0.0f,
            settings.MenuScreenOpenDuration,
            settings.MenuScreenCloseDuration);

        _animation = new(
            _chunk1,
            _chunk2,
            settings.MenuScreenOpenPosition1,
            settings.MenuScreenClosePosition1,
            settings.MenuScreenOpenPosition2,
            settings.MenuScreenClosePosition2,
            settings.MenuScreenOpenDuration,
            settings.MenuScreenCloseDuration,
            settings.MenuScreenOpenEasing,
            settings.MenuScreenCloseEasing, 
            _blackScreenAnimation);

        _scoreService.OnBestScoreChanged += SetBestScore;
        _scoreService.OnBestStageChanged += SetBestStage;
    }

    public void SetCanvas(bool isCanvas) => _canvas.enabled = isCanvas;

    public void SetBestScore(int bestScore)
    {
        if (_bestScoreText == null) return;

        _bestScoreText.SetText($"BEST SCORE: {bestScore}");
    }

    public void SetBestStage(int bestStage)
    {
        if (_bestStageText == null) return;

        _bestStageText.SetText($"BEST STAGE: {bestStage}");
    }

    public void Open(Action onComplete = null) => _animation.Open(onComplete);

    public void Close(Action onComplete = null) => _animation.Close(onComplete);

    public void TipPulse() => _pulseAnimation.AlphaPulse(_startTipAlpha);

    public void StopTipPulse() => _pulseAnimation.StopAnimation();
}