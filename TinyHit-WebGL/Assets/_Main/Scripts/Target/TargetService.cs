using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TargetService
{
    public Target Target { get; }

    private readonly List<TargetConfig> _statsConfigs;
    private readonly ObjectPool<Knife> _staticKnivesPool;

    public TargetService(Target target, List<TargetConfig> statsConfigs, Knife knifePrefab)
    {
        Target = target;
        _statsConfigs = statsConfigs;

        _staticKnivesPool = new(knifePrefab, null);
        _staticKnivesPool.Initialize(5);
    }

    public void Initialize() => Target.Initialize(_staticKnivesPool);

    public Vector2 GetKnifePositionOnTarget() => new(0, Target.transform.position.y - 1 / Target.KnifeDepth);

    public void HitTarget(Knife knife)
    {
        Target.TargetHealth.TakeDamage(knife.Damage);
        knife.transform.SetParent(Target.KnivesParent);
        knife.MoveTo(GetKnifePositionOnTarget());
        knife.KnifeTrigger.SetStatic(true);
    }

    public void ReinitializeTarget()
    {
        Target.SetStats(_statsConfigs[UnityEngine.Random.Range(0, _statsConfigs.Count)]);
        Target.TargetVisual.AppearanceAnimation();
        Target.SetRotateActive(true);
    }
}