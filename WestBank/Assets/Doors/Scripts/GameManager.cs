using System.Collections;
using System.Collections.Generic;
using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

namespace Doors
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameObject DoorPrefab;
        public GameObject BulletHolePrefab;
        public float Distance = 2f;
        public float maxOpenAngle = 270f;
        public float doorRotationSpeed = .7f;

        private BuildPhysicsWorld physicsWorldSystem;
        private Entity bulletHoleEntity;
        private EntityManager entityManager;
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            physicsWorldSystem = World.Active.GetExistingSystem<BuildPhysicsWorld>();

            var doorEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(DoorPrefab, World.Active);
            bulletHoleEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(BulletHolePrefab, World.Active);

            entityManager = World.Active.EntityManager;


            for (var x = -1; x <= 1; x++)
            {
                var instance = entityManager.Instantiate(doorEntity);
                var position = transform.TransformPoint(new float3(x * Distance, 1f, 0));
                entityManager.SetComponentData(instance, new Translation { Value = position });
                entityManager.AddComponentData(instance, new RotationComponent { Opening = true });
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
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
                    // see hit.Position 
                    // see hit.SurfaceNormal

                    Entity e = physicsWorldSystem.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;

                    var bulletHole = entityManager.Instantiate(bulletHoleEntity);
                    entityManager.SetComponentData(bulletHole, new Translation { Value = hit.Position });
                    entityManager.SetComponentData(bulletHole, new Rotation { Value = Quaternion.FromToRotation(Vector3.forward, hit.SurfaceNormal) });
                }

            }
        }
    }
}