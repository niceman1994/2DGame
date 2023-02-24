using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyManager : ManagerSingleton<EnemyManager>
{
    public GameObject[] EnemyPrefab;
    public GameObject BullterPrefab;

    public Dictionary<object, List<GameObject>> EnemyLists = new Dictionary<object, List<GameObject>>();
    public List<GameObject> BulletLists = new List<GameObject>();
    
    void Start()
    {
        Initialize();

        StartCoroutine(setDelay1<smallEnemy1>(0.195f, 0, 8));
        StartCoroutine(setDelay1<smallEnemy1>(0.195f, 8, 12));
        StartCoroutine(setDelay1<smallEnemy1>(0.195f, 12, 16));
        StartCoroutine(setDelay1<smallEnemy1>(0.195f, 16, 20));
        StartCoroutine(setDelay1<smallEnemy1>(0.195f, 20, 24));
        StartCoroutine(setDelay2<smallEnemy2>(0.45f));
        StartCoroutine(setDelay3<smallEnemy3>(0.5f));
        StartCoroutine(setDelay4<smallEnemy4>(1.0f));
        StartCoroutine(setDelay5<smallEnemy5>(0.5f));
    }

	void Initialize()
	{
        SpawnEnemy<smallEnemy1>(4, new Vector2(46.0f, 2.4f));
        SpawnEnemy<smallEnemy1>(4, new Vector2(50.0f, -2.4f));
        
        for (int i = 0; i < 4; ++i)
            SpawnEnemy<smallEnemy1>(4, new Vector2(86.0f, 0.0f - i));

        for (int i = 0; i < 10; ++i)
        {
            SpawnEnemy<smallEnemy2>(1, new Vector2(34.0f, 4.1f));
            SpawnEnemy<smallEnemy2>(1, new Vector2(34.0f, -4.1f));
        }

        for (int i = 0; i < 8; ++i)
		{
            SpawnEnemy<smallEnemy3>(1, 38.0f, 1.8f);
            SpawnEnemy<smallEnemy3>(1, 38.0f, -1.8f);
        }

        for (int i = 0; i < 10; ++i)
            SpawnEnemy<smallEnemy4>(1, 36.0f, -4.4f);

        SpawnEnemy<smallEnemy5>(5, new Vector2(36.0f, Random.Range(1.5f, 1.8f)));
        SpawnEnemy<smallEnemy5>(5, new Vector2(36.0f, Random.Range(-1.5f, -1.8f)));

        SpawnEnemy<Boss>(1, 40.0f, 6.33f);
    }

    void SpawnEnemy<T>(int count, Vector2 pos)
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

                    GameObject Enemy = Instantiate(EnemyPrefab[i], transform);
                    Enemy.name = typeof(T).Name;
                    Enemy.transform.position = new Vector2(pos.x + (j * 2), pos.y);
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

                    GameObject Enemy = Instantiate(EnemyPrefab[i], transform);
                    Enemy.name = typeof(T).Name;
                    Enemy.transform.position = new Vector2(_x + (j * 2), _y);
                    EnemyLists[EnemyPrefab[i].name].Add(Enemy);
                }
            }
        }
    }

    IEnumerator setDelay1<T>(float delay, int startcount, int endcount)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        while (true)
        {
            yield return null;

            if (EnemyLists.ContainsKey(typeof(T).Name))
            {
                for (int i = startcount; i < endcount; ++i)
                {
                    if (EnemyLists[typeof(T).Name][i].activeInHierarchy == true)
                    {
                        StartCoroutine(EnemyLists[typeof(T).Name][i].transform.GetComponent<smallEnemy1>().UpDown());
                        yield return waitForSeconds;
                    }
                    else
                        yield break;
                }
            }
        }
    }

    IEnumerator setDelay2<T>(float delay)
	{
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

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
                        yield return waitForSeconds;
                    }
                    else
                        yield break;
                }
            }
        }
	}

    IEnumerator setDelay3<T>(float delay)
	{
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        while (true)
		{
            yield return null;

            if (EnemyLists.ContainsKey(typeof(T).Name))
            {
                for (int i = 0; i < EnemyLists[typeof(T).Name].Count; ++i)
                {
                    if (EnemyLists[typeof(T).Name][i].activeInHierarchy == true)
                    {
                        StartCoroutine(EnemyLists[typeof(T).Name][i].transform.GetComponent<smallEnemy3>().UpDown());
                        yield return waitForSeconds;
                    }
                    else
                        yield break;
                }
            }
        }
    }

    IEnumerator setDelay4<T>(float delay)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        while (true)
        {
            yield return null;

            if (EnemyLists.ContainsKey(typeof(T).Name))
            {
                for (int i = 0; i < EnemyLists[typeof(T).Name].Count; ++i)
                {
                    if (EnemyLists[typeof(T).Name][i].activeInHierarchy == true)
                    {
                        StartCoroutine(EnemyLists[typeof(T).Name][i].transform.GetComponent<smallEnemy4>().UpDown());
                        yield return waitForSeconds;
                    }
                    else
                        yield break;
                }
            }
        }
    }

    IEnumerator setDelay5<T>(float delay)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        while (true)
        {
            yield return null;

            if (EnemyLists.ContainsKey(typeof(T).Name))
            {
                for (int i = 0; i < EnemyLists[typeof(T).Name].Count; ++i)
                {
                    if (EnemyLists[typeof(T).Name][i].activeInHierarchy == true)
                    {
                        StartCoroutine(EnemyLists[typeof(T).Name][i].transform.GetComponent<smallEnemy5>().UpDown());
                        yield return waitForSeconds;
                    }
                    else
                        yield break;
                }
            }
        }
    }

    void SetnewPos<T>(int count, Vector2 pos)
	{
        if (EnemyLists.ContainsKey(typeof(T).Name))
		{
            for (int i = 0; i < count; ++i)
                EnemyLists[typeof(T).Name][i].transform.position = new Vector2(pos.x + (i * 2), pos.y);
		}
	}
}
