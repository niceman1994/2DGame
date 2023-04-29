using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Object
{
	public override void Initialize()
	{
		base.Name = "BossBullet";
		base.Hp = 0;
		base.Speed = 1.0f;
		base.ObjectAnim = null;
	}

	public override void Progress()
	{
		if (transform.position.x >= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
		{
			transform.position = Vector2.MoveTowards(transform.position,
				new Vector2(Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize - 1.0f, transform.position.y), 0.0125f);
		}
		else
		{
			gameObject.SetActive(false);
			transform.GetComponent<BoxCollider2D>().enabled = false;
			transform.SetParent(EnemyManager.Instance.transform.GetChild(1));
		}
	}
}
