using System;
using System.Collections.Generic;

[Serializable]
public class EffectStats
{
    private int _levelDurationHits = 0;
    private List<int> _durationHits;

    public EffectStats(EffectConfig effectConfig)
    {
        _durationHits = effectConfig.DurationHits;
    }

    public int GetDurationHits() => _durationHits[_levelDurationHits];

    public void UpgradeDurationHits() => _levelDurationHits++;
}