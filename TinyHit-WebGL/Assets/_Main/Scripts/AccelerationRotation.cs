using System;
using UnityEngine;

[Serializable]
public class AccelerationRotation : TargetRotationType
{
    private float _maxRotateSpeed;
    private float _accelerationTime;
    private float _decelerationTime;
    private float _maxSpeedTime;
    private float _timeout;

    private float _currentSpeed;
    private float _timer;
    private AccelerationState _state = AccelerationState.Timeout;

    public AccelerationRotation(Transform targetTransform) : base(targetTransform) { }

    public void SetAccelerationStats(float maxRotateSpeed, float accelerationTime, float decelerationTime, float maxSpeedTime, float timeout)
    {
        _maxRotateSpeed = maxRotateSpeed;
        _accelerationTime = accelerationTime;
        _decelerationTime = decelerationTime;
        _maxSpeedTime = maxSpeedTime;
        _timeout = timeout;
    }

    public override void Update()
    {
        switch (_state)
        {
            case AccelerationState.Timeout:
                TimeoutUpdate();
                break;
            case AccelerationState.Acceleration:
                AccelerationUpdate();
                break;
            case AccelerationState.MaxSpeed:
                MaxSpeedUpdate();
                break;
            case AccelerationState.Deceleration:
                DecelerationUpdate();
                break;
        }

        _targetTransform.Rotate(0, 0, _currentSpeed * Time.deltaTime);
    }

    private void DecelerationUpdate()
    {
        if (_currentSpeed <= 0)
        {
            _currentSpeed = 0;
            _state = AccelerationState.Timeout;
            return;
        }

        _currentSpeed -= Time.deltaTime * (_maxRotateSpeed / _decelerationTime);
    }

    private void MaxSpeedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer < _maxSpeedTime) return;

        _timer = 0;
        _state = AccelerationState.Deceleration;
    }

    private void AccelerationUpdate()
    {
        if (_currentSpeed >= _maxRotateSpeed)
        {
            _currentSpeed = _maxRotateSpeed;
            _state = AccelerationState.MaxSpeed;
            return;
        }

        _currentSpeed += Time.deltaTime * (_maxRotateSpeed / _accelerationTime);
    }

    private void TimeoutUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer < _timeout) return;

        _timer = 0;
        _state = AccelerationState.Acceleration;
    }
}

public enum AccelerationState
{
    Timeout,
    Acceleration,
    MaxSpeed,
    Deceleration
}