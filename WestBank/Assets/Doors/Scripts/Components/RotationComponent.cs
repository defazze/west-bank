using Unity.Entities;
using Unity.Mathematics;

namespace Doors
{
    public struct RotationComponent : IComponentData
    {
        public bool IsTest;
        public float3 Pivot;
        public bool Opening;
        public bool Closing;
    }
}