using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class InputEngine : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAll<SelectedComponent>().ForEach((ref InputComponent inputComponent) =>
        {
            inputComponent.Vertical = Math.Sign(Input.GetAxis("Vertical"));
            inputComponent.Horizontal = Math.Sign(Input.GetAxis("Horizontal"));
        });
    }
}