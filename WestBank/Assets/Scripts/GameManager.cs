using System.Collections;
using System.Collections.Generic;
using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject Prefab;
    public float Distance = 2f;
    public float maxOpenAngle = 270f;
    public float doorRotationSpeed = .7f;

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
}
