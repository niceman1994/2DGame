using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossMissile : Object
{
    GameObject Player;

    public override void Initialize()
    {
        base.Name = "BossMissile";
        base.Hp = 0;
        base.Speed = 2.5f;
        base.ObjectAnim = null;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Progress()
    {
        if (transform.position.x >= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
        {
            Vector3 Distance = (transform.position - Player.transform.position).normalized;
            transform.position -= new Vector3(Distance.x - (Speed * Time.deltaTime), Distance.y + Random.Range(-1.0f, 1.0f), Distance.z);
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
}
