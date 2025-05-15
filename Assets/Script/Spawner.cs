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

    [Header("Coin Settings")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float minCoinSpawnRate = 2f;
    [SerializeField] private float maxCoinSpawnRate = 4f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        InvokeSpawn();
        InvokeCoinSpawn();
    }

    private void OnEnable()
    { 
        InvokeSpawn();
        InvokeCoinSpawn();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        if (!gameManager.IsGameRunning()) return;

        float spawnChance = Random.value;

        foreach(var obj in spawnObject)
        {
            if(spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.ObjectPrefab);
                Vector3 spawnPosition = obstacle.transform.position;
                spawnPosition.x = transform.position.x;
                obstacle.transform.position = spawnPosition;
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        InvokeSpawn();
    }

    private void SpawnCoin()
    {
        if (!gameManager.IsGameRunning()) return;

        if (coinPrefab == null) return;

        Vector3 coinPosition = transform.position;
        Instantiate(coinPrefab, coinPosition, Quaternion.identity);

        InvokeCoinSpawn();
    }

    private void InvokeSpawn()
    {
        Invoke(nameof(Spawn), Random.Range(MinSpawnRate, MaxSpawnRate));
    }

    private void InvokeCoinSpawn()
    {
        Invoke(nameof(SpawnCoin), Random.Range(minCoinSpawnRate, maxCoinSpawnRate));
    }
}
