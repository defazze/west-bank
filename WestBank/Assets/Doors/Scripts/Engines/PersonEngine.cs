using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class PersonEngine : ComponentSystem
{
    private Configuration _config;
    private Entity _regularEntity;
    private Entity _banditEntity;
    protected override void OnCreate()
    {
        _config = GameManager.Instance.configuration;
        _regularEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(_config.prefabs.regular, World.Active);
        _banditEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(_config.prefabs.bandit, World.Active);
    }

    protected override void OnUpdate()
    {
        Entities.WithAll<CreatePerson>().ForEach((Entity e, ref Translation translation, ref DoorComponent door) =>
        {
            var random = UnityEngine.Random.Range(0, 2);
            var person = EntityManager.Instantiate(random == 0 ? _regularEntity : _banditEntity);
            EntityManager.AddBuffer<LinkedEntityGroup>(person);

            door.Person = person;
            PostUpdateCommands.AddComponent(person, new PersonComponent());
            PostUpdateCommands.SetComponent(person, new Translation { Value = new float3(translation.Value.x, .7f, .6f) });
            PostUpdateCommands.SetComponent(e, door);
            PostUpdateCommands.RemoveComponent<CreatePerson>(e);
        });

        Entities.WithAll<DestroyPerson>().ForEach((Entity e) =>
        {
            PostUpdateCommands.DestroyEntity(e);
        });
    }
}