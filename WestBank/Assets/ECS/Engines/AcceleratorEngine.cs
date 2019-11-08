using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class AcceleratorEngine : ComponentSystem
{
    const float BOOST_VALUE = 2f;
    protected override void OnUpdate()
    {
        Entities.ForEach((ref AcceleratorComponent acceleratorComponent, ref SpeedComponent speedComponent) =>
        {
            speedComponent.Speed += acceleratorComponent.Boost * BOOST_VALUE * Time.deltaTime;
        });
    }
}