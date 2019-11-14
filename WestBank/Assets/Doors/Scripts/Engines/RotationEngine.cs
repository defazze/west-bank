using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Doors
{
    public class RotationEngine : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Translation translation, ref Rotation rotation, ref RotationComponent rotationComponent) =>
            {
                var direction = rotationComponent.Opening ? -1 : 1;
                var delta = quaternion.RotateY(direction * GameManager.Instance.doorRotationSpeed * Time.deltaTime);

                quaternion newRotation = math.mul(rotation.Value, delta);
                var position = translation.Value;

                var pivot = rotationComponent.Pivot;
                var newPosition = math.mul(delta, position - pivot) + pivot;

                translation.Value = newPosition;
                rotation.Value = newRotation;

            });
        }
    }
}