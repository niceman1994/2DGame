using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upMissile : Object
{
	[SerializeField] private GameObject bulletPrefab;

	Animator animator;
	float attackDelay;

	public override void Initialize()
	{
		base.Name = "upMissile";
		base.Hp = 80;
		base.Speed = 0.0f;
		base.ObjectAnim = GetComponent<Animator>();
		ObjectAnim.enabled = false;

		animator = transform.parent.GetChild(7).GetComponent<Animator>();
		animator.enabled = false;
		attackDelay = 0.0f;
	}

	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (transform.parent.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize &&
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
				transform.GetComponent<PolygonCollider2D>().enabled = false;
				animator.enabled = true;
			}
		}
	}

	void StartAnim()
	{
		if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("upMissile"))
		{
			ObjectAnim.enabled = true;

			if (attackDelay <= 3.0f)
				attackDelay += Time.deltaTime;
			else if (attackDelay > 3.0f && ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.66f)
			{
				GameObject bullet = Instantiate(bulletPrefab);
				bullet.transform.position += new Vector3(transform.position.x - 0.5f - (3.0f * Time.deltaTime),
					transform.position.y + 0.5f, 0.0f);

				attackDelay = 0.0f;
			}
		}
	}
}
