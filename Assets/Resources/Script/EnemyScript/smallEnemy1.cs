using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	float currentTime;
	float turnpointTime;

	DirType dir;

	public override void Initialize()
	{
		base.name = "smallEnemy1";
		base.Hp = 0;
		base.Speed = 2.0f;
		base.ObjectAnim = gameObject.GetComponent<Animator>();

		randomBullet = 0.0f;
		turnpointTime = 1.0f;
		GetDirType(1.7f, -1.7f);
	}

	public override void Progress()
	{
		randomBullet = Random.Range(0, 10);

		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			if (transform.position.x >= Camera.main.transform.position.x - BackgroundManager.Instance.xScreenHalfSize)
			{
				transform.position = new Vector3(transform.position.x - (Speed * Time.deltaTime), transform.position.y, 0.0f);
				UpDown();
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
            EnemyManager.Instance.Score += Random.Range(5, 6) * 10;

            StartCoroutine(ReturnObject());
        }
    }

	void GetDirType(float _y1, float _y2)
    {
		if (transform.position.y >= _y1)
			dir.type = 1;
		if (transform.position.y <= -_y2)
			dir.type = 2;
    }

    public void UpDown()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			currentTime += Time.deltaTime;
	
			if (dir.type == 1)
			{
				transform.position += new Vector3(0.0f, -Speed * 0.25f * Time.deltaTime, 0.0f);
	
				if (currentTime > turnpointTime)
				{
					dir.type = 2;
					currentTime = 0.0f;
				}
			}
			else if (dir.type == 2)
			{
				transform.position += new Vector3(0.0f, Speed * 0.25f * Time.deltaTime, 0.0f);
	
				if (currentTime > turnpointTime)
				{
					dir.type = 1;
					currentTime = 0.0f;
				}
			}
		}
	}

	//public IEnumerator UpDown()
	//{
	//	yield return null;
	//
	//	if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
	//		GameManager.Instance.CoinCanvas.activeInHierarchy == false)
	//	{
	//		if (dir.type == 1)
	//		{
	//			dir.pos = new Vector3(0.0f, -Speed * 0.25f * Time.deltaTime, 0.0f);
	//			transform.position += dir.pos;
	//
	//			yield return new WaitForSeconds(1.0f);
	//			dir.type = 2;
	//		}
	//		else if (dir.type == 2)
	//		{
	//			dir.pos = new Vector3(0.0f, Speed * 0.25f * Time.deltaTime, 0.0f);
	//			transform.position += dir.pos;
	//
	//			yield return new WaitForSeconds(1.0f);
	//			dir.type = 1;
	//		}
	//	}
	//}

	public void EnemyAttack()
	{
		attackDelay += Time.deltaTime;

		if (attackDelay >= 6.0f && randomBullet == 5)
		{
			bullet = Instantiate(BullterPrefab);
			bullet.name = "EnemyBullet";
			bullet.transform.position += new Vector3(
				BulletPoint.transform.position.x - Speed * 1.3f * Time.deltaTime,
				BulletPoint.transform.position.y,
				BulletPoint.transform.position.z);

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
