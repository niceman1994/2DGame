using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private float Speed = 24.0f;
    [SerializeField] private float lifeTime = 0.75f;
    [SerializeField] private float _elapsedTime = 0f;

    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);

        if (GetTimer() > lifeTime)
            SetTimer();
    }

    float GetTimer()
    {
        return (_elapsedTime += Time.deltaTime);
    }

    void SetTimer()
    {
        _elapsedTime = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            SetTimer();
            ObjectPool.Instance.PushPooledObject("EnemyBullet", gameObject);
        }
    }
}
