using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TargetRotate
{
    private readonly Transform _targetTransform;

    private List<RotationState> _sequence;

    private RotationState _currentState;
    private int _currentStateIndex;
    private float _timer;

    public TargetRotate(Transform targetTransform) => _targetTransform = targetTransform;

    public void SetRotationSequence(List<RotationState> sequence)
    {
        _sequence = sequence;

        _currentStateIndex = 0;
        _currentState = _sequence[_currentStateIndex];
        _currentState.Enter();
        _timer = 0;
    }

    public void Update()
    {
        if (_sequence.Count <= 0) return;

        _timer += Time.deltaTime;
        _currentState.Update(_targetTransform);

        if (_timer < _currentState.Duration) return;

        _timer = 0;
        NextState();
    }

    private void NextState()
    {
        _currentStateIndex = (_currentStateIndex + 1) % _sequence.Count;
        _currentState = _sequence[_currentStateIndex];
        _currentState.Enter();
    }
}