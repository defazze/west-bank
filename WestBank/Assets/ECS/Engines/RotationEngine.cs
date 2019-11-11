using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RotationEngine : ComponentSystem
{
    private float rotationFactor = .5f;
    protected override void OnUpdate()
    {
        Entities.WithAll<RotationComponent>().ForEach((ref InputComponent input, ref Rotation rotation) =>
        {
            var angle = rotationFactor * input.Horizontal;
            quaternion newRotation = math.mul(rotation.Value, quaternion.RotateZ(-1 * angle * Time.deltaTime));

            rotation.Value = newRotation;
        });
    }
}