using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyManager : ManagerSingleton<EnemyManager>
{
    public GameObject BullterPrefab;
    GameObject bullet;

    public Text ScoreText;
    public int Score;

    public GameObject[] EnemyPrefab;
    public List<GameObject> EnemyList = new List<GameObject>();

    int randomBullet;
    float attackDelay;

	void Start()
    {
        SpawnEnemy("smallEnemy1", 4, new Vector3(46.0f, 2.1f, 0.0f));
        SpawnEnemy("smallEnemy1", 4, new Vector3(50.0f, -2.1f, 0.0f));
        SpawnEnemy("smallEnemy2", 6, new Vector3(32.0f, 4.1f, 0.0f));
        SpawnEnemy("smallEnemy2", 6, new Vector3(32.0f, -4.1f, 0.0f));
        ScoreText.text = "00";

        randomBullet = Random.Range(0, EnemyList.Count);
        attackDelay = 0.0f;
    }

	private void Update()
	{
        if (Score != 0)
            ScoreText.text = Score.ToString();

        //RandomAttack();
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
                    Enemy.name = _name;
                    Enemy.transform.position = new Vector3(_position.x + (j + 2), _position.y, 0.0f);
                    EnemyList.Add(Enemy);
                }
            }
        }
    }
    
    public void RandomAttack()
	{
        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
        {
            attackDelay += Time.deltaTime;

            for (int i = 0; i < EnemyList.Count; ++i)
            {
                if (i == randomBullet && attackDelay >= 6.5f)
				{
                    //randomBullet = Random.Range(0, EnemyList.Count);
                    //Debug.Log(randomBullet);
                    bullet = Instantiate(BullterPrefab);
                    bullet.name = "EnemyBullet";
                    bullet.transform.position = new Vector3(
                        EnemyList[i].transform.position.x - 0.15f,
                        EnemyList[i].transform.position.y,
                        EnemyList[i].transform.position.z);
                }
            }
        }
	}
}
