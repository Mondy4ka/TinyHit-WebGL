using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public TargetHealth TargetHealth { get; private set; }
    public TargetEffects TargetEffects { get; private set; }
    public TargetRotationType TargetRotationType { get; private set; }
    public float KnifeDepth => _knifeDepth;

    [SerializeField] private float _knifeDepth;

    private List<Knife> _hitKnifes = new();

    public void Initialize()
    {
        TargetHealth = new();
        TargetEffects = new();
    }

    public void SetStats(float maxHealth, TargetRotationType targetRotationType)
    {
        TargetHealth.SetHealthStats(maxHealth);
        TargetRotationType = targetRotationType;
    }

    private void Update()
    {
        TargetRotationType?.Update();
    }

    public void OnKnifeHit(Knife knife)
    {
        _hitKnifes.Add(knife);

        TargetEffects?.UpdateEffects();
        TargetHealth?.TakeDamage(knife.Damage);
    }
}
