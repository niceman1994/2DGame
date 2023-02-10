using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : ManagerSingleton<ObjectPool>
{
	public GameObject[] poolPrefabs;
	private Dictionary<object, List<GameObject>> pooledObjects = new Dictionary<object, List<GameObject>>();

	private void Start()
	{
		CreateMultiplePoolObjects("Bullet", 30);
		CreateMultiplePoolObjects("smallEnemy1", 8);
		ThrowPoolObject("smallEnemy1");
	}

	public void CreateMultiplePoolObjects(string _name, int _poolCount)
	{
		for (int i = 0; i < poolPrefabs.Length; ++i)
		{
			if (poolPrefabs[i].name.Equals(_name))
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
					newDoll.name = _name;
					pooledObjects[poolPrefabs[i].name].Add(newDoll);
				}
			}
		}
	}
	
	public void PushPooledObject(string _name, GameObject _item)
	{
		if (pooledObjects.ContainsKey(_name))
		{
			_item.transform.SetParent(transform);
			_item.SetActive(false);
			pooledObjects[_name].Add(_item);
		}
	}
	
	public GameObject PopPooledObject(string _name, Transform parent = null)
	{
		if (pooledObjects.ContainsKey(_name))
		{
			GameObject poolObject = pooledObjects[_name][0];
			pooledObjects[_name].RemoveAt(0);
			poolObject.transform.SetParent(null);
			poolObject.SetActive(true);
			return poolObject;
		}
		else
			return null;
	}

	public void ThrowPoolObject(string _name)
	{
		if (pooledObjects.ContainsKey(_name))
		{
			for (int i = 0; i < pooledObjects[_name].Count; ++i)
			{
				GameObject item = pooledObjects[_name][i];

				ObjectManager.Instance.CatchObject.Add(item);
			}
		}
	}
}
