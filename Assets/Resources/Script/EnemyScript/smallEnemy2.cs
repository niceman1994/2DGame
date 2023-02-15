using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class smallEnemy2 : Object
{
	DirType dir;

	GameObject Player;
	bool inScene;

	public override void Initialize()
	{
		base.Name = "smallEnemy2";
		base.Hp = 0;
		base.Speed = 2.0f;
		base.ObjectAnim = gameObject.GetComponent<Animator>();

		Player = GameObject.FindGameObjectWithTag("Player");
		inScene = false;
		GetDirType(3.9f, -3.9f);
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
				{
					gameObject.SetActive(false);
					transform.SetParent(EnemyManager.Instance.transform);
				}
			}
		}
	}

	public override void Release()
	{
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			ObjectAnim.SetTrigger("destroy");
			transform.GetComponent<BoxCollider2D>().enabled = false;
			SoundManager.Instance.PlaySE("smallEnemyDestroySound");
			EnemyManager.Instance.Score += Random.Range(8, 11) * 10;

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

		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (dir.type == 1)
			{
				yield return new WaitForSeconds(12.0f);

				if (gameObject.activeInHierarchy == true)
				{
					transform.DOPath(
						new[] { transform.position, new Vector3(transform.position.x - 14.0f, transform.position.y, 0.0f),
						new Vector3(Player.transform.position.x + 2.0f, transform.position.y - 4.1f, 0.0f),
						new Vector3(transform.position.x + 8.0f, transform.position.y - 5.2f, 0.0f) }, 5.0f, PathType.CatmullRom).SetEase(Ease.Linear);
				}
				else
					transform.DOKill();
			}

			if (dir.type == 2)
			{
				yield return new WaitForSeconds(12.0f);

				if (gameObject.activeInHierarchy == true)
				{
					transform.DOPath(
						new[] { transform.position, new Vector3(transform.position.x - 14.0f, transform.position.y, 0.0f),
						new Vector3(Player.transform.position.x + 2.0f, transform.position.y + 4.1f, 0.0f),
						new Vector3(transform.position.x + 8.0f,transform.position.y + 5.2f, 0.0f) }, 5.0f, PathType.CatmullRom).SetEase(Ease.Linear);
				}
				else
					transform.DOKill();
			}
		}
	}

	IEnumerator ReturnObject()
	{
		while (true)
		{
			yield return null;

			if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
			{
				gameObject.SetActive(false);
				transform.SetParent(EnemyManager.Instance.transform);
			}
		}
	}
}
