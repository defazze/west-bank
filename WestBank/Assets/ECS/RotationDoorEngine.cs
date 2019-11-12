using System;
using Doors;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

/*
public class RotationDoorEngine : ComponentSystem
{
    private float closedAngle = 0;
    private float openAngle = 270f;
    private float rotationSpeed = .7f;

    private bool opening = true;
    private bool closing = false;

    protected override void OnUpdate()
    {
        Entities.WithAll<RotationComponent>().ForEach((Entity entity, ref Rotation rotation) =>
        {
            var angle = ((Quaternion)rotation.Value).eulerAngles.y;

            if (opening)
            {
                quaternion newRotation = math.mul(rotation.Value, quaternion.RotateY(-rotationSpeed * Time.deltaTime));
                rotation.Value = newRotation;

                if (angle > 0 && angle < openAngle)
                {
                    opening = false;
                    closing = true;
                }
            }

            if (closing)
            {
                //Debug.Log(angle);
                quaternion newRotation = math.mul(rotation.Value, quaternion.RotateY(rotationSpeed * Time.deltaTime));
                rotation.Value = newRotation;

                if (angle > 0 && angle < openAngle)
                {
                    opening = true;
                    closing = false;
                }
            }
        });
    }
}*/