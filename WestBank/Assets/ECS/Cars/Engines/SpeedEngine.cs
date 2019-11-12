using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SpeedEngine : ComponentSystem
{
    private float speedFactor = .5f;
    private float brakingFactor = .2f;
    private float maxSpeed = 2f;
    protected override void OnUpdate()
    {
        Entities.ForEach((ref InputComponent input, ref MovementComponent movement) =>
        {
            var newSpeed = movement.Speed + speedFactor * input.Vertical * Time.deltaTime;
            newSpeed = math.clamp(newSpeed, -1 * maxSpeed, maxSpeed);

            if (input.Vertical == 0)
            {
                newSpeed += brakingFactor * Time.deltaTime * (-1) * Math.Sign(newSpeed);
            }

            movement.Speed = newSpeed;
        });
    }
}