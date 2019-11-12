using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class CarGameManager : MonoBehaviour
{
    public Mesh selectionMesh;
    public Material selectionMaterial;

    public static CarGameManager Instanse { get; private set; }
    void Awake()
    {
        Instanse = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
