using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class DoorEngine : ComponentSystem
{
    private const float OPEN_PERIOD = 2;

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
                if (door.OpenTime >= OPEN_PERIOD)
                {
                    door.OpenTime = 0;
                    door.State = DoorState.Closing;
                    PostUpdateCommands.AddComponent(e, new RotationComponent { Opening = false, Pivot = door.Pivot });
                }
            }
        });

        Entities.WithAll<RotationComponent>().ForEach((Entity e, ref DoorComponent door, ref Rotation rotation) =>
        {
            var angle = ((Quaternion)rotation.Value).eulerAngles.y;

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
        });
    }
}