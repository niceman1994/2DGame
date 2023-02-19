using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class greenEnemy : Object
{
	[SerializeField] private GameObject BulletPoint1;
	[SerializeField] private GameObject BulletPoint2;
	[SerializeField] private Animator[] animators;

	public override void Initialize()
	{
		base.Name = "greenEnemy";
		base.Hp = 100;
		base.Speed = 3.0f;
		base.ObjectAnim = gameObject.GetComponent<Animator>();
		ObjectAnim.enabled = false;

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
			{
				animators[i].enabled = false;
				animators[i].speed = 0;
			}
		}
		else
			ObjectAnim.enabled = false;
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

			transform.DOMoveX(transform.position.x - 22.0f, 2.0f).SetEase(Ease.Linear).OnComplete(() =>
			{
				transform.DOPath(new[] { new Vector3(transform.position.x + 8.0f, transform.position.y + 3.0f, 0.0f),
					new Vector3(transform.position.x + 8.0f, transform.position.y + 5.0f, 0.0f),
					new Vector3(transform.position.x - 1.5f, transform.position.y + 5.0f, 0.0f) }, 3.0f, PathType.CatmullRom)
					.SetEase(Ease.Linear).OnComplete(() => { }).Kill(true);
			});
			
			yield return waitForSeconds;
			ObjectAnim.enabled = true;

			if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
			{
				yield return new WaitForSeconds(1.0f);

				SoundManager.Instance.PlaySE("GreenEnemyBulletSound");
				ShootBullet();
				ObjectAnim.enabled = false;
			}

			yield return new WaitForSeconds(1.0f);
			transform.DOPath(new[] { transform.position,
				new Vector3(transform.position.x + 12.0f, transform.position.y - 4.5f, 0.0f)}, 2.0f, PathType.Linear).SetEase(Ease.Linear).OnComplete(() =>
				{
					ObjectAnim.enabled = true;
					SoundManager.Instance.PlaySE("GreenEnemyBulletSound");
					ShootBullet();
				});

			ObjectAnim.enabled = false;
		}
	}

	void ShootBullet()
    {
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
}
