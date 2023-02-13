using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : ManagerSingleton<EnemyManager>
{
    public Text ScoreText;
    public int Score;

    public GameObject[] EnemyPrefab;
    public List<GameObject> EnemyList = new List<GameObject>();

	void Start()
    {
        SpawnEnemy("smallEnemy1", 4, new Vector3(46.0f, 2.1f, 0.0f));
        SpawnEnemy("smallEnemy1", 4, new Vector3(50.0f, -2.1f, 0.0f));
    }

	private void Update()
	{
        ScoreText.text = Score.ToString();
    }

	void SpawnEnemy(string _name, int count, Vector3 _position)
    {
        for (int i = 0; i < EnemyPrefab.Length; ++i)
        {
            if (EnemyPrefab[i].name == _name)
            {
                for (int j = 0; j < count; ++j)
                {
                    GameObject Enemy = Instantiate(EnemyPrefab[i]);
                    Enemy.transform.position = new Vector3(_position.x + (j + 1), _position.y, 0.0f);
                    EnemyList.Add(Enemy);
                }
            }
        }
    }
}
