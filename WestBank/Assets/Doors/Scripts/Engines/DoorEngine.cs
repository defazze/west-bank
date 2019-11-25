using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(ShootEngine))]
public class DoorEngine : ComponentSystem
{
    private Configuration _config;
    private Entity _personEntity;
    protected override void OnCreate()
    {
        _config = GameManager.Instance.configuration;
        _personEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(_config.prefabs.regular, World.Active);
    }

    protected override void OnUpdate()
    {
        Entities.WithNone<RotationComponent>().ForEach((Entity e, ref DoorComponent door, ref Translation translation) =>
        {
            if (door.State == DoorState.MustOpen)
            {
                door.State = DoorState.Opening;
                PostUpdateCommands.AddComponent(e, new RotationComponent { Opening = true, Pivot = door.Pivot });
                PostUpdateCommands.AddComponent<CreatePerson>(e);
            }

            if (door.State == DoorState.Open)
            {
                door.OpenTime += Time.deltaTime;
                if (door.OpenTime >= _config.openPeriod)
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

            if (angle > 0 && angle < _config.maxOpenAngle)
            {
                if (door.State == DoorState.Opening)
                {
                    door.State = DoorState.Open;
                }

                if (door.State == DoorState.Closing)
                {
                    door.State = DoorState.Closed;

                    if (EntityManager.Exists(door.Person))
                    {
                        PostUpdateCommands.AddComponent<DestroyPerson>(door.Person);
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