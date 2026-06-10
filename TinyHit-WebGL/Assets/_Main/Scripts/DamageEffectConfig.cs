using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "DamageEffect")]
public class DamageEffectConfig : EffectConfig
{
    public List<int> Damage;
}
