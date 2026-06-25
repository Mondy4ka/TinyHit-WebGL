using System;
using UnityEngine;

[Serializable]
public class TargetHealth
{
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth <= 0)
                OnDeath?.Invoke();
        }
    }

    private float _maxHealth;
    private float _currentHealth;

    public void SetHealthStats(float maxHealth)
    {
        if (maxHealth <= 0) return;

        _maxHealth = maxHealth;
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0) return;

        CurrentHealth -= damage;
    }
}
