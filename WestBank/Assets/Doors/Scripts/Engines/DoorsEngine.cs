using System.Collections.Generic;
using Doors;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class DoorsEngine : ComponentSystem
{
    private float _currentDelay = 0;
    private float _currentTime = 0;
    private EntityQuery query;
    protected override void OnCreate()
    {
        query = GetEntityQuery(typeof(DoorComponent));
    }

    protected override void OnUpdate()
    {
        if (_currentDelay == 0)
        {
            _currentDelay = Random.Range(0, GameManager.Instance.maxDelayBetweenOpen);
        }

        _currentTime += Time.deltaTime;

        if (_currentTime > _currentDelay)
        {
            var closedDoors = new List<Entity>();

            Entities.ForEach((Entity e, ref DoorComponent doorComponent) =>
            {
                if (doorComponent.State == DoorState.Closed)
                {
                    closedDoors.Add(e);
                }
            });

            if (closedDoors.Count > 0)
            {
                var randomDoorIndex = Random.Range(0, closedDoors.Count - 1);
                var closedDoor = closedDoors[randomDoorIndex];
                var closedDoorComponent = EntityManager.GetComponentData<DoorComponent>(closedDoor);
                closedDoorComponent.State = DoorState.MustOpen;
                PostUpdateCommands.SetComponent(closedDoor, closedDoorComponent);
            }

            _currentDelay = 0;
            _currentTime = 0;
        }
    }
}