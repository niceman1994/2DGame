using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallEnemyController : Object
{
	public override void Initialize()
	{
		base.name = "smallEnemy";
		base.Hp = 0;
		base.Speed = 15.0f;
		base.ObjectAnim = GetComponent<Animator>();
	}

	private void LateUpdate()
	{
		transform.position = new Vector3(36.0f - (Speed * Time.deltaTime), Random.Range(-3.15f, 3.15f), 0.0f);
	}

	public override void Progress()
	{
		//transform.Translate(Vector2.left * Speed * Time.deltaTime);

		ActiveEnemy();
	}

	public override void Release()
	{

	}

	void ActiveEnemy()
	{
		if (transform.position.x <= 22.0f + BackgroundManager.Instance.xScreenHalfSize)
			ObjectPool.Instance.PopPooledObject("smallEnemy1");
		else if (transform.position.x < 22.0f - BackgroundManager.Instance.xScreenHalfSize)
			ObjectPool.Instance.PushPooledObject("smallEnemy1", gameObject);
	}
}
