using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

[RequiresEntityConversion]
public class BlueCarEntity : MonoBehaviour, IConvertGameObjectToEntity
{
    public Mesh mesh;
    public Material material;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var types = new[] {
            typeof(RenderMesh),
            typeof(MovementComponent),
            typeof(RotationComponent),
            typeof(InputComponent),
            typeof(SpeedComponent)
            };
            
        var componentTypes = new ComponentTypes(types.Select(t => (ComponentType)t).ToArray());

        dstManager.AddComponents(entity, componentTypes);
        dstManager.SetSharedComponentData(entity, new RenderMesh { mesh = mesh, material = material });

    }
}
