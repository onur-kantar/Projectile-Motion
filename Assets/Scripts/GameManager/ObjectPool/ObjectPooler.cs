using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] List<ObjectPoolItem> itemsToPool;
    Dictionary<string, List<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            poolDictionary.Add(item.tag, objectPool);
        }
    }
    public GameObject GetPooledObject(string tag)
    {
        foreach (GameObject obj in poolDictionary[tag])
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.tag == tag)
            {
                if (item.expandAmount > 0)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    poolDictionary[tag].Add(obj);
                    item.expandAmount--;
                    return obj;
                }
            }
        }
        return null;
    }
}
