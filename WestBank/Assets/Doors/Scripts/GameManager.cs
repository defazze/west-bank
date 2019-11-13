﻿using System.Collections;
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
        public GameObject Prefab;
        public float Distance = 2f;
        public float maxOpenAngle = 270f;
        public float doorRotationSpeed = .7f;
        private BuildPhysicsWorld physicsWorldSystem = World.Active.GetExistingSystem<BuildPhysicsWorld>();
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(Prefab, World.Active);
            var entityManager = World.Active.EntityManager;


            for (var x = -1; x <= 1; x++)
            {
                var instance = entityManager.Instantiate(prefab);
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
                }

            }
        }
    }
}