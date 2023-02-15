using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallEnemy3 : Object
{
	DirType dir;

	public override void Initialize()
	{
		base.Name = "smallEnemy3";
		base.Hp = 0;
		base.Speed = 2.0f;
		base.ObjectAnim = gameObject.GetComponent<Animator>();
	}

	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
        {

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
