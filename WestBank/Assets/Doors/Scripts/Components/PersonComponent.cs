using Unity.Entities;

public struct PersonComponent : IComponentData
{
    public DynamicBuffer<LinkedEntityGroup> Buffer;
}

public struct CreatePerson : IComponentData
{

}

public struct DestroyPerson : IComponentData
{

}