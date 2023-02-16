using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyManager : ManagerSingleton<EnemyManager>
{
    public GameObject[] EnemyPrefab;
    public Dictionary<object, List<GameObject>> EnemyLists = new Dictionary<object, List<GameObject>>();

	void Start()
    {
        Initialize();
        StartCoroutine(setDelay1<smallEnemy1>(0.2f));
        StartCoroutine(setDelay2<smallEnemy2>(0.55f));
    }

	void Initialize()
	{
        SpawnEnemy<smallEnemy1>(4, new Vector2(46.0f, 2.1f));
        SpawnEnemy<smallEnemy1>(4, new Vector2(50.0f, -2.1f));

        for (int i = 0; i < 12; ++i)
        {
            SpawnEnemy<smallEnemy2>(1, 34.0f, 4.1f);
            SpawnEnemy<smallEnemy2>(1, 34.0f, -4.1f);
        }
    }

	void SpawnEnemy<T>(int count, Vector2 _position)
    {
        for (int i = 0; i < EnemyPrefab.Length; ++i)
        {
            if (EnemyPrefab[i].name.Equals(typeof(T).Name))
            {
                for (int j = 0; j < count; ++j)
                {
                    if (!EnemyLists.ContainsKey(EnemyPrefab[i].name))
					{
                        List<GameObject> EnemyList = new List<GameObject>();
                        EnemyLists.Add(EnemyPrefab[i].name, EnemyList);
					}

                    GameObject Enemy = Instantiate(EnemyPrefab[i]);
                    Enemy.name = typeof(T).Name;
                    Enemy.transform.position = new Vector2(_position.x + (j * 2), _position.y);
                    EnemyLists[EnemyPrefab[i].name].Add(Enemy);
                }
            }
        }
    }

    void SpawnEnemy<T>(int count, float _x, float _y)
    {
        for (int i = 0; i < EnemyPrefab.Length; ++i)
        {
            if (EnemyPrefab[i].name.Equals(typeof(T).Name))
            {
                for (int j = 0; j < count; ++j)
                {
                    if (!EnemyLists.ContainsKey(EnemyPrefab[i].name))
                    {
                        List<GameObject> EnemyList = new List<GameObject>();
                        EnemyLists.Add(EnemyPrefab[i].name, EnemyList);
                    }

                    GameObject Enemy = Instantiate(EnemyPrefab[i]);
                    Enemy.name = typeof(T).Name;
                    Enemy.transform.position = new Vector2(_x, _y);
                    EnemyLists[EnemyPrefab[i].name].Add(Enemy);
                }
            }
        }
    }

    IEnumerator setDelay1<T>(float _delay)
    {
        while (true)
        {
            yield return null;

            if (EnemyLists.ContainsKey(typeof(T).Name))
            {
                for (int i = 0; i < EnemyLists[typeof(T).Name].Count; ++i)
                {
                    if (EnemyLists[typeof(T).Name][i].activeInHierarchy == true)
                    {
                        StartCoroutine(EnemyLists[typeof(T).Name][i].transform.GetComponent<smallEnemy1>().UpDown());
                        yield return new WaitForSeconds(_delay);
                    }
                    else
                        yield break;
                }
            }
        }
    }

    IEnumerator setDelay2<T>(float _delay)
	{
        while (true)
        {
            yield return null;
            
            if (EnemyLists.ContainsKey(typeof(T).Name))
            {
                for (int i = 0; i < EnemyLists[typeof(T).Name].Count; ++i)
                {
                    if (EnemyLists[typeof(T).Name][i].activeInHierarchy == true)
                    {
                        StartCoroutine(EnemyLists[typeof(T).Name][i].transform.GetComponent<smallEnemy2>().UpDown());
                        yield return new WaitForSeconds(_delay);
                    }
                    else
                        yield break;
                }
            }
        }
	}

    public void PopPooledObject<T>(int _count, Transform parent = null)
    {
        if (EnemyLists.ContainsKey(typeof(T).Name))
        {
            for (int i = 0; i < _count; ++i)
            {
                GameObject poolObject = EnemyLists[typeof(T).Name][i];
                EnemyLists[typeof(T).Name].RemoveAt(0);
                poolObject.transform.SetParent(null);
                poolObject.SetActive(true);
            }
        }
    }
}
