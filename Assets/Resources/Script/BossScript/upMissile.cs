using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upMissile : Object
{
	Animator animator;

	public override void Initialize()
	{
		base.Name = "upMissile";
		base.Hp = 80;
		base.Speed = 0.0f;
		base.ObjectAnim = GetComponent<Animator>();
		ObjectAnim.enabled = false;

		animator = transform.parent.GetChild(5).GetComponent<Animator>();
		animator.enabled = false;
	}

	public override void Progress()
	{
		
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
				animator.enabled = true;
			}
		}
	}
}
