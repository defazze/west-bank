using Doors;
using Unity.Entities;
using Unity.Mathematics;
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

                var bulletHoleEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(GameManager.Instance.bulletHolePrefab, World.Active);
                var bulletHole = entityManager.Instantiate(bulletHoleEntity);

                var bulletHoleRotation = Quaternion.FromToRotation(Vector3.back, hit.SurfaceNormal);

                if (entityManager.HasComponent<DoorComponent>(e))
                {
                    var doorMatrix = (Matrix4x4)entityManager.GetComponentData<LocalToWorld>(e).Value;

                    var bulletHoleMatrix = Matrix4x4.identity;
                    bulletHoleMatrix.SetTRS(hit.Position, bulletHoleRotation, entityManager.GetComponentData<NonUniformScale>(bulletHole).Value);

                    var transformMatrix = doorMatrix.inverse * bulletHoleMatrix;

                    entityManager.SetComponentData(bulletHole, new Translation { Value = transformMatrix.ExtractPosition() });
                    entityManager.SetComponentData(bulletHole, new Rotation { Value = transformMatrix.ExtractRotation() });
                    entityManager.SetComponentData(bulletHole, new NonUniformScale { Value = transformMatrix.ExtractScale() });
                    entityManager.AddComponentData(bulletHole, new Parent { Value = e });
                    entityManager.AddComponent<LocalToParent>(bulletHole);
                }
                else
                {
                    entityManager.SetComponentData(bulletHole, new Rotation { Value = bulletHoleRotation });
                    entityManager.SetComponentData(bulletHole, new Translation { Value = hit.Position });
                }
            }
        }
    }
}