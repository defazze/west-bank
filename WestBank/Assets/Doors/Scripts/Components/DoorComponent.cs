using Unity.Entities;
using Unity.Mathematics;

namespace Doors
{
    public struct DoorComponent : IComponentData
    {
        public DoorState state;
        public float openTime;
        public float3 pivot;
        public Entity person;
        public float personXOffset;
        public float personRotation;
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