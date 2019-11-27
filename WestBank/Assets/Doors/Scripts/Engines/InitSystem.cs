using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class InitSystem : ComponentSystem
{
    private ConfigData _config;
    private bool _created = false;
    protected override void OnUpdate()
    {
        if (!_created)
        {
            _config = GetSingleton<ConfigData>();
            var em = EntityManager;

            var doorEntity = _config.door;

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

            _created = true;
        }
    }
}