using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upMissile : Object
{
	[SerializeField] private GameObject bulletPrefab;

	Animator animator;

	public override void Initialize()
	{
		base.Name = "upMissile";
		base.Hp = 80;
		base.Speed = 0.0f;
		base.ObjectAnim = GetComponent<Animator>();
		ObjectAnim.speed = 0;

		animator = transform.parent.GetChild(7).GetComponent<Animator>();
		animator.enabled = false;

		StartCoroutine(StartAnim());
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
				animator.enabled = true;
			}
		}
	}

	IEnumerator StartAnim()
	{
		WaitForSeconds waitForSeconds = new WaitForSeconds(3.5f);

		while (true)
		{
			yield return null;

			if (transform.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize &&
				transform.position.x >= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize &&
				transform.position.y <= Camera.main.transform.position.y + BackgroundManager.Instance.yScreenHalfSize)
			{
				if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("upMissile"))
				{
					yield return waitForSeconds;

					if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.0f)
						ObjectAnim.speed = 1.0f;
					else if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
					{
						for (int i = 0; i < 4; ++i)
						{
							GameObject bullet = Instantiate(bulletPrefab);
							bullet.name = "BossMissle";
							bullet.transform.position = new Vector2(transform.position.x - Random.Range(1.0f, 2.0f),
								transform.position.y + Random.Range(0.5f, 1.5f));
						}

						ObjectAnim.SetBool("end", true);
					}
				}
				else if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("upMissile 0"))
                {
					yield return null;
					ObjectAnim.SetBool("end", false);
				}
			}
			else if (Hp == 0) break;
		}
	}
}
