using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject ObjectPrefab;

        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] spawnObject;

    public float MinSpawnRate = 1f;
    public float MaxSpawnRate = 2f;

    private void Start()
    {
        InvokeSpawn();
    }

    private void OnEnable()
    {
        InvokeSpawn();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach(var obj in spawnObject)
        {
            if(spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.ObjectPrefab);
                obstacle.transform.position = transform.position;
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        InvokeSpawn();
    }

    private void InvokeSpawn()
    {
        Invoke(nameof(Spawn), Random.Range(MinSpawnRate, MaxSpawnRate));
    }
}
