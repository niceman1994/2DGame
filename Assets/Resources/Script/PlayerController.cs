using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : Object
{
	[SerializeField] private Animator SkillBar;
	[SerializeField] private GameObject BulletPoint;
	[SerializeField] private GameObject BulletPrefab;
	[SerializeField] private GameObject[] Smog = new GameObject[3];
	[SerializeField] private Vector3[] point = new Vector3[4];

	GameObject Bullet;
	Animator[] smoganim = new Animator[3];

	private void FixedUpdate()
	{
		DOTween.SetTweensCapacity(500, 50);
		//transform.DOMove(point[1], 3.0f).onPlay(() => transform.DOMove(point[2], 1.0f));
		//transform.DOPath(point, 2.0f, PathType.CatmullRom);
	}

	public override void Initialize()
	{
		base.Name = "Player";
		base.Hp = 0;
		base.Atk = 0;
		base.ObjectAnim = _Object.GetComponent<Animator>();
		ObjectAnim.enabled = false;

		for (int i = 0; i < Smog.Length; ++i)
		{
			smoganim[i] = Smog[i].GetComponent<Animator>();
			smoganim[i].enabled = false;
		}
	}

	public override void Progress()
	{
		Sally();
		//Move();
		Attack();
	}

	public override void Release()
	{
		
	}

	void Sally()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			ObjectAnim.enabled = true;
			smoganim[0].enabled = true;
			

			if (transform.position.x >= 15.7f)
				smoganim[1].enabled = true;

			if (transform.position.x >= 17.2f)
				smoganim[2].enabled = true;

			if (transform.position.x >= 29.0f)
			{
				for (int i = 0; i < smoganim.Length; ++i)
				{
					smoganim[i].enabled = false;
					Smog[i].SetActive(false);
				}
			}
		}
	}

    void Move()
	{
		CameraView();

		if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
		{
			ObjectAnim.SetBool("up", true);
			ObjectAnim.SetBool("down", false);
			transform.Translate(new Vector3(0.0f, 0.03f, 0.0f));
		}
		else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
		{
			ObjectAnim.SetBool("down", true);
			ObjectAnim.SetBool("up", false);
			transform.Translate(new Vector3(0.0f, -0.03f, 0.0f));
		}
		else
		{
			ObjectAnim.SetBool("idle", true);
			ObjectAnim.SetBool("up", false);
			ObjectAnim.SetBool("down", false);
		}

		if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
			transform.Translate(new Vector2(-0.03f, 0.0f));
		else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
			transform.Translate(new Vector2(0.03f, 0.0f));
	}

	void CameraView()
    {
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

		if (pos.x > 0.94f) pos.x = 0.94f;
		if (pos.x < 0.06f) pos.x = 0.06f;
		if (pos.y > 0.96f) pos.y = 0.96f;
		if (pos.y < 0.039f) pos.y = 0.039f;

		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}

	void Attack()
    {
		if (Input.GetKeyDown(KeyCode.Z))
		{
			Bullet = Instantiate(BulletPrefab, BulletPoint.transform);
			Bullet.transform.position += new Vector3(Bullet.transform.position.x + Time.deltaTime, Bullet.transform.position.y, 0.0f);
		}
    }
}
