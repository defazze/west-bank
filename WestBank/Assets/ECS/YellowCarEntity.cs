using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

[RequiresEntityConversion]
public class YellowCarEntity : MonoBehaviour, IConvertGameObjectToEntity
{
    public Mesh mesh;
    public Material material;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent(entity, typeof(RenderMesh));
        dstManager.SetSharedComponentData(entity, new RenderMesh { mesh = mesh, material = material });

    }
}