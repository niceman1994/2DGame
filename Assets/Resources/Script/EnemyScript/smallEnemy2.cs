using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public struct DirType
{
	public int type;
	public Vector3 direction;
}

public class smallEnemy2 : Object
{
	public DirType dir;
	bool CheckScene;

	public override void Initialize()
	{
		base.Name = "smallEnemy2";
		base.Hp = 0;
		base.Speed = 2.0f;
		base.ObjectAnim = GetComponent<Animator>();

		CheckScene = false;
		GetDirType();
		StartCoroutine(UpDown());
	}

	// TODO : 추후 좌표 수정
	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			CheckScene = true;
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
			EnemyManager.Instance.Score += 100;
		}
	}

	void GetDirType()
	{
		if (transform.position.y >= 4.0f)
		{
			dir.type = 1;
			dir.direction = new Vector3(transform.position.x, transform.position.y - (Speed * Time.deltaTime), 0.0f);
		}
		else if (transform.position.y <= -4.0f)
		{
			dir.type = 2;
			dir.direction = new Vector3(transform.position.x, transform.position.y + (Speed * Time.deltaTime), 0.0f);
		}
	}

	IEnumerator UpDown()
	{
		while (true)
		{
			yield return null;

			if (CheckScene == true)
			{
				if (dir.type == 1)
				{
					yield return new WaitForSeconds(15.0f);
					
					transform.DOPath(
						new[] { transform.position, new Vector3(transform.position.x - 12.0f, transform.position.y, 0.0f),
						new Vector3(transform.position.x - 6.0f, 0.0f, 0.0f),
						new Vector3(transform.position.x + 8.0f, -5.2f, 0.0f) }, 10.0f, PathType.CatmullRom).SetEase(Ease.Linear);
				}
				
				if (dir.type == 2)
				{
					yield return new WaitForSeconds(15.0f);
				
					transform.DOPath(
						new[] { transform.position, new Vector3(transform.position.x - 12.0f, transform.position.y, 0.0f),
						new Vector3(transform.position.x - 6.0f, 0.0f, 0.0f),
						new Vector3(transform.position.x + 8.0f, 5.2f, 0.0f) }, 10.0f, PathType.CatmullRom).SetEase(Ease.Linear);
				}

				yield return null;
				CheckScene = false;
				break;
			}
		}
	}
}
