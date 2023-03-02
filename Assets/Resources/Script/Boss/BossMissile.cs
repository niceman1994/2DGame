using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(
                Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize - 1.0f,
                Random.Range(Player.transform.position.y - 1.0f, Player.transform.position.y + 1.0f)), 0.04f);

            Vector3 Direction = (transform.position - Player.transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(new Vector3(0.0f, 0.0f, Direction.z));
        }
        else
        {
            gameObject.SetActive(false);
            transform.GetComponent<BoxCollider2D>().enabled = false;
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
