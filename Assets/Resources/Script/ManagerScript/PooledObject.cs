using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public string ItemName;
    public GameObject prefab;
    public int count;
}

public class PooledObject : MonoBehaviour
{
    public ObjectInfo[] Infos = null;

    public string poolItemName = string.Empty;
    public GameObject prefab = null;
    public int poolCount = 0;

    [SerializeField] private List<List<GameObject>> poolLists = new List<List<GameObject>>();
    [SerializeField] private List<GameObject> poolList = new List<GameObject>();

    public void Initialize()
    {
        if (Infos != null)
        {
            for (int i = 0; i < Infos.Length; ++i)
                poolLists.Add(CreateItem(Infos[i]));
        }
    }

    public void Initialize(Transform parent = null) 
    {
        for (int i = 0; i < poolCount; ++i)
            poolList.Add(CreateItem(parent));
    }

    public void PushToPool(GameObject item, Transform parent = null) 
    {
        item.transform.SetParent(parent);
        item.SetActive(false);
        poolList.Add(item);
    }

    public GameObject PopFromPool(Transform parent = null) 
    {
        if (poolList.Count == 0)
            poolList.Add(CreateItem(parent));

        GameObject item = poolList[0];
        poolList.RemoveAt(0);
        return item;
    }

    //public GameObject PopFromPool(ObjectInfo prefabInfo)
    //{
    //    if (poolLists.Count == 0)
    //    {
    //        for (int i = 0; i < Infos.Length; ++i)
    //            poolLists.Add(CreateItem(Infos[i]));
    //    }
    //
    //    List<GameObject> item = poolLists.IndexOf(prefabInfo);
    //}

    private List<GameObject> CreateItem(ObjectInfo prefabInfo)
    {
        List<GameObject> tempInfo = new List<GameObject>();

        for (int i = 0; i < prefabInfo.count; ++i)
        {
            GameObject item = Object.Instantiate(prefabInfo.prefab) as GameObject;
            item.name = prefabInfo.ItemName;
            item.transform.SetParent(transform);
            item.SetActive(false);
            tempInfo.Add(item);
        }
        return tempInfo;
    }

    private GameObject CreateItem(Transform parent = null) 
    {
        GameObject item = Object.Instantiate(prefab) as GameObject;
        item.name = poolItemName;
        item.transform.SetParent(parent);
        item.SetActive(false);
        return item;
    }
}
