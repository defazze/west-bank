using Unity.Entities;

namespace Cars
{
    public struct RotationComponent : IComponentData
    {
    }
}

namespace Doors
{
    public struct RotationComponent : IComponentData
    {
        public bool Opening;
        public bool Closing;
    }
}