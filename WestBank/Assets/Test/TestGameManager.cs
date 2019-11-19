using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public GameObject CubePrefab;
    public GameObject ChildCubePrefab;
    // Start is called before the first frame update
    void Start()
    {
        var parentCubeEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(CubePrefab, World.Active);
        var childCubeEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(ChildCubePrefab, World.Active);

        var entityManager = World.Active.EntityManager;

        var parentCube = entityManager.Instantiate(parentCubeEntity);
        //entityManager.AddComponent<TestRotation>(parentCube);

        var childCube = entityManager.Instantiate(childCubeEntity);

        //пошла 3Д-математика
        var parentMatrix = (Matrix4x4)entityManager.GetComponentData<LocalToWorld>(parentCube).Value;
        var childMatrix = Matrix4x4.identity;
        childMatrix.SetTRS(new float3(-1, .5f, 0), Quaternion.identity, new float3(1f, 1f, 1f));
        var localMatrix = parentMatrix.inverse * childMatrix;

        var localTranslation = localMatrix.GetColumn(3);
        //entityManager.SetComponentData(childCube, new Translation { Value = new float3(-1, .5f, 0) });

        entityManager.SetComponentData(childCube, new Translation { Value = localMatrix.ExtractPosition() });
        entityManager.SetComponentData(childCube, new Rotation { Value = localMatrix.ExtractRotation() });
        entityManager.AddComponentData(childCube, new NonUniformScale { Value = localMatrix.ExtractScale() });
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
