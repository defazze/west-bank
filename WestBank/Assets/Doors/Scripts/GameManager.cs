using System.Collections;
using System.Collections.Generic;
using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Doors
{
    public class GameManager : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {

        public static GameManager Instance { get; private set; }

        public GameObject testPrefab;
        public GameObject doorPrefab;
        public GameObject bulletHolePrefab;
        public GameObject regularPrefab;
        public GameObject banditPrefab;
        public float distanceBetweenDoors = .2f;
        public float doorWidth = 1.2f;
        public float maxOpenAngle = 270f;
        public float doorRotationSpeed = .7f;
        public float openPeriod = 2f;
        public float maxDelayBetweenOpen = 2f;
        private EntityManager _entityManager;
        private GameManager()
        {
            Instance = this;
        }

        void Start()
        {


        }

        void Update()
        {

        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.AddRange(new[] { testPrefab, doorPrefab, bulletHolePrefab, regularPrefab, banditPrefab });
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var config = new ConfigData
            {
                test = conversionSystem.GetPrimaryEntity(testPrefab),
                bulletHole = conversionSystem.GetPrimaryEntity(bulletHolePrefab),
                door = conversionSystem.GetPrimaryEntity(doorPrefab),
                regular = conversionSystem.GetPrimaryEntity(regularPrefab),
                bandit = conversionSystem.GetPrimaryEntity(banditPrefab),
                distanceBetweenDoors = distanceBetweenDoors,
                doorWidth = doorWidth,
                maxOpenAngle = maxOpenAngle,
                doorRotationSpeed = doorRotationSpeed,
                openPeriod = openPeriod,
                maxDelayBetweenOpen = maxDelayBetweenOpen
            };

            dstManager.AddComponentData(entity, config);
        }
    }
}