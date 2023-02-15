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
	Animator Charge;
	Animator dieSmog;

	public override void Initialize()
	{
		base.Name = "Player";
		base.Hp = GameManager.Instance.PlayerLife;
		base.Speed = 10.0f;
		base.ObjectAnim = _Object.GetComponent<Animator>();
		ObjectAnim.enabled = false;

		StartCoroutine(Sally());

		for (int i = 0; i < Smog.Length; ++i)
		{
			smoganim[i] = Smog[i].GetComponent<Animator>();
			smoganim[i].enabled = false;
		}

		Charge = transform.GetChild(4).GetComponent<Animator>();
		Charge.enabled = false;

		dieSmog = transform.GetChild(5).GetComponent<Animator>();

		StartCoroutine(ChargeEffect());
	}

	public override void Progress()
	{
		SmogAni();

		if (!ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Sally") && !ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
		{
			Attack();
			Move();
			ChargeEffect();
		}
	}
	
	public override void Release()
	{
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "EnemyBullet" || collision.gameObject.tag == "Enemy")
		{
			ObjectAnim.SetBool("idle", false);
			ObjectAnim.SetTrigger("die");
			dieSmog.gameObject.SetActive(true);
			dieSmog.SetTrigger("die");
			transform.GetComponent<BoxCollider2D>().enabled = false;
			GameManager.Instance.PlayerLife -= 1;

			transform.DOPath(
				new[] { transform.position, new Vector3(transform.position.x + 3.0f, - 6.2f, 0.0f) }, 2.0f, PathType.CatmullRom)
				.SetEase(Ease.Linear).OnComplete(() =>
			{
				StartCoroutine(DieCheck());
			});
		}
	}

	private IEnumerator Sally()
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

				yield break;
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
		if (Input.GetKeyDown(KeyCode.A))
		{
			SoundManager.Instance.PlaySE("Shootsound");
			GameObject Bullet = ObjectPool.Instance.PopPooledObject("Bullet");
			Bullet.transform.position = BulletPoint.transform.position;
		}
    }

	IEnumerator ChargeEffect()
	{
		while (true)
		{
			yield return null;

			if (SkillBar.GetCurrentAnimatorStateInfo(0).IsName("GaugeUp"))
			{
				if (Input.GetKey(KeyCode.A))
				{
					if (SkillBar.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.25f)
					{
						Charge.enabled = true;
						Charge.GetComponent<SpriteRenderer>().enabled = true;
					}
				}
				else
				{
					if (SkillBar.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
					{
						Charge.enabled = false;
						Charge.GetComponent<SpriteRenderer>().enabled = false;
					}
					else
					{
						Charge.SetBool("chargeEnd", true);
						SoundManager.Instance.PlaySE("SkillSound");
						yield return Charge.GetCurrentAnimatorStateInfo(0).normalizedTime;
						Charge.SetBool("chargeEnd", false);
						yield return new WaitForSeconds(0.1f);
						Charge.GetComponent<SpriteRenderer>().enabled = false;
						Charge.enabled = false;
					}
				}
			}
		}
	}

	IEnumerator DieCheck()
	{
		yield return null;

		dieSmog.gameObject.SetActive(false);
		transform.position = Vector3.MoveTowards(new Vector3(11.0f, 1.3f, 0.0f), new Vector3(16.0f, 1.3f, 0.0f), 0.01f);

		yield return new WaitForSeconds(2.0f);
		ObjectAnim.Play("Idle");
		transform.GetComponent<BoxCollider2D>().enabled = true;
	}
}
