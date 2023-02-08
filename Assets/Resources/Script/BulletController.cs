using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Object 
{
	float BulletSpeed;

	private void Awake()
	{
		BulletSpeed = 0.125f;
	}

	public override void Initialize()
	{
		base.Name = "Bullet";
		base.Hp = 0;
		base.Atk = 10;
		base.ObjectAnim = null;
		base._Object = null;

		Invoke("DestroyBullet", 1.0f);
	}

	public override void Progress()
	{
		transform.Translate(Vector2.right * BulletSpeed);
	}

	public override void Release()
	{
		
	}

	public void DestroyBullet()
	{
		ObjectPool.ReturnObject(transform.gameObject);
	}
}
