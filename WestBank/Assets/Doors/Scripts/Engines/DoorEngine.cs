using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(ShootEngine))]
public class DoorEngine : ComponentSystem
{
    private ConfigData _config;

    protected override void OnUpdate()
    {
        _config = GetSingleton<ConfigData>();

        Entities.WithNone<RotationComponent>().ForEach((Entity e, ref DoorComponent door, ref Translation translation) =>
        {
            if (door.state == DoorState.MustOpen)
            {
                door.state = DoorState.Opening;
                PostUpdateCommands.AddComponent(e, new RotationComponent { Opening = true, Pivot = door.pivot });
                PostUpdateCommands.AddComponent<CreatePerson>(e);
            }

            if (door.state == DoorState.Open)
            {
                door.openTime += Time.deltaTime;
                if (door.openTime >= _config.openPeriod)
                {
                    door.openTime = 0;
                    door.state = DoorState.Closing;

                    PostUpdateCommands.AddComponent(e, new RotationComponent { Opening = false, Pivot = door.pivot });
                }
            }
        });

        Entities.WithAll<RotationComponent>().ForEach((Entity e, ref DoorComponent door, ref Rotation rotation, ref Translation translation, ref RotationComponent rotationComponent) =>
        {
            var angle = ((Quaternion)rotationComponent.NewRotation).eulerAngles.y;

            if (angle > 0 && angle < _config.maxOpenAngle)
            {
                if (door.state == DoorState.Opening)
                {
                    door.state = DoorState.Open;
                }

                if (door.state == DoorState.Closing)
                {
                    door.state = DoorState.Closed;

                    if (EntityManager.Exists(door.person))
                    {
                        PostUpdateCommands.AddComponent<DestroyPerson>(door.person);
                    }
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