using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KnifeConfig", menuName = "KnifeConfig")]
public class KnifeConfig : ScriptableObject
{
    public Sprite Sprite;
    public string KnifeName;
    public List<float> Chance;
    public List<int> Damage;
    public List<EffectConfig> Effects;
}
