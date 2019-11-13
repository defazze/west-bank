using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class RotationDoorEngine : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref Rotation rotation, ref RotationComponent rotationComponent) =>
        {
            if (rotationComponent.Opening)
            {
                Rotate(ref rotation, ref rotationComponent, true);
            }

            if (rotationComponent.Closing)
            {
                Rotate(ref rotation, ref rotationComponent, false);
            }
        });
    }

    private void Rotate(ref Rotation rotation, ref RotationComponent rotationComponent, bool isOpening)
    {
        var direction = isOpening ? -1 : 1;
        quaternion newRotation = math.mul(rotation.Value, quaternion.RotateY(direction * GameManager.Instance.doorRotationSpeed * Time.deltaTime));
        var angle = ((Quaternion)newRotation).eulerAngles.y;

        if (angle > 0 && angle < GameManager.Instance.maxOpenAngle)
        {
            rotationComponent.Opening = !isOpening;
            rotationComponent.Closing = isOpening;
        }
        else
        {
            rotation.Value = newRotation;
        }
    }
}