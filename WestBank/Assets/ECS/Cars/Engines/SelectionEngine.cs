using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SelectionEngine : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAll<SelectedComponent>().ForEach((ref Translation translation, ref Rotation rotation) =>
        {
            var position = translation.Value - new float3(0, 0, 1);
            var scale = new float3(1.5f, 2.7f, 1);
            var matrix = Matrix4x4.TRS(position, rotation.Value, scale);

            var manager = CarGameManager.Instanse;
            Graphics.DrawMesh(manager.selectionMesh,
            matrix,
            manager.selectionMaterial, 0);
        });
    }
}