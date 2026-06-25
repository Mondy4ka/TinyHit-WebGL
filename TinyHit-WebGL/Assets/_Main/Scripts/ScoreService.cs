using System;
using UnityEngine;

public class ScoreService
{
    public event Action<int> OnScoreChanged;
    public event Action<int> OnStageChanged;
    public event Action<int> OnBestScoreChanged;
    public event Action<int> OnBestStageChanged;

    public int Score
    {
        get => _score;
        set
        {
            _score = Mathf.Max(value, 0);
            OnScoreChanged?.Invoke(_score);

            if (_score > _bestScore)
            {
                _bestScore = _score;
                OnBestScoreChanged?.Invoke(_bestScore);
            }
        }
    }

    public int Stage
    {
        get => _stage;
        set
        {
            _stage = Mathf.Max(value, 0);
            OnStageChanged?.Invoke(_stage);

            if (_stage > _bestStage)
            {
                _bestStage = _stage;
                OnBestStageChanged?.Invoke(_stage);
            }
        }
    }

    private int _score = 0;
    private int _stage = 1;

    private int _bestScore;
    private int _bestStage;

    public void AddScore(int amount)
    {
        if (amount < 0) return;

        Score += amount;
    }

    public void NextStage() => Stage++;

    public void Reset()
    {
        Score = 0;
        Stage = 1;
    }
}

public enum StageType
{
    Stage,
    Boss
}