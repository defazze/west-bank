using System.Collections.Generic;
using Doors;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class DoorsEngine : ComponentSystem
{
    private float _currentDelay = 0;
    private float _currentTime = 0;
    private EntityQuery _query;

    private Configuration _config;

    protected override void OnCreate()
    {
        _query = GetEntityQuery(typeof(DoorComponent));
        _config = GameManager.Instance.configuration;
    }

    protected override void OnUpdate()
    {
        if (_currentDelay == 0)
        {
            _currentDelay = Random.Range(0, _config.maxDelayBetweenOpen);
        }

        _currentTime += Time.deltaTime;

        if (_currentTime > _currentDelay)
        {
            var closedDoorIndexes = new List<int>();
            var doors = _query.ToComponentDataArray<DoorComponent>(Allocator.TempJob);

            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].State == DoorState.Closed)
                {
                    closedDoorIndexes.Add(i);
                }
            }

            if (closedDoorIndexes.Count > 0)
            {
                var random = Random.Range(0, closedDoorIndexes.Count - 1);
                var doorIndex = closedDoorIndexes[random];

                var temp = doors[doorIndex];
                temp.State = DoorState.MustOpen;
                doors[doorIndex] = temp;
                _query.CopyFromComponentDataArray(doors);
            }

            doors.Dispose();

            _currentDelay = 0;
            _currentTime = 0;
        }
    }
}