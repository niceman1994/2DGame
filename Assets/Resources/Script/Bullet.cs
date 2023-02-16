using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    [SerializeField] private string Name;
    [SerializeField] private float Speed = 24.0f;

	void Update()
	{
        transform.Translate(Vector2.right * Speed * Time.deltaTime);

        if (transform.position.x >= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize)
            ObjectPool.Instance.PushPooledObject<Bullet>(gameObject);
    }

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            ObjectPool.Instance.PushPooledObject<Bullet>(gameObject);
        else if (collision.gameObject.tag == "ItemMob")
            ObjectPool.Instance.PushPooledObject<Bullet>(gameObject);
    }
} 
