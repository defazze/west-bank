using System.IO;
using Unity.Entities;
using Unity.Entities.Serialization;
using UnityEngine;

public class SerializationSystem : ComponentSystem
{
    private string _worldPath = Path.Combine(Application.dataPath, "world.data");
    private string _componentsPath = Path.Combine(Application.dataPath, "components.data");

    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!Directory.Exists(Application.dataPath))
            {
                Directory.CreateDirectory(Application.dataPath);
            }


            int[] sharedComponents;
            GameObject gameObject = new GameObject();
            using (var writer = new StreamBinaryWriter(_worldPath))
            {
                SerializeUtility.SerializeWorld(EntityManager, writer, out sharedComponents);
            }


            using (var writer = new StreamBinaryWriter(_componentsPath))
            {
                SerializeUtility.SerializeSharedComponents(EntityManager, writer, in sharedComponents);
            }

            Debug.Log("Saved");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            using (World tempWorld = new World("loading"))
            {
                var tempEm = tempWorld.EntityManager;

                int sharedComponents;

                using (var reader = new StreamBinaryReader(_componentsPath))
                {
                    sharedComponents = SerializeUtility.DeserializeSharedComponents(tempEm, reader);
                }

                var transaction = tempEm.BeginExclusiveEntityTransaction();

                using (var reader = new StreamBinaryReader(_worldPath))
                {
                    SerializeUtility.DeserializeWorld(transaction, reader, sharedComponents);
                }

                tempEm.EndExclusiveEntityTransaction();
            }
            Debug.Log("Loaded");
        }
    }
}