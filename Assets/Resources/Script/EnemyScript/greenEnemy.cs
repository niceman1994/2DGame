using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class greenEnemy : Object
{
	public override void Initialize()
	{
		base.Name = "greenEnemy";
		base.Hp = 100;
		base.Speed = 3.0f;
		base.ObjectAnim = GetComponent<Animator>();
		ObjectAnim.speed = 0;
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
			Hp -= 10;
	}

	void ZeroHp()
	{
		if (Hp <= 0)
		{
			Hp = 0;
			GameManager.Instance.Score += Random.Range(25, 30) * 10;
		}
	}

	public IEnumerator UpDown()
	{
		yield return null;

		WaitForSeconds waitForSeconds = new WaitForSeconds(65.0f);

		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			yield return waitForSeconds;

			transform.DOMoveX(transform.position.x - 12.0f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
			{
				transform.DOPath(new[] { new Vector3(transform.position.x - 7.0f, transform.position.y + 4.0f, 0.0f),
				new Vector3(transform.position.x - 2.0f, transform.position.y + 7.0f, 0.0f),
				new Vector3(transform.position.x - 10.0f, transform.position.y + 7.0f, 0.0f)}, 3.0f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
				{
					ObjectAnim.speed = 1;
				}).SetLoops(-1);
			});
		}
	}
}
