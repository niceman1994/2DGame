using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : ManagerSingleton<ObjectPool>
{
	[SerializeField] private GameObject poolObjectPrefab;

	private Queue<GameObject> BulletQueue = new Queue<GameObject>();
	private Queue<GameObject> EnemyQueue = new Queue<GameObject>();

	private void Start()
	{
		Initialize(60);
	}

	private void Initialize(int initCount)
	{
		for (int i = 0; i < initCount; ++i)
			BulletQueue.Enqueue(CreateNewObject());
	}

	private GameObject CreateNewObject()
	{
		GameObject newObj = Instantiate(poolObjectPrefab).GetComponent<BulletController>().gameObject;
		newObj.gameObject.SetActive(false);
		newObj.transform.SetParent(transform);
		return newObj;
	}

	public static GameObject GetObject()
	{
		if (Instance.BulletQueue.Count > 0)
		{
			GameObject Obj = Instance.BulletQueue.Dequeue();
			Obj.transform.SetParent(null);
			Obj.gameObject.SetActive(true);
			return Obj;
		}
		else
		{
			GameObject newObj = Instance.CreateNewObject();
			newObj.gameObject.SetActive(true);
			newObj.transform.SetParent(null);
			return newObj;
		}
	}

	public static void ReturnObject(GameObject Obj)
	{
		Obj.gameObject.SetActive(false);
		Obj.transform.SetParent(Instance.transform);
		Instance.BulletQueue.Enqueue(Obj);
	}
}
