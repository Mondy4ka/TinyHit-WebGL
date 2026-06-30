using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetStatsConfig", menuName = "TargetStatsConfig")]
public class TargetConfig : ScriptableObject
{
    public Sprite Sprite;
    public int MaxHealth;
    public List<int> KnivesAngel;
    public List<int> ApplesAngel;
    [SerializeReference] public List<RotationState> RotationSequence;

    [ContextMenu("AddAccelerationState")]
    public void AddAccelerationState() => RotationSequence.Add(new AccelerationState());

    [ContextMenu("AddRotateState")]
    public void AddRotateState() => RotationSequence.Add(new RotateState());
}