using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData
{
    public event Action<int> OnMoneyChanged;
    public event Action<int> OnBestScoreChanged;
    public event Action<int> OnBestStageChanged;
    public event Action<int> OnKnifeChanged;

    public KnifeConfig CurrentKnifeConfig { get; private set; }
    public IReadOnlyList<KnifeConfig> KnifeConfigs => _knifeConfigs;
    public int Money
    {
        get => _money;
        set
        {
            _money = Mathf.Max(value, 0);
            OnMoneyChanged?.Invoke(_money);
        }
    }
    public int BestScore
    {
        get => _bestScore;
        set
        {
            _bestScore = Mathf.Max(value, 0);
            OnBestScoreChanged?.Invoke(_bestScore);
        }
    }
    public int BestStage
    {
        get => _bestStage;
        set
        {
            _bestStage = Mathf.Max(value, 0);
            OnBestStageChanged?.Invoke(_bestStage);
        }
    }
    public int CurrentKnifeId
    {
        get => _currentKnifeId;
        set
        {
            if (_knivesUnlockedId.Contains(value) == false) return;

            _currentKnifeId = value;
            OnKnifeChanged?.Invoke(_currentKnifeId);

            CurrentKnifeConfig = GetKnifeConfigById(_currentKnifeId);
        }
    }
    public List<int> KnivesUnlockedId
    {
        get => _knivesUnlockedId;
        set
        {
            if (value == null) return;

            _knivesUnlockedId = value;
        }
    }

    private int _money = 0;

    private int _bestScore = 0;
    private int _bestStage = 0;

    private int _currentKnifeId = 0;
    private List<int> _knivesUnlockedId = new() { 0 };

    private readonly List<KnifeConfig> _knifeConfigs;

    public GameData(List<KnifeConfig> knifeConfigs) => _knifeConfigs = knifeConfigs;

    public void SetData(int money, int bestScore, int bestStage, int currentKnifeId, List<int> knivesUnlockedId)
    {
        Money = money;
        BestScore = bestScore;
        BestStage = bestStage;
        CurrentKnifeId = currentKnifeId;
        KnivesUnlockedId = knivesUnlockedId;
    }

    public KnifeConfig GetKnifeConfigById(int id) => _knifeConfigs.FirstOrDefault(c => c.Id == id);

    public bool IsKnifeUnlocked(int id) => _knivesUnlockedId.Contains(id);

    public bool TryUnlockedKnife(int id)
    {
        if (_knivesUnlockedId.Contains(id)) return false;

        _knivesUnlockedId.Add(id);
        return true;
    }
}