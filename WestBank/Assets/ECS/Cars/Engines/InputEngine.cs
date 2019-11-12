using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class InputEngine : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse click!");
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Entities.WithAll<SelectedComponent>().ForEach((Entity entity) =>
            {
                PostUpdateCommands.RemoveComponent(entity, typeof(SelectedComponent));
            });

            Entities.WithAll<SelectableComponent>().ForEach((Entity entity, ref Translation translation, ref NonUniformScale scale) =>
            {
                if (translation.Value.x - scale.Value.x / 2 < mousePosition.x
                && translation.Value.x + scale.Value.x / 2 > mousePosition.x
                && translation.Value.y - scale.Value.y / 2 < mousePosition.y
                && translation.Value.y + scale.Value.y / 2 > mousePosition.y)
                {
                    PostUpdateCommands.AddComponent<SelectedComponent>(entity);
                }
            });
        }

        Entities.ForEach((ref InputComponent inputComponent) =>
        {
            inputComponent.Vertical = Math.Sign(Input.GetAxis("Vertical"));
            inputComponent.Horizontal = Math.Sign(Input.GetAxis("Horizontal"));
        });
    }
}