using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Object 
{
    [SerializeField] private float lifeTime = 0.75f;
    [SerializeField] private float _elapsedTime = 0f;

	public override void Initialize()
	{
		base.Name = "Bullet";
        base.Hp = 0;
        base.Speed = 24.0f;
        base.ObjectAnim = null;
    }

	public override void Progress()
	{
        transform.Translate(Vector2.right * Speed * Time.deltaTime);

        if (GetTimer() > lifeTime)
        {
            SetTimer();
            ObjectPool.Instance.PushPooledObject("Bullet", gameObject);
        }
    }

	public override void Release()
	{
        
    }

    float GetTimer()
    {
        return (_elapsedTime += Time.deltaTime);
    }

    void SetTimer()
    {
        _elapsedTime = 0f;
    }
}
