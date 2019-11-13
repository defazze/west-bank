using Unity.Entities;

namespace Doors
{
    public struct RotationComponent : IComponentData
    {
        public bool Opening;
        public bool Closing;
    }
}