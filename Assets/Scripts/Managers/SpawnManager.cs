using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    //------------------------------------------------------------------

    [Header("Object Pooling")]
    [SerializeField] private GameObject prefabToPool;
    [SerializeField] private int numObjectsToPool = 10;
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    //------------------------------------------------------------------
    [Space(10)]

    [Header("Spawn Position")]
    [SerializeField] private Transform spawnPositionsParent;
    private List<Transform> openLanes = new List<Transform>(), 
                            closedLanes = new List<Transform>();

    //------------------------------------------------------------------

    private void Awake()
    {
        CreateSingleton();
        AddEvents();
    }

    private void Start()
    {
        InstantiatePoolObjects();
        InitLanes();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    //--------------------
    #region Set Events

    private void AddEvents()
    {
        Events.OnWaveIntervalTimerDepleted += Events_OnIntervalTimerDepleted;
        Events.OnObjectReadyToFinish += Events_ObjectReadyToFinish;
    }

    private void RemoveEvents()
    {
        Events.OnWaveIntervalTimerDepleted -= Events_OnIntervalTimerDepleted;
        Events.OnObjectReadyToFinish -= Events_ObjectReadyToFinish;
    }

    #endregion
    //--------------------

    //--------------------
    #region Events Activation

    private void Events_OnIntervalTimerDepleted(object sender, System.EventArgs e)
    {
        SpawnObjectFromPool();
    }

    private void Events_ObjectReadyToFinish(GameObject poolObject)
    {
        DespawnObjectToPool(poolObject);
    }

    #endregion
    //--------------------

    //--------------------
    #region Pooling

    private void InstantiatePoolObjects()
    {
        for (int i = 0; i < numObjectsToPool; i++)
        {
            GameObject gameObject = Instantiate(prefabToPool, transform);
            AddObjectToPool(gameObject);
        }
    }

    private void AddObjectToPool(GameObject poolObject)
    {
        objectPool.Enqueue(poolObject);
    }

    #endregion
    //--------------------

    //--------------------
    #region Lanes

    private void InitLanes()
    {
        // add all spawn positions to openLane list
        for (int i = 0; i < spawnPositionsParent.childCount; i++)
        {
            openLanes.Add(spawnPositionsParent.GetChild(i)); 
        }
        closedLanes.Clear();
    }

    private Transform GetRandomOpenLane()
    {
        int spawnPositionIndex = UnityEngine.Random.Range(0, openLanes.Count);
        return openLanes[spawnPositionIndex];
    }

    private void MoveLane(Transform lane, List<Transform> MoveFrom, List<Transform> MoveTo)
    {
        MoveFrom.Remove(lane);
        MoveTo.Add(lane);
    }

    #endregion
    //--------------------

    //--------------------
    #region Spawning


    private void SpawnObjectFromPool()
    {
        if (objectPool.Count > 0 && openLanes.Count > 0)
        {
            Transform spawnPosition = GetRandomOpenLane();
            MoveLane(spawnPosition, openLanes, closedLanes);
            ReadyObjectAtPosition(spawnPosition);
        }
    }

    private void ReadyObjectAtPosition(Transform position)
    {
        GameObject gameObject = objectPool.Dequeue();
        gameObject.transform.SetParent(position);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }

    private void DespawnObjectToPool(GameObject poolObject)
    {
        poolObject.SetActive(false);
        MoveLane(poolObject.transform.parent, closedLanes, openLanes);
        poolObject.transform.parent = transform;
        AddObjectToPool(poolObject);
    }

    #endregion
    //--------------------

    private void CreateSingleton()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of SpawnManager");
        }
        Instance = this;
    }
}
