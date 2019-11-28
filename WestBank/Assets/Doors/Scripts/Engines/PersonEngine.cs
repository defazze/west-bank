using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class PersonEngine : ComponentSystem
{
    private ConfigData _config;
    private Entity _regularEntity;
    private Entity _banditEntity;

    protected override void OnUpdate()
    {
        _config = GetSingleton<ConfigData>();
        _regularEntity = _config.regular;
        _banditEntity = _config.bandit;

        Entities.WithAll<CreatePerson>().ForEach((Entity e, ref Translation translation, ref DoorComponent door) =>
        {
            var random = UnityEngine.Random.Range(0, 2);
            var person = EntityManager.Instantiate(random == 0 ? _regularEntity : _banditEntity);
            EntityManager.AddBuffer<LinkedEntityGroup>(person);

            door.person = person;

            PostUpdateCommands.AddComponent(person, new PersonComponent());
            PostUpdateCommands.SetComponent(person, new Translation { Value = new float3(translation.Value.x + door.personXOffset, .7f, .6f) });
            PostUpdateCommands.SetComponent(e, door);
            PostUpdateCommands.RemoveComponent<CreatePerson>(e);
        });

        Entities.WithAll<DestroyPerson>().ForEach((Entity e) =>
        {
            PostUpdateCommands.DestroyEntity(e);
        });
    }
}