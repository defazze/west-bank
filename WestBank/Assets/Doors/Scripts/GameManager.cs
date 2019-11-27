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
        public GameObject testPrefab;
        public GameObject doorPrefab;
        public GameObject bulletHolePrefab;
        public Configuration configuration;
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
    }
}