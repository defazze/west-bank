using Unity.Entities;
using Unity.Mathematics;

namespace Doors
{
    public struct RotationComponent : IComponentData
    {
        public float3 Pivot;
        public bool Opening;
        public float3 NewPosition;
        public quaternion NewRotation;
    }
}