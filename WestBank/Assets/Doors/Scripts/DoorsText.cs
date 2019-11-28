using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UI;

public class DoorsText : MonoBehaviour
{
    void Start()
    {
        var entityManager = World.Active.EntityManager;

        var text = entityManager.CreateEntity();
        entityManager.AddComponent<ShootCountComponent>(text);
        //entityManager.AddComponentObject(text, GetComponent<Text>());
    }
}

public class ShootCountEngine : ComponentSystem
{
    protected override void OnUpdate()
    {

        Entities.WithAll<DebugPosition>().ForEach((ref Translation translation) =>
        {
            var temp = translation;
            Entities.ForEach((Text text) =>
            {
                text.text = temp.Value.ToString();
            });
        });
    }
}
