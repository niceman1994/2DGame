using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private string Name;
    [SerializeField] private float Speed;

	void Update()
    {
        DestroyBullet();
        transform.Translate(Vector2.left * Speed * Time.deltaTime);

        if (transform.position.x <= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
        {
            gameObject.SetActive(false);
            transform.SetParent(ObjectPool.Instance.transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            gameObject.SetActive(false);
            transform.SetParent(ObjectPool.Instance.transform);
        }
    }

    void DestroyBullet()
	{
        if (transform.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize &&
            transform.position.x > Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
        {
            if (GameManager.Instance.PlayerCharge == true)
            {
                gameObject.SetActive(false);
                transform.SetParent(ObjectPool.Instance.transform);
            }
        }
	}
}
