using System;
using UnityEngine;

public class ScoreService
{
    public event Action<int> OnScoreChanged;
    public event Action<int> OnStageChanged;

    public int Score
    {
        get => _score;
        set
        {
            _score = Mathf.Max(value, 0);
            OnScoreChanged?.Invoke(_score);

            if (_score > _gameData.BestScore)
                _gameData.BestScore = _score;
        }
    }

    public int Stage
    {
        get => _stage;
        set
        {
            _stage = Mathf.Max(value, 0);
            OnStageChanged?.Invoke(_stage);

            if (_stage > _gameData.BestStage)
                _gameData.BestStage = _stage;
        }
    }

    private int _score = 0;
    private int _stage = 1;

    private readonly GameData _gameData;

    public ScoreService(GameData gameData) => _gameData = gameData;

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