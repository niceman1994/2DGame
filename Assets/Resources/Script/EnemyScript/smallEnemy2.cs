using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct DirType
{
	public int type;
	public Vector3 direction;
}

public class smallEnemy2 : Object
{
	DirType dir;

	public override void Initialize()
	{
		base.Name = "smallEnemy2";
		base.Hp = 0;
		base.Speed = 2.0f;
		base.ObjectAnim = null;

		GetDirType();
	}

	// TODO : 추후 좌표 수정
	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(18.0f, transform.position.y, 0.0f), 0.01f);
			UpDown();
		}
	}

	public override void Release()
	{
		
	}

	private void OnBecameInvisible()
	{
		gameObject.SetActive(false);
		transform.SetParent(EnemyManager.Instance.transform);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
			EnemyManager.Instance.Score += 100;
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

	void UpDown()
	{
		if (dir.type == 1)
			transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, -5.2f, 0.0f), 0.2f);
		else if (dir.type == 2)
			transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, 5.2f, 0.0f), 0.2f);
	}
}
