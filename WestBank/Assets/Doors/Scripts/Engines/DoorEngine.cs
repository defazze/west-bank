using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(ShootEngine))]
public class DoorEngine : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithNone<RotationComponent>().ForEach((Entity e, ref DoorComponent door, ref Translation translation) =>
        {
            if (door.State == DoorState.MustOpen)
            {
                door.State = DoorState.Opening;
                PostUpdateCommands.AddComponent(e, new RotationComponent { Opening = true, Pivot = door.Pivot });
            }

            if (door.State == DoorState.Open)
            {
                door.OpenTime += Time.deltaTime;
                if (door.OpenTime >= GameManager.Instance.openPeriod)
                {
                    door.OpenTime = 0;
                    door.State = DoorState.Closing;

                    PostUpdateCommands.AddComponent(e, new RotationComponent { Opening = false, Pivot = door.Pivot });
                }
            }
        });

        Entities.WithAll<RotationComponent>().ForEach((Entity e, ref DoorComponent door, ref Rotation rotation, ref Translation translation, ref RotationComponent rotationComponent) =>
        {
            var angle = ((Quaternion)rotationComponent.NewRotation).eulerAngles.y;

            if (angle > 0 && angle < GameManager.Instance.maxOpenAngle)
            {
                if (door.State == DoorState.Opening)
                {
                    door.State = DoorState.Open;
                }

                if (door.State == DoorState.Closing)
                {
                    door.State = DoorState.Closed;
                }

                PostUpdateCommands.RemoveComponent<RotationComponent>(e);
            }
            else
            {
                rotation.Value = rotationComponent.NewRotation;
                translation.Value = rotationComponent.NewPosition;
            }
        });
    }
}