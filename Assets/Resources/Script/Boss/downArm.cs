using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downArm : Object
{
	[SerializeField] private GameObject MissilePrefab;
	GameObject Missile;

	Animator animator;

	public override void Initialize()
	{
		base.Name = "downArm";
		base.Hp = 200;
		base.Speed = 0.0f;
		base.ObjectAnim = GetComponent<Animator>();
		ObjectAnim.speed = 0;

		animator = transform.parent.GetChild(5).GetComponent<Animator>();
		animator.enabled = false;

		StartCoroutine(MissileEject());
	}

	public override void Progress()
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

	IEnumerator MissileEject()
	{
		WaitForSeconds waitForSeconds = new WaitForSeconds(3.5f);

		while (true)
		{
			yield return null;
			
			if (transform.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize &&
				transform.position.x >= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
			{
				if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("downArmsDestroy"))
				{
					ObjectAnim.enabled = false;
					break;
				}
				else if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("downArms"))
				{
					yield return waitForSeconds;

					if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.0f)
						ObjectAnim.speed = 1.0f;
					else if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.66f &&
						!ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("downArmsDestroy"))
					{
						for (int i = 0; i < 6; ++i)
						{
							Missile = Instantiate(MissilePrefab);
							Missile.name = "BossMissile";
							Missile.transform.position = new Vector2(transform.position.x + Random.Range(-3.0f, 3.0f), transform.position.y - 1.2f);
						}

						ObjectAnim.SetBool("end", true);
					}
				}
				else if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("downArms 0"))
				{
					yield return waitForSeconds;
					ObjectAnim.SetBool("end", false);
				}
			}
		}
	}

    
}
