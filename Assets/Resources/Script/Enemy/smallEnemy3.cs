using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class smallEnemy3 : Object
{
	DirType dir;
	GameObject Player;
	bool inScene;

	public override void Initialize()
	{
		base.Name = "smallEnemy3";
		base.Hp = 0;
		base.Speed = 2.0f;
		base.ObjectAnim = gameObject.GetComponent<Animator>();
		ObjectAnim.speed = 0;

		Player = GameObject.FindGameObjectWithTag("Player");
		inScene = false;
		GetDirType(1.8f, -1.8f);
	}

	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (transform.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize)
				inScene = true;
			else
			{
				if (inScene == true)
					gameObject.SetActive(false);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			ObjectAnim.SetTrigger("destroy");
			transform.GetComponent<BoxCollider2D>().enabled = false;
			SoundManager.Instance.PlaySE("smallEnemyDestroySound");
			GameManager.Instance.Score += Random.Range(2, 3) * 10;

			StartCoroutine(ReturnObject());
		}
	}

	void GetDirType(float y1, float y2)
	{
		if (transform.position.y >= y1)
		{
			dir.type = 1;
			dir.pos = transform.position;
		}

		if (transform.position.y <= -y2)
		{
			dir.type = 2;
			dir.pos = transform.position;
		}
	}

	public IEnumerator UpDown()
	{
		yield return null;

		WaitForSeconds waitForSeconds = new WaitForSeconds(27.0f);

		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (dir.type == 1)
			{
				yield return waitForSeconds;

				if (gameObject.activeInHierarchy == true)
				{
					ObjectAnim.speed = 1;

					transform.DOPath(new[] {transform.position,
						new Vector3(28.0f, transform.position.y, 0.0f),
						new Vector3(Player.transform.position.x + 4.0f, transform.position.y, 0.0f),
						new Vector3(34.0f, -transform.position.y, 0.0f) }, 3.0f, PathType.CatmullRom).SetEase(Ease.Linear);
				}
				else
				{
					ObjectAnim.speed = 0;
					transform.DOKill();
				}
			}

			if (dir.type == 2)
			{
				yield return waitForSeconds;

				if (gameObject.activeInHierarchy == true)
				{
					ObjectAnim.speed = 1;

					transform.DOPath(new[] {transform.position,
						new Vector3(28.0f, transform.position.y, 0.0f),
						new Vector3(Player.transform.position.x + 4.0f, transform.position.y, 0.0f),
						new Vector3(34.0f, -transform.position.y, 0.0f) }, 3.0f, PathType.CatmullRom).SetEase(Ease.Linear);
				}
				else
				{
					ObjectAnim.speed = 0;
					transform.DOKill();
				}
			}
		}
	}

	IEnumerator ReturnObject()
	{
		while (true)
		{
			yield return null;

			if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
				gameObject.SetActive(false);
			else if (gameObject.activeInHierarchy == false) break;
		}
	}
}
