using System;

public abstract class Effect
{
    public event Action<Effect> OnEffectEnd;

    public EffectStats EffectStats { get; private set; }

    private int _hitCounter;

    protected Effect(EffectConfig config)
    {
        EffectStats = new(config);
    }

    public virtual void Update()
    {
        _hitCounter++;

        if (_hitCounter >= EffectStats.GetDurationHits())
        {
            OnEffectEnd?.Invoke(this);
            return;
        }
    }
}