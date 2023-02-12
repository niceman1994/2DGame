using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] EnemyPrefab;
    public List<GameObject> EnemyList = new List<GameObject>();

    void Start()
    {
        SpawnEnemy("smallEnemy1", 4);
    }

    void Update()
    {
        
    }

    void SpawnEnemy(string _name, int count)
    {
        for (int i = 0; i < EnemyPrefab.Length; ++i)
        {
            if (EnemyPrefab[i].name == _name)
            {
                for (int j = 0; j < count; ++j)
                {
                    GameObject Enemy = Instantiate(EnemyPrefab[i]);
                    Enemy.transform.position = new Vector3(transform.position.x + (j + 1), 2.2f, transform.position.z);
                    EnemyList.Add(Enemy);
                }
            }
        }
    }
}
