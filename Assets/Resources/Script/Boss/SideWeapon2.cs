using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWeapon2 : Object
{
	[SerializeField] private GameObject bulletPrefab;

	float attackDelay;

	public override void Initialize()
	{
		base.Name = "SideWeapon2";
		base.Hp = 80;
		base.Speed = 0.0f;
		base.ObjectAnim = GetComponent<Animator>();

		ObjectAnim.speed = 0;
		attackDelay = 0.0f;
	}

	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (transform.parent.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize + 2.0f &&
				transform.parent.position.y <= 1.0f)
				StartAnim();
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
				transform.GetComponent<BoxCollider2D>().enabled = false;
			}
		}
	}

	void StartAnim()
	{
		if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Weapon2"))
		{
			if (attackDelay <= 5.0f)
				attackDelay += Time.deltaTime;
			else
			{
				if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.0f)
					ObjectAnim.speed = 1;
				else if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.66f)
				{
					for (int i = 0; i < 3; ++i)
					{
						GameObject bullet = Instantiate(bulletPrefab);
						bullet.name = "BossBullet";
						bullet.transform.position = new Vector3(
							transform.position.x - 1.0f - i, transform.position.y, 0.0f);
					}

					attackDelay = 0.0f;
				}
			}
		}
	}
}
