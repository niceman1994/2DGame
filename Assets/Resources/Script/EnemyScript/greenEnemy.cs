using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class greenEnemy : Object
{
	[SerializeField] private GameObject BulletPoint1;
	[SerializeField] private GameObject BulletPoint2;
	[SerializeField] private Animator[] animators;
	[SerializeField] private Vector3[] pos = new Vector3[5];

	float time;

	public override void Initialize()
	{
		base.Name = "greenEnemy";
		base.Hp = 100;
		base.Speed = 3.0f;
		base.ObjectAnim = GetComponent<Animator>();
		ObjectAnim.enabled = false;
		time = 0.0f;

		StartCoroutine(UpDown());
	}

	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			ZeroHp();
			ShootBullet();
		}
	}

	public override void Release()
	{

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			Hp -= 10;
			SoundManager.Instance.PlaySE("hitSound");

			if (Hp <= 0)
			{
				Hp = 0;
				transform.GetComponent<BoxCollider2D>().enabled = false;
				transform.DOPath(new[] { transform.position,
					new Vector3(transform.position.x + 4.0f, -6.5f, 0.0f)}, 2.0f, PathType.Linear).SetEase(Ease.Linear).OnComplete(() =>
					{
						gameObject.SetActive(false);
					});

				GameManager.Instance.Score += Random.Range(25, 30) * 10;

				for (int i = 0; i < transform.childCount - 2; ++i)
				{
					animators[i].enabled = true;
					animators[i].speed = 1;
				}
			}
		}
		else if (collision.gameObject.tag == "Player")
			GameManager.Instance.PlayerLife -= 1;
	}

	void ZeroHp()
	{
		if (Hp > 0)
		{
			for (int i = 0; i < transform.childCount - 2; ++i)
				animators[i].enabled = false;
		}
	}

	// TODO : 추후 수정
	public IEnumerator UpDown()
	{
		WaitForSeconds waitForSeconds = new WaitForSeconds(6.0f);

		yield return waitForSeconds;

		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (gameObject.activeInHierarchy == true && Hp > 0)
			{
				transform.DOMove(pos[0], 2.0f).SetEase(Ease.Linear).OnComplete(() =>
				{
					transform.DOPath(new[] { pos[0], pos[1], pos[2], pos[3] }, 2.0f, PathType.CatmullRom).SetEase(Ease.Linear).SetDelay(1.0f, false).OnComplete(() =>
				   {
					   transform.DOPath(new[] { pos[3], pos[4] }, 2.0f).SetEase(Ease.Linear).SetDelay(1.0f, false).SetAutoKill(true);
				   });
				});
			}
			else if (gameObject.activeInHierarchy == false)
				transform.DOKill(true);
		}
	}

	void ShootBullet()
    {
		time += Time.deltaTime;

		if (transform.position.x == 21.0f || transform.position.x == 31.0f)
		{
			if (time >= 1.0f)
			{
				time = 0.0f;
				ObjectAnim.enabled = true;

				for (int i = 0; i < 6; ++i)
				{
					GameObject bullet1 = Instantiate(EnemyManager.Instance.BullterPrefab);
					bullet1.name = "EnemyBullet";
					bullet1.transform.position = new Vector3(
						BulletPoint1.transform.position.x - i, BulletPoint1.transform.position.y, BulletPoint1.transform.position.z);

					GameObject bullet2 = Instantiate(EnemyManager.Instance.BullterPrefab);
					bullet2.name = "EnemyBullet";
					bullet2.transform.position = new Vector3(
						BulletPoint2.transform.position.x - i, BulletPoint2.transform.position.y, BulletPoint2.transform.position.z);
				}
			}
			else
				ObjectAnim.enabled = false;
		}
	}
}
