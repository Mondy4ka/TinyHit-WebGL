using UnityEngine;

public abstract class TargetRotationType
{
    protected Transform _targetTransform;

    protected TargetRotationType(Transform targetTransform) => _targetTransform = targetTransform;

    public abstract void Update();
}