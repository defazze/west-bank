using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class InputEngine : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAll<InputComponent>().WithNone<AcceleratorComponent>().ForEach((ref SpeedComponent speedComponent) =>
        {
            speedComponent.Speed = Input.GetAxis("Vertical");
        });

        Entities.WithAll<InputComponent>().ForEach((ref AcceleratorComponent acceleratorComponent) =>
        {
            acceleratorComponent.Boost = Math.Sign(Input.GetAxis("Vertical"));
        });
    }
}