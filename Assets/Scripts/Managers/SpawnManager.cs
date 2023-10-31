using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("Object Pooling")]
    [SerializeField] private GameObject prefabToPool;
    [SerializeField] private int numObjectsToPool = 10;
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    [Header("Spawn Position")]
    [SerializeField] private Transform spawnPositionsParent;
    Transform[] openLanes;
    Transform[] closedLanes;
    private int lastSpawnPositionIndex = -1; // index of last spawn position used. -1 if no spawn positions have been used

    private void Awake()
    {
        CreateSingleton();
        Events.OnWaveIntervalTimerDepleted += WaveManager_OnIntervalTimerDepleted;
    }

    private void Start()
    {
        InstantiatePoolObjects();
    }

    private void OnDestroy()
    {
        Events.OnWaveIntervalTimerDepleted -= WaveManager_OnIntervalTimerDepleted;
    }

    private void InstantiatePoolObjects()
    {
        for (int i = 0; i < numObjectsToPool; i++)
        {
            Transform spawnPositionParent = GetRandomSpawnPosition();
            GameObject gameObject = Instantiate(prefabToPool, spawnPositionParent);
            AddObjectToPool(gameObject);
        }
    }

    private void AddObjectToPool(GameObject objectToPool)
    {
        objectPool.Enqueue(objectToPool);
    }

    public void ReturnObjectToPool(GameObject objectToPool)
    {
        objectToPool.transform.parent = GetRandomSpawnPosition();
        objectToPool.transform.localPosition = Vector3.zero;
        AddObjectToPool(objectToPool);
    }

    /// <summary>
    /// Gets a random transform from the spawn positions.<br />
    /// Ignores the last spawn position used if there is one.
    /// </summary>
    /// <returns>Transform of random spawn position</returns>
    private Transform GetRandomSpawnPosition()
    {
        bool excludeAPosition = lastSpawnPositionIndex > -1;
        int amountSpawnPositions = spawnPositionsParent.childCount;
        int spawnPositionIndex = Random.Range(0, excludeAPosition ? amountSpawnPositions - 1 : amountSpawnPositions);
        if (excludeAPosition && spawnPositionIndex >= lastSpawnPositionIndex)
        {
            spawnPositionIndex++;
        }
        lastSpawnPositionIndex = spawnPositionIndex;

        return spawnPositionsParent.GetChild(spawnPositionIndex);
    }

    private void WaveManager_OnIntervalTimerDepleted(object sender, System.EventArgs e)
    {
        if (objectPool.Count > 0)
        {
            GameObject gameObject = objectPool.Dequeue();
            gameObject.SetActive(true);
        }
    }

    private void CreateSingleton()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of SpawnManager");
        }
        Instance = this;
    }
}
