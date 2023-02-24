using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class downArm : Object
{
	[SerializeField] private GameObject MissilePrefab;
	GameObject Missile;

	GameObject Player;
	Animator animator;
	float time;

	public override void Initialize()
	{
		base.Name = "downArm";
		base.Hp = 200;
		base.Speed = 0.0f;
		base.ObjectAnim = GetComponent<Animator>();
		ObjectAnim.enabled = false;
		Player = GameObject.FindGameObjectWithTag("Player");

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
				if (time <= 2.0f)
					time += Time.deltaTime;
				else
				{
					MissileEject();
					time = 0.0f;
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
				for (int i = 0; i < 10; ++i)
				{
					Missile = Instantiate(MissilePrefab);
					Missile.name = "BossMissile";
					Missile.transform.position = transform.position;
					Missile.transform.DOPath(new[] { Missile.transform.position,
						new Vector3(transform.localPosition.x + Random.Range(-2.0f, 2.0f), transform.localPosition.y - 0.1f, 0.0f) }, 2.0f, PathType.Linear).OnComplete(() =>
						{
							transform.DOPath(new[] { transform.position,
							new Vector3(Player.transform.position.x, Player.transform.position.y, 0.0f) }, 2.0f, PathType.CatmullRom).SetEase(Ease.Linear).SetAutoKill(true);
						}).SetAutoKill(true);
				}
			}
			else if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
				ObjectAnim.Play("downArms", -1, 1.0f);
		}
	}
}
