using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        [Header("Reference")]
        [SerializeField] public string tag;
        [SerializeField] public GameObject prefab;
        [Header("Data")]
        [SerializeField] public int size;
    }

    #region Singleton
    // Singleton
    public static ObjectPooler Instance;
    
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDict;

    // Start is called before the first frame update
    void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();
        
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject gObject = Instantiate(pool.prefab);
                gObject.SetActive(false);
                objectPool.Enqueue(gObject);
            }
            poolDict.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag_, Vector3 pos_, Quaternion rot_)
    {
        if (!poolDict.ContainsKey(tag_))
        {
            Debug.LogWarning("Pool " + tag_ + " is not exist. Perhaps you didn't set object?");
            return null;
        }

        GameObject objToSpawn = poolDict[tag_].Dequeue();

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos_;
        objToSpawn.transform.rotation = rot_;

        IPooledObject poolObj = objToSpawn.GetComponent<IPooledObject>();

        if(poolObj != null)
        {
            poolObj.OnObjectSpawn();
        }

        poolDict[tag_].Enqueue(objToSpawn);

        return objToSpawn;
    }
}
