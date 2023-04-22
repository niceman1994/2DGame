using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWeapon2 : Object
{
	[SerializeField] private GameObject bulletPrefab;

	public override void Initialize()
	{
		base.Name = "SideWeapon2";
		base.Hp = 80;
		base.Speed = 0.0f;
		base.ObjectAnim = GetComponent<Animator>();

		ObjectAnim.speed = 0;
		StartCoroutine(BulletEject());
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
				transform.GetComponent<BoxCollider2D>().enabled = false;
			}
		}
	}

	IEnumerator BulletEject()
	{
		WaitForSeconds waitForSeconds = new WaitForSeconds(4.0f);

		while (true)
		{
			yield return null;

			if (transform.parent.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize + 2.0f &&
				transform.parent.position.y <= 1.0f)
			{
				yield return waitForSeconds;
				ObjectAnim.speed = 1.0f;

				if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Weapon2Destroy"))
				{
					ObjectAnim.enabled = false;
					break;
				}
				else if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Weapon2"))
				{
					if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.66f &&
						!ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Weapon2Destroy"))
					{
						for (int i = 0; i < 3; ++i)
						{
							GameObject bullet = Instantiate(bulletPrefab);
							bullet.name = "BossBullet";
							bullet.transform.position = new Vector3(
								transform.position.x - 1.5f - i, transform.position.y, 0.0f);
						}

						ObjectAnim.SetBool("end", true);
					}
				}
				else if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Weapon2 0"))
				{
					yield return Random.Range(1.0f, 3.0f);
					ObjectAnim.SetBool("end", false);
				}
			}
		}
	}
}
