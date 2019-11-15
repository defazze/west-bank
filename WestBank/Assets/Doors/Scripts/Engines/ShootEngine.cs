using Doors;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

public class ShootEngine : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var physicsWorldSystem = World.Active.GetExistingSystem<BuildPhysicsWorld>();
            var entityManager = World.Active.EntityManager;

            var v3 = Input.mousePosition;
            v3.z = 10.0f;

            var mousePosition = Camera.main.ScreenToWorldPoint(v3);
            var collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;

            RaycastInput input = new RaycastInput()
            {
                Start = Camera.main.transform.position,
                End = mousePosition,
                Filter = new CollisionFilter()
                {
                    BelongsTo = ~0u,
                    CollidesWith = ~0u, // all 1s, so all layers, collide with everything 
                    GroupIndex = 0
                }
            };

            var hit = new Unity.Physics.RaycastHit();
            bool haveHit = collisionWorld.CastRay(input, out hit);
            if (haveHit)
            {
                Entity e = physicsWorldSystem.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;

                var bulletHoleEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(GameManager.Instance.BulletHolePrefab, World.Active);
                var bulletHole = entityManager.Instantiate(bulletHoleEntity);

                var bulletHoleRotation = new Rotation { Value = Quaternion.FromToRotation(Vector3.back, hit.SurfaceNormal) };
                entityManager.SetComponentData(bulletHole, bulletHoleRotation);
                entityManager.SetComponentData(bulletHole, new Translation { Value = hit.Position });

                if (entityManager.HasComponent<DoorComponent>(e))
                {
                    entityManager.AddComponentData(bulletHole, new BulletHoleComponent { Surface = e });
                }
            }
        }
    }
}