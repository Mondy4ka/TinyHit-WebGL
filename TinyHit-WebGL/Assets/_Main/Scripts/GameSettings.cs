using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Targets Settings")]
    public List<TargetConfig> TargetConfigs;

    [Header("Knives Settings")]
    [Header("Pool Settings")]
    public Knife KnifePrefab;
    public int KnifePoolSize;

    [Header("Stats Settings")]
    public List<KnifeConfig> KnifeConfigs;
    public float KnifeThrowTime;
}
