using System.Linq;
using Cars;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

[RequiresEntityConversion]
public class YellowCarEntity : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var types = new[] {
            typeof(InputComponent),
            typeof(RotationComponent),
            typeof(SelectableComponent)
             };
        var componentTypes = new ComponentTypes(types.Select(t => (ComponentType)t).ToArray());

        dstManager.AddComponents(entity, componentTypes);
    }
}