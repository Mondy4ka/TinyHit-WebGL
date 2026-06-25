using System;
using UnityEngine;

[Serializable]
public class RotateState : RotationState
{
    [Header("Rotate")]
    public float CurrentSpeed;

    public override void Update(Transform transform)
    {
        if (CurrentSpeed == 0) return;

        transform.Rotate(0, 0, CurrentSpeed * Time.deltaTime);
    }
}