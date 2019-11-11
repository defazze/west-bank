using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class MovementEngine : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref Rotation rotation, ref MovementComponent movement) =>
        {
            float3 forwardVector = math.mul(rotation.Value, new float3(0, 1, 0));
            translation.Value += forwardVector * Time.deltaTime * movement.Speed;
        });
    }
}