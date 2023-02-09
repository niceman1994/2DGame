using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallEnemyController : Object
{
	public override void Initialize()
	{
		base.name = "smallEnemy";
		base.Hp = 0;
		base.Speed = 10.0f;
		base.ObjectAnim = GetComponent<Animator>();
	}

	public override void Progress()
	{
		
	}

	public override void Release()
	{
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Bullet")
			GameManager.Instance.Score += 10;
	}
}
