using UnityEngine;

public class ConstantSpeedRotation : TargetRotationType
{
    private float _rotateSpeed;

    public ConstantSpeedRotation(Transform targetTransform) : base(targetTransform) { }

    public void SetRotateSpeed(float rotateSpeed)
    {
        _rotateSpeed = rotateSpeed;
    }

    public override void Update()
    {
        _targetTransform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
    }
}
