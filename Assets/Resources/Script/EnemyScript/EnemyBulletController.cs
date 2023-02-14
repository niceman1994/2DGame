using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private string Name;
    [SerializeField] private float Speed;

    void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);

        if (transform.position.x <= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            gameObject.SetActive(false);
    }
}
