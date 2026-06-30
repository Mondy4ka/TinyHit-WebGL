using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    [SerializeField] private TMP_Text _stageNameText;
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private TMP_Text _targetHealthText;
    [SerializeField] private Image _targetHealthFiller;

    private ScoreService _scoreService;
    private Target _target;
    private Tween _currentTween;
    
    public void Initialize(ScoreService scoreService, Target target)
    {
        _scoreService = scoreService;
        _target = target;

        _target.TargetHealth.OnHealthChanged += UpdateTargetHealthUI;
        _scoreService.OnScoreChanged += UpdateScoreUI;
    }

    public void Deinitialize()
    {
        _target.TargetHealth.OnHealthChanged -= UpdateTargetHealthUI;
        _scoreService.OnScoreChanged -= UpdateScoreUI;
    }

    public void SetActiveCanvas(bool isActive) => _canvas.enabled = isActive;

    public void UpdateScoreUI(int score)
    {
        if (_scoreText == null) return;

        _scoreText.SetText(score.ToString());
    }

    public void UpdateStageNameUI(int stage)
    {
        if (_stageNameText == null) return;

        _stageNameText.SetText($"Stage {stage}");
    }

    public void UpdateStageNameUI(string stageName)
    {
        if (_stageNameText == null) return;

        _stageNameText.SetText(stageName);
    }

    public void UpdateTargetHealthUI(float currentHealth, float maxHealth)
    {
        if (_targetHealthFiller == null || _targetHealthText == null) return;

        _targetHealthText.SetText($"{currentHealth} / {maxHealth}");

        if (_currentTween.isAlive)
            _currentTween.Stop();

        _currentTween = Tween.UIFillAmount(_targetHealthFiller, currentHealth / maxHealth, 0.1f);
    }

    private void OnDestroy() => Deinitialize();
}