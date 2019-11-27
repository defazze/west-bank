using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class InitSystem : ComponentSystem
{
    private Configuration _config;

    protected override void OnCreate()
    {
        _config = GameManager.Instance.configuration;
        var em = EntityManager;

        /*
                var doorEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(GameManager.Instance.doorPrefab, World.Active);

                var step = _config.doorWidth + _config.distanceBetweenDoors;
                for (var x = -1 * step; x <= step; x += step)
                {
                    var door = em.Instantiate(doorEntity);
                    var position = new float3(x, 1f, 0);
                    em.SetComponentData(door, new Translation { Value = position });
                    em.AddComponentData(door, new DoorComponent
                    {
                        State = DoorState.Closed,
                        OpenTime = 0,
                        Pivot = (float3)position + new float3(_config.doorWidth / 2, 0, 0)
                    });
                }

        */
        var testEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(GameManager.Instance.testPrefab, World.Active);
        var test = em.Instantiate(testEntity);
        em.AddComponent<DebugPosition>(test);
        var pos = new float3(0, 0, 0);
        em.SetComponentData(test, new Translation { Value = pos });

    }

    protected override void OnUpdate()
    {
        //throw new System.NotImplementedException();
    }
}