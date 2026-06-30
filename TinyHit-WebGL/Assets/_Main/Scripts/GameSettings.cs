using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Stage Settings")]
    public List<StageData> Stages;

    [Header("Stats Settings")]
    public List<KnifeConfig> KnifeConfigs;
    public float KnifeThrowTime;

    [Header("Pools Settings")]
    [Header("Prefabs Settings")]
    public Knife KnifePrefab;
    public Apple ApplePrefab;

    [Header("Pools Size Settings")]
    public int ThrowKnifePoolSize;
    public int TargetKnifePoolSize;
    public int TargetApplePoolSize;
}