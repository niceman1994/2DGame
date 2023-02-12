using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour 
{
    [SerializeField] private string Name;
    [SerializeField] private float Speed = 24.0f;
    [SerializeField] private float lifeTime = 0.75f;
    [SerializeField] private float _elapsedTime = 0f;

    void Update()
	{
        transform.Translate(Vector2.right * Speed * Time.deltaTime);

        if (GetTimer() > lifeTime)
        {
            SetTimer();
            ObjectPool.Instance.PushPooledObject("Bullet", gameObject);
        }
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
        if (collision.gameObject.tag == "Enemy")
            ObjectPool.Instance.PushPooledObject("Bullet", gameObject);
    }
}
