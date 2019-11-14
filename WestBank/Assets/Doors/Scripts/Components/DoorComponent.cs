using Unity.Entities;
using Unity.Mathematics;

namespace Doors
{
    public struct DoorComponent : IComponentData
    {
        public DoorState State;
        public float OpenTime;
        public float3 Pivot;
    }

    public enum DoorState
    {
        Closed = 0,
        MustOpen = 1,
        Opening = 2,
        Open = 3,
        Closing = 4
    }
}