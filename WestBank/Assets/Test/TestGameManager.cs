using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public GameObject CubePrefab;
    // Start is called before the first frame update
    void Start()
    {
        var parentCubeEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(CubePrefab, World.Active);

        var entityManager = World.Active.EntityManager;

        var parentCube = entityManager.Instantiate(parentCubeEntity);
        entityManager.AddComponent<TestRotation>(parentCube);

        var childCube = entityManager.Instantiate(parentCubeEntity);
        entityManager.SetComponentData(childCube, new Translation { Value = new float3(-1, .5f, 0) });
        entityManager.AddComponentData(childCube, new Parent { Value = parentCube });
        entityManager.AddComponent<LocalToParent>(childCube);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class TestRotationSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAll<TestRotation>().ForEach((ref Rotation rotation) =>
        {
            rotation.Value = math.mul(rotation.Value, quaternion.RotateY(Time.deltaTime));

        });
    }
}
