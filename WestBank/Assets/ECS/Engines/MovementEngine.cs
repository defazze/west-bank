using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class MovementEngine : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref SpeedComponent speedComponent) =>
        {
            float speed = speedComponent.Speed;
            translation.Value.y += Time.deltaTime * speed;
        });
    }
}