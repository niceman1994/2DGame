using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BonusMob : Object
{
	[SerializeField] private GameObject[] itemPrefab;
	GameObject[] item;

	public override void Initialize()
	{
		base.Name = "BonusMob";
		base.Hp = 40;
		base.Speed = 1.0f;
		base.ObjectAnim = GetComponent<Animator>();

		for (int i = 0; i < itemPrefab.Length; ++i)
		{
			item[i] = Instantiate(itemPrefab[i]);
			item[i].transform.SetParent(transform);
		}
	}

	public override void Progress()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (transform.position.x >= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
			{
				transform.position = new Vector3(transform.position.x - (Speed * Time.deltaTime), transform.position.y, 0.0f);

				if (Hp <= 0)
				{
					Hp = 0;
					Destroy(gameObject);
				}
			}
		}
	}

	public override void Release()
	{
		GiveItem();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
			Hp -= 10;
	}

	void GiveItem()
	{
		for (int i = 0; i < item.Length; ++i)
		{
			item[i].transform.DOPath(
				new[] { transform.position, new Vector3(transform.position.x - 8.0f, transform.position.y + 4.0f, 0.0f),
				new Vector3(transform.position.x - 14.0f - (i * 2), transform.position.y - 14.0f - i, 0.0f) }, 6.0f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
			{
				item[i].transform.DOKill();
				item[i].gameObject.SetActive(false);
			});
		}

		//item[0].transform.DOPath(
		//	new[] { transform.position, new Vector3(transform.position.x - 5.0f, transform.position.y + 4.0f, 0.0f),
		//	new Vector3(transform.position.x - 10.0f, transform.position.y - 14.0f, 0.0f) }, 2.0f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
		//	{
		//		transform.DOKill();
		//	});
		//
		//item[1].transform.DOPath(
		//	new[] { transform.position, new Vector3(transform.position.x - 5.0f, transform.position.y + 3.0f, 0.0f),
		//	new Vector3(transform.position.x - 9.0f, transform.position.y - 13.0f, 0.0f) }, 2.0f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
		//	{
		//		transform.DOKill();
		//	}); ;
		//
		//item[2].transform.DOPath(
		//	new[] { transform.position, new Vector3(transform.position.x - 5.0f, transform.position.y + 2.0f, 0.0f),
		//	new Vector3(transform.position.x - 8.0f, transform.position.y - 12.0f, 0.0f) }, 2.0f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
		//	{
		//		transform.DOKill();
		//	}); ;
		//
		//item[3].transform.DOPath(
		//	new[] { transform.position, new Vector3(transform.position.x - 5.0f, transform.position.y + 1.0f, 0.0f),
		//	new Vector3(transform.position.x - 7.0f, transform.position.y - 11.0f, 0.0f) }, 2.0f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
		//	{
		//		transform.DOKill();
		//	}); ;
	}
}
