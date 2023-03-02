using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Object
{
	public override void Initialize()
	{
		base.Name = "BossBullet";
		base.Hp = 0;
		base.Speed = 2.0f;
		base.ObjectAnim = null;
	}

	public override void Progress()
	{
		if (transform.position.x >= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
		{
			transform.position += new Vector3(
				transform.position.x - (Speed * 1.2f * Time.deltaTime),
				transform.position.y, 0.0f);
		}
		else
		{
			gameObject.SetActive(false);
			transform.GetComponent<BoxCollider2D>().enabled = false;
			transform.SetParent(EnemyManager.Instance.transform.GetChild(1));
		}
	}

	public override void Release()
	{
		
	}
}
