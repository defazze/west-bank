using Unity.Entities;

public struct ConfigData : IComponentData
{
    public Entity test;
    public Entity door;
    public Entity bulletHole;
    public Entity regular;
    public Entity bandit;
    public float distanceBetweenDoors;
    public float doorWidth;
    public float maxOpenAngle;
    public float doorRotationSpeed;
    public float openPeriod;
    public float maxDelayBetweenOpen;
}