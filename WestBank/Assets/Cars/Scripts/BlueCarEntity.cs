using System.Linq;
using Cars;
using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class BlueCarEntity : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var types = new[] {
            typeof(MovementComponent),
            typeof(RotationComponent),
            typeof(InputComponent),
            typeof(SelectableComponent),
            typeof(SelectedComponent)
            };

        var componentTypes = new ComponentTypes(types.Select(t => (ComponentType)t).ToArray());

        dstManager.AddComponents(entity, componentTypes);
    }
}
