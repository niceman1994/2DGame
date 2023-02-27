using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWeapon1 : Object
{
	public override void Initialize()
	{
		base.Name = "SideWeapon1";
		base.Hp = 100;
		base.Speed = 0.0f;
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
		if (collision.gameObject.CompareTag("Bullet"))
		{
			Hp -= 10;
			SoundManager.Instance.PlaySE("hitSound");

			if (Hp <= 0)
			{
				Hp = 0;
				ObjectAnim.enabled = true;
				ObjectAnim.SetTrigger("destroy");
				transform.GetComponent<PolygonCollider2D>().enabled = false;
			}
		}
	}
}
