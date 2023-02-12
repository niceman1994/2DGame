using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class smallEnemyController : Object
{
	Vector3 upPos;
	Vector3 downPos;

	public override void Initialize()
	{
		base.name = "smallEnemy";
		base.Hp = 0;
		base.Speed = 1.2f;
		base.ObjectAnim = GetComponent<Animator>();

		upPos = new Vector3(transform.position.x - 1.0f, transform.position.y + 0.3f, 0.0f);
		downPos = new Vector3(transform.position.x - 1.0f, transform.position.y - 0.3f, 0.0f);
		Move();
	}

	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			transform.position = new Vector3(
				transform.position.x - (Speed * Time.deltaTime), 
				transform.position.y, 0.0f);
		}
	}

	public override void Release()
	{

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Bullet")
			ObjectAnim.SetTrigger("destroy");
    }


	// TODO : 위아래로 움직이는 코드할 수 있으면 해볼 것
	void Move()
    {
		transform.DOPath(new[] { upPos, transform.position }, 2.0f, PathType.CatmullRom).OnComplete(() =>
		{
			Vector3 tempPos = new Vector3(transform.position.x - 1.0f, transform.position.y, 0.0f);
			transform.DOPath(new[] { downPos, tempPos }, 2.0f, PathType.CatmullRom);
		}).SetEase(Ease.Linear).SetLoops(-1);
    }
}
