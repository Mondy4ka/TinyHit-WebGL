using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TargetService
{
    public Target Target { get; }

    private readonly List<StageData> _stageDatas;
    private readonly ScoreService _scoreService;
    private readonly ObjectPool<Knife> _knivesPool;
    private readonly ObjectPool<Apple> _applesPool;

    private readonly List<TargetConfig> _stageTargetsConfigs;
    private readonly List<TargetConfig> _bossesTargetsConfigs;

    public TargetService(Target target, ObjectPool<Knife> knivesPool, ObjectPool<Apple> applesPool, List<StageData> stageDatas, ScoreService scoreService)
    {
        Target = target;
        _knivesPool = knivesPool;
        _applesPool = applesPool;
        _stageDatas = stageDatas;
        _scoreService = scoreService;
    }

    public void Initialize() => Target.Initialize(_knivesPool, _applesPool);

    public Vector2 GetKnifePositionOnTarget() => new(0, Target.transform.position.y - Target.KnivesRadius);

    public void HitTarget(Knife knife)
    {
        Target.TargetHealth.TakeDamage(knife.Damage);
        knife.transform.SetParent(Target.ItemsParent);
        knife.MoveTo(GetKnifePositionOnTarget());
        knife.KnifeTrigger.SetStatic(true);
    }

    public void ReinitializeTarget()
    {
        TargetConfig config;

        if (_scoreService.Stage >= _stageDatas.Count)
        {
            if (_scoreService.Stage % 5 == 0)
                config = _bossesTargetsConfigs[UnityEngine.Random.Range(0, _bossesTargetsConfigs.Count)];
            else
                config = _stageTargetsConfigs[UnityEngine.Random.Range(0, _stageTargetsConfigs.Count)];
        }
        else
            config = _stageDatas[_scoreService.Stage - 1].TargetConfigs[UnityEngine.Random.Range(0, _stageDatas[_scoreService.Stage - 1].TargetConfigs.Length)];

        Target.SetStats(config);
        Target.TargetVisual.AppearanceAnimation();
        Target.SetRotateActive(true);
    }
}