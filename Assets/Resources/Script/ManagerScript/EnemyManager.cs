using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyManager : ManagerSingleton<EnemyManager>
{
    public Text ScoreText;
    public int Score;

    public GameObject[] EnemyPrefab;
    public Dictionary<object, List<GameObject>> EnemyLists = new Dictionary<object, List<GameObject>>();

	void Start()
    {
        SpawnEnemy<smallEnemy1>(4, new Vector2(46.0f, 2.1f));
        SpawnEnemy<smallEnemy1>(4, new Vector2(50.0f, -2.1f));

        for (int i = 0; i < 10; ++i)
        {
            SpawnEnemy<smallEnemy2>(1, 32.0f + (i + 4), 4.1f);
            SpawnEnemy<smallEnemy2>(1, 32.0f + (i + 4), -4.1f);
        }

        ScoreText.text = "00";

        //StartCoroutine(setDelay<smallEnemy1>(2.0f));
        StartCoroutine(setDelay1<smallEnemy1>(2.0f));
        StartCoroutine(setDelay2<smallEnemy2>(1.0f));
    }

	private void Update()
	{
        if (Score != 0)
            ScoreText.text = Score.ToString();
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
                    Enemy.transform.position = new Vector2(_position.x + (j + 2), _position.y + Random.Range(-0.2f, 0.2f));
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

            //if (EnemyLists.ContainsKey(typeof(T).Name))
            //{
            //    for (int i = 0; i < EnemyLists[typeof(T).Name].Count; ++i)
            //    {
            //        StartCoroutine(EnemyLists[typeof(T).Name][i].transform.GetComponent<smallEnemy1>().UpDown());
            //        //EnemyLists[_name][i].transform.GetComponent<smallEnemy1>().EnemyAttack();
            //        yield return new WaitForSeconds(_delay);
            //    }
            //}
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
                    StartCoroutine(EnemyLists[typeof(T).Name][i].transform.GetComponent<smallEnemy2>().UpDown());
                    yield return new WaitForSeconds(_delay);                    
                }
            }
        }
	}
}
