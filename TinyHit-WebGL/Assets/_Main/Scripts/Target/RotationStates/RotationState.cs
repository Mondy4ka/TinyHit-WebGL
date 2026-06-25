using System;
using UnityEngine;

[Serializable]
public abstract class RotationState
{
    public float Duration;

    public virtual void Enter() { }
    public virtual void Update(Transform transform) { }
}