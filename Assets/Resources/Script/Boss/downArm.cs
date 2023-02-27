using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class downArm : Object
{
	[SerializeField] private GameObject MissilePrefab;
	GameObject Missile;

	Animator animator;
	float time;

	public override void Initialize()
	{
		base.Name = "downArm";
		base.Hp = 200;
		base.Speed = 0.0f;
		base.ObjectAnim = GetComponent<Animator>();
		ObjectAnim.enabled = false;

		animator = transform.parent.GetChild(5).GetComponent<Animator>();
		animator.enabled = false;
		time = 0.0f;
	}

	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (transform.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize)
			{
				if (time <= 6.0f)
					time += Time.deltaTime;
				else
				{
					MissileEject();
					time = 0.0f;

					if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && Hp > 0)
						ObjectAnim.Play("downArms", -1, 1.0f);
				}
			}
		}
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
				animator.enabled = true;
			}
		}
	}

	void MissileEject()
	{
		if (Hp > 0)
		{
			ObjectAnim.enabled = true;

			if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
			{
				for (int i = 0; i < 6; ++i)
				{
					Missile = Instantiate(MissilePrefab);
					Missile.name = "BossMissile";
					Missile.transform.position = transform.position;
				}
			}
		}
	}
}
