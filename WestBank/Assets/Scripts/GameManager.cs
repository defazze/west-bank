using System.Collections;
using System.Collections.Generic;
using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Prefab;
    public float Distance = 2f;
    void Start()
    {
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(Prefab, World.Active);
        var entityManager = World.Active.EntityManager;


        for (var x = -1; x <= 1; x++)
        {
            var instance = entityManager.Instantiate(prefab);
            var position = transform.TransformPoint(new float3(x*Distance, 1f, 0));
            entityManager.SetComponentData(instance, new Translation { Value = position });

            entityManager.AddComponent(instance, typeof(RotationComponent));
        }
    }
}
