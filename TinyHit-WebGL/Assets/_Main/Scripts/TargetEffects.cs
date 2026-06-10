using System.Collections.Generic;

public class TargetEffects
{
    private List<Effect> _activeEffects = new();

    public void AddEffect(Effect newEffect)
    {
        if (_activeEffects.Contains(newEffect)) return;

        newEffect.OnEffectEnd += RemoveEffect;
        _activeEffects.Add(newEffect);
    }

    public void RemoveEffect(Effect effect)
    {
        if (_activeEffects.Contains(effect) == false) return;

        effect.OnEffectEnd -= RemoveEffect;
        _activeEffects.Remove(effect);
    }

    public void UpdateEffects()
    {
        foreach(var effect in _activeEffects)
            effect.Update();
    }
}
