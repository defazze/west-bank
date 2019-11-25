
using UnityEngine;

[CreateAssetMenu(fileName = "New Configuration", menuName = "Configuration", order = 51)]
public class Configuration : ScriptableObject
{
    public float distanceBetweenDoors = .2f;
    public float doorWidth = 1.2f;
    public float maxOpenAngle = 270f;
    public float doorRotationSpeed = .7f;
    public float openPeriod = 2f;
    public float maxDelayBetweenOpen = 2f;

    [SerializeField]
    public Prefabs prefabs = new Prefabs();
}

[System.Serializable]
public class Prefabs
{
    public GameObject regular;
    public GameObject bandit;
}