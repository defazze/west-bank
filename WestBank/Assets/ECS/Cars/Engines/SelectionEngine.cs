using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SelectionEngine : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
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

        Entities.WithAll<SelectedComponent>().ForEach((ref Translation translation, ref Rotation rotation) =>
        {
            var scale = new float3(1.5f, 2.7f, 1);
            var matrix = Matrix4x4.TRS(translation.Value, rotation.Value, scale);

            var manager = CarGameManager.Instanse;
            Graphics.DrawMesh(manager.selectionMesh,
            matrix,
            manager.selectionMaterial, 0);
        });
    }
}