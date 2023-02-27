using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossBullet : Object
{
    GameObject Player;

    public override void Initialize()
    {
        base.Name = "BossBullet";
        base.Hp = 0;
        base.Speed = 2.5f;
        base.ObjectAnim = null;

        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Move());
    }

    public override void Progress()
    {
        if (transform.position.x >= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
        {
            Vector3 Distance = (transform.position - Player.transform.position).normalized;
            transform.position += Distance;
        }
        else
        {
            gameObject.SetActive(false);
            transform.SetParent(EnemyManager.Instance.transform.GetChild(1));
        }
    }

    public override void Release()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
            transform.GetComponent<BoxCollider2D>().enabled = false;
            transform.SetParent(EnemyManager.Instance.transform);
        }
    }
    
    IEnumerator Move()
    {
        yield return null;

        transform.DOPath(new[] { transform.position,
            new Vector3(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y - 1.0f, 0.0f) }, 2.0f, PathType.Linear).SetAutoKill(true);
    }
}
