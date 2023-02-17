using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class greenEnemy : Object
{
	[SerializeField] private Animator[] animators;

	public override void Initialize()
	{
		base.Name = "greenEnemy";
		base.Hp = 100;
		base.Speed = 3.0f;
		base.ObjectAnim = GetComponent<Animator>();

		StartCoroutine(UpDown());
	}

	public override void Progress()
	{
		ZeroHp();
	}

	public override void Release()
	{
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			Hp -= 10;

			if (Hp <= 0)
			{
				Hp = 0;
				transform.GetComponent<BoxCollider2D>().enabled = false;
				transform.DOPath(new[] { transform.position,
					new Vector3(transform.position.x + 4.0f, -3.0f, 0.0f)}, 2.0f, PathType.Linear).SetEase(Ease.Linear);

				GameManager.Instance.Score += Random.Range(25, 30) * 10;

				for (int i = 0; i < transform.childCount; ++i)
				{
					animators[i].enabled = true;
					animators[i].speed = 1;
				}
			}
		}
	}

	void ZeroHp()
	{
		if (Hp > 0)
		{
			for (int i = 0; i < transform.childCount; ++i)
			{
				animators[i].enabled = false;
				animators[i].speed = 0;
			}
		}
	}

	// TODO : 추후 수정
	public IEnumerator UpDown()
	{
		yield return null;

		WaitForSeconds waitForSeconds = new WaitForSeconds(5.0f);

		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			yield return waitForSeconds;

			transform.DOMoveX(transform.position.x - 22.0f, 3.0f).SetEase(Ease.Linear).OnComplete(() =>
			{
				transform.DOPath(new[] { new Vector3(transform.position.x + 8.0f, transform.position.y + 3.0f, 0.0f),
					new Vector3(transform.position.x + 8.0f, transform.position.y + 5.0f, 0.0f),
					new Vector3(transform.position.x - 1.0f, transform.position.y + 5.0f, 0.0f) }, 3.0f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
				{
					ObjectAnim.speed = 1;
				}).SetDelay(1.0f).SetLoops(-1);
			});
		}
	}}
