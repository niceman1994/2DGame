public interface Interface
{
	public void Initialize();
	public void Progress();
	public void Release();
}

/*
	public List<PooledObject> objectPool = new List<PooledObject>();

	private void Start()
	{
		for (int i = 0; i < objectPool.Count; ++i)
			objectPool[i].Initialize(transform);
	}
	
	public bool PushToPool(string itemName, GameObject item, Transform parent = null)
	{
		PooledObject pool = GetPoolItem(itemName);

		if (pool == null) return false;

		pool.PushToPool(item, parent == null ? transform : parent);
		return true;
	}

	public GameObject PopFromPool(string itemName, Transform parent = null)
	{
		PooledObject pool = GetPoolItem(itemName);

		if (pool == null)
			return null;

		return pool.PopFromPool(parent);
	}

	PooledObject GetPoolItem(string itemName) 
	{
		for (int i = 0; i < objectPool.Count; ++i)
		{
			if (objectPool[i].poolItemName.Equals(itemName))
				return objectPool[i];
		}

		Debug.LogWarning("There's no matched pool list.");
		return null;
	}

	/// <summary>
	/// Queue형식의 단일 오브젝트 풀링
	/// </summary>

	[SerializeField] private GameObject poolObjectPrefab;

	private Queue<GameObject> ObjectQueue = new Queue<GameObject>();

	private void Start()
	{
		Initialize(30);
	}
	
	private void Initialize(int initCount)
	{
		for (int i = 0; i < initCount; ++i)
			ObjectQueue.Enqueue(CreateNewObject());
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
		if (Instance.ObjectQueue.Count > 0)
		{
			GameObject Obj = Instance.ObjectQueue.Dequeue();
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
		Instance.ObjectQueue.Enqueue(Obj);
	}
*/