using UnityEngine;

namespace Cars
{
    public class GameManager : MonoBehaviour
    {
        public Mesh selectionMesh;
        public Material selectionMaterial;

        public static GameManager Instanse { get; private set; }
        void Awake()
        {
            Instanse = this;
        }

        void Start()
        {

        }
    }
}