using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : Object
{
	[SerializeField] private Animator SkillBar;
	[SerializeField] private GameObject BulletPoint;
	[SerializeField] private GameObject[] Smog = new GameObject[3];
	[SerializeField] private Vector3[] point = new Vector3[3];

	Animator[] smoganim = new Animator[3];

	public override void Initialize()
	{
		base.Name = "Player";
		base.Hp = GameManager.Instance.PlayerLife;
		base.Atk = 10;
		base.ObjectAnim = _Object.GetComponent<Animator>();
		ObjectAnim.enabled = false;

		StartCoroutine(Sally());

		for (int i = 0; i < Smog.Length; ++i)
		{
			smoganim[i] = Smog[i].GetComponent<Animator>();
			smoganim[i].enabled = false;
		}
	}

	public override void Progress()
	{
		SmogAni();
		Attack();
		Move();
	}
	
	public override void Release()
	{
		
	}

	IEnumerator Sally()
    {
		while (true)
        {
			yield return null;
			
			if (GameManager.Instance.PlayerCanvas.activeInHierarchy == true)
			{
				yield return new WaitForSeconds(1.65f);
				transform.GetComponent<AudioSource>().Play();
				transform.DOMove(point[0], 2.2f).SetEase(Ease.InSine).OnComplete(() =>
			  {
				  transform.DOPath(point, 2.0f, PathType.CatmullRom).SetEase(Ease.OutSine);
			  });

				break;
			}
		}
    }

	void SmogAni()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			ObjectAnim.enabled = true;

			if (transform.position.x >= 15.0f)
				smoganim[0].enabled = true;
			
			if (transform.position.x >= 18.7f)
				smoganim[1].enabled = true;
			
			if (transform.position.x >= 22.0f)
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
		if (!ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Sally") && transform != null)
		{
			CameraView();

			if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
			{
				ObjectAnim.SetBool("up", true);
				ObjectAnim.SetBool("down", false);
				transform.Translate(new Vector3(0.0f, 0.02f, 0.0f));
			}
			else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
			{
				ObjectAnim.SetBool("down", true);
				ObjectAnim.SetBool("up", false);
				transform.Translate(new Vector3(0.0f, -0.02f, 0.0f));
			}
			else
			{
				ObjectAnim.SetBool("idle", true);
				ObjectAnim.SetBool("up", false);
				ObjectAnim.SetBool("down", false);
			}

			if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
				transform.Translate(new Vector2(-0.02f, 0.0f));
			else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
				transform.Translate(new Vector2(0.02f, 0.0f));
		}
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
		if (Input.GetKeyDown(KeyCode.A) &&
			!ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Sally")
			&& transform != null)
		{
			SoundManager.Instance.PlaySE("Shootsound");
			GameObject Bullet = ObjectPool.GetObject();
			Bullet.transform.position = BulletPoint.transform.position;
		}
    }
}
