using System;
using UnityEngine;

[Serializable]
public class AccelerationState : RotationState
{
    [Header("Acceleration")]
    public float StartRotateSpeed;
    public float FinalRotateSpeed;

    private float _currentRotateSpeed;

    public override void Enter() => _currentRotateSpeed = StartRotateSpeed;

    public override void Update(Transform transform)
    {
        _currentRotateSpeed += Time.deltaTime * (FinalRotateSpeed - StartRotateSpeed) / Duration;

        transform.Rotate(0, 0, _currentRotateSpeed * Time.deltaTime);
    }
}