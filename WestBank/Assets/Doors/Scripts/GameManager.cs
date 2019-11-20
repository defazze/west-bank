using System.Collections;
using System.Collections.Generic;
using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Doors
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameObject DoorPrefab;
        public GameObject BulletHolePrefab;
        public float distanceBetweenDoors = .2f;
        public float doorWidth = 1.2f;
        public float maxOpenAngle = 270f;
        public float doorRotationSpeed = .7f;
        public float openPeriod = 2f;
        public float maxDelayBetweenOpen = 2f;

        private EntityManager entityManager;
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            var doorEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(DoorPrefab, World.Active);

            entityManager = World.Active.EntityManager;

            var step = doorWidth + distanceBetweenDoors;
            for (var x = -1 * step; x <= step; x += step)
            {
                var door = entityManager.Instantiate(doorEntity);
                var position = new float3(x, 1f, 0);
                entityManager.SetComponentData(door, new Translation { Value = position});
                entityManager.AddComponentData(door, new DoorComponent
                {
                    State = DoorState.Closed,
                    OpenTime = 0,
                    Pivot = (float3)position + new float3(doorWidth / 2, 0, 0)
                });
            }
        }

        void Update()
        {

        }
    }
}