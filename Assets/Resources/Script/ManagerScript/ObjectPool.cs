using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : ManagerSingleton<ObjectPool>
{
	public GameObject[] poolPrefabs;
	public int BulletLevel = 1;

	public Dictionary<object, List<GameObject>> pooledObjects = new Dictionary<object, List<GameObject>>();

	private void Start()
	{
		CreateMultiplePoolObjects<Bullet>(30);
		//CreateMultiplePoolObjects<BonusMob>(2);
	}

	public void CreateMultiplePoolObjects<T>(int _poolCount)
	{
		for (int i = 0; i < poolPrefabs.Length; ++i)
		{
			if (poolPrefabs[i].name.Equals(typeof(T).Name))
			{
				for (int j = 0; j < _poolCount; ++j)
				{
					if (!pooledObjects.ContainsKey(poolPrefabs[i].name))
					{
						List<GameObject> newList = new List<GameObject>();
						pooledObjects.Add(poolPrefabs[i].name, newList);
					}
	
					GameObject newDoll = Instantiate(poolPrefabs[i], transform);
					newDoll.SetActive(false);
					newDoll.name = typeof(T).Name;
					pooledObjects[poolPrefabs[i].name].Add(newDoll);
				}
			}
		}
	}
	
	public void PushPooledObject<T>(GameObject _item)
	{
		if (pooledObjects.ContainsKey(typeof(T).Name))
		{
			_item.transform.SetParent(transform);
			_item.SetActive(false);
			pooledObjects[typeof(T).Name].Add(_item);
		}
	}
	
	public GameObject PopPooledObject<T>(Transform parent = null)
	{
		if (pooledObjects.ContainsKey(typeof(T).Name))
		{
			GameObject poolObject = pooledObjects[typeof(T).Name][0];
			pooledObjects[typeof(T).Name].RemoveAt(0);
			poolObject.transform.SetParent(null);
			poolObject.SetActive(true);
			return poolObject;
		}
		else
			return null;
	}
}
