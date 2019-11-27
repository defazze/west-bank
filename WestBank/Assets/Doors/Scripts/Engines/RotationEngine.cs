using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Doors
{
    [UpdateAfter(typeof(DoorEngine))]
    public class RotationEngine : ComponentSystem
    {
        private ConfigData _config;

        protected override void OnUpdate()
        {
            _config = GetSingleton<ConfigData>();

            Entities.ForEach((ref Translation translation, ref Rotation rotation, ref RotationComponent rotationComponent) =>
            {
                var direction = rotationComponent.Opening ? -1 : 1;
                var delta = quaternion.RotateY(direction * _config.doorRotationSpeed * Time.deltaTime);

                quaternion newRotation = math.mul(rotation.Value, delta);
                var position = translation.Value;

                var pivot = rotationComponent.Pivot;
                var newPosition = math.mul(delta, position - pivot) + pivot;

                rotationComponent.NewPosition = newPosition;
                rotationComponent.NewRotation = newRotation;
            });
        }
    }
}