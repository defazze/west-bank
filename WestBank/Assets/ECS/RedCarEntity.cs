using System.Linq;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

[RequiresEntityConversion]
public class RedCarEntity : MonoBehaviour, IConvertGameObjectToEntity
{
    public Mesh mesh;
    public Material material;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var types = new[] {
            typeof(RenderMesh),
            typeof(SpeedComponent),
            typeof(InputComponent),
            typeof(AcceleratorComponent)
             };
        var componentTypes = new ComponentTypes(types.Select(t => (ComponentType)t).ToArray());

        dstManager.AddComponents(entity, componentTypes);
        dstManager.SetSharedComponentData(entity, new RenderMesh { mesh = mesh, material = material });
    }
}