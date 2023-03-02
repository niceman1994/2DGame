using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downMissile : Object
{
	Animator animator;

	public override void Initialize()
	{
		base.Name = "downMissile";
		base.Hp = 120;
		base.Speed = 0.0f;
		base.ObjectAnim = GetComponent<Animator>();
		ObjectAnim.speed = 0;

		animator = transform.parent.GetChild(6).GetComponent<Animator>();
		animator.enabled = false;
	}

	public override void Progress()
	{
		if (transform.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize &&
			transform.position.x >= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
		{
			ObjectAnim.speed = 1;
		}
	}

	public override void Release()
	{
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			Hp -= 10;
			SoundManager.Instance.PlaySE("hitSound");

			if (Hp <= 0)
			{
				Hp = 0;
				ObjectAnim.enabled = true;
				ObjectAnim.SetTrigger("destroy");
				transform.GetComponent<PolygonCollider2D>().enabled = false;
				animator.enabled = true;
			}
		}
	}
}
