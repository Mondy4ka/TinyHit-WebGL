using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetStatsConfig", menuName = "TargetStatsConfig")]
public class TargetConfig : ScriptableObject
{
    [Header("Visual Settings")]
    public Sprite Sprite;

    [Header("Health Stats")]
    public int MaxHealth;

    [Header("Static Knives Stats")]
    public List<int> KnivesAngel;

    [Header("Rotation Stats")]
    [SerializeReference] public List<RotationState> RotationSequence;

    [ContextMenu("AddAccelerationState")]
    public void AddAccelerationState() => RotationSequence.Add(new AccelerationState());

    [ContextMenu("AddRotateState")]
    public void AddRotateState() => RotationSequence.Add(new RotateState());
}