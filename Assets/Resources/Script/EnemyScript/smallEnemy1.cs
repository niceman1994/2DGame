using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

internal struct DirType
{
	public int type;
	public Vector3 pos;
}

public class smallEnemy1 : Object
{
	[SerializeField] private GameObject BullterPrefab;
	[SerializeField] private GameObject BulletPoint;
	GameObject bullet;

	float attackDelay;
	float randomBullet;

	DirType dir;

	public override void Initialize()
	{
		base.name = "smallEnemy1";
		base.Hp = 0;
		base.Speed = 2.5f;
		base.ObjectAnim = gameObject.GetComponent<Animator>();

		randomBullet = Random.Range(2, 8);
		GetDirType(2.0f, -2.0f);
	}

	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (transform.position.x >= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
			{
				transform.position = new Vector3(transform.position.x - (Speed * Time.deltaTime), transform.position.y, 0.0f);

				if (transform.position.x < Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize)
					EnemyAttack();
			}
			else
			{
				gameObject.SetActive(false);
				transform.SetParent(EnemyManager.Instance.transform);
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
            GameManager.Instance.Score += Random.Range(5, 6) * 10;

            StartCoroutine(ReturnObject());
        }
    }

	void GetDirType(float y1, float y2)
    {
		if (transform.position.y >= y1)
			dir.type = 1;
		else if (transform.position.y <= -y2)
			dir.type = 2;
		else if (transform.position.y < y1 && transform.position.y > -y2)
			dir.type = 3;
    }

	public IEnumerator UpDown()
	{
		yield return null;
	
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (dir.type == 1)
			{
				if (gameObject.activeInHierarchy == true)
				{
					yield return new WaitForSeconds(4.0f);

					transform.DOPath(new[] { transform.position,
						new Vector3(transform.position.x - 1.0f, transform.position.y - 0.75f, 0.0f),
						new Vector3(transform.position.x - 2.0f, transform.position.y, 0.0f),
						new Vector3(transform.position.x - 3.0f, transform.position.y + 0.75f, 0.0f),
						new Vector3(transform.position.x - 4.0f, transform.position.y, 0.0f) }, 1.6f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
						{
							dir.pos = transform.position;
						});
				}
				else
					transform.DOKill();
			}
			else if (dir.type == 2)
			{
				if (gameObject.activeInHierarchy == true)
				{
					yield return new WaitForSeconds(4.0f);

					transform.DOPath(new[] { transform.position,
						new Vector3(transform.position.x - 1.0f, transform.position.y + 0.75f, 0.0f),
						new Vector3(transform.position.x - 2.0f, transform.position.y, 0.0f),
						new Vector3(transform.position.x - 3.0f, transform.position.y - 0.75f, 0.0f),
						new Vector3(transform.position.x - 4.0f, transform.position.y, 0.0f) }, 1.6f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
						{
							dir.pos = transform.position;
						});
				}
				else
					transform.DOKill();
			}
			else if (dir.type == 3)
            {
				if (gameObject.activeInHierarchy == true)
                {
					yield return new WaitForSeconds(4.0f);

					transform.DOPath(new[] { transform.position,
						new Vector3(transform.position.x - 1.0f, transform.position.y - 0.75f, 0.0f),
						new Vector3(transform.position.x - 2.0f, transform.position.y, 0.0f),
						new Vector3(transform.position.x - 3.0f, transform.position.y + 0.75f, 0.0f),
						new Vector3(transform.position.x - 4.0f, transform.position.y , 0.0f) }, 1.6f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
						{
							dir.pos = transform.position;
						});
				}
				else
					transform.DOKill();
			}
		}
	}

	public void EnemyAttack()
	{
		attackDelay += Time.deltaTime;

		if (attackDelay >= 4.0f)
		{
			if (randomBullet == 4 || randomBullet == 5)
			{
				bullet = Instantiate(BullterPrefab);
				bullet.name = "EnemyBullet";
				bullet.transform.position += new Vector3(
					BulletPoint.transform.position.x - Speed * 1.2f * Time.deltaTime,
					BulletPoint.transform.position.y,
					BulletPoint.transform.position.z);
			}

			attackDelay = 0.0f;
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
