using System.Linq;
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
        /*
        if (_currentDelay == 0)
        {
            _currentDelay = Random.Range(0, GameManager.Instance.maxDelayBetweenOpen);
            Debug.Log("Delay: " + _currentDelay);
        }

        _currentTime += Time.deltaTime;
        Debug.Log("Current Time: " + _currentTime);

        if (_currentTime > _currentDelay)
        {
            Debug.Log("Select Door");
            var doors = query.ToComponentDataArray<DoorComponent>(Allocator.TempJob);

            var closedDoors = doors.Where(d => d.State == DoorState.Closed).ToArray();
            var closedDoorsCount = closedDoors.Length;

            var randomDoorIndex = Random.Range(0, closedDoorsCount - 1);
            closedDoors[randomDoorIndex].State = DoorState.MustOpen;

            _currentDelay = 0;
            _currentTime = 0;
        }*/
    }
}