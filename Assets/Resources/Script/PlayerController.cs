using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Object
{
	[SerializeField] private Animator SkillBar;
	[SerializeField] private GameObject Smog;
	[SerializeField] private GameObject BulletPoint;
	[SerializeField] private GameObject BulletPrefab;

	GameObject Bullet;
	Animator smoganim;

	private void Start()
	{
		Initialize();
	}

	void Update()
	{
		Progress();
	}

	public override void Initialize()
	{
		base.Name = "Player";
		base.Hp = 0;
		base.Atk = 0;
		base.ObjectAnim = _Object.GetComponent<Animator>();
		ObjectAnim.enabled = false;

		smoganim = Smog.GetComponent<Animator>();
		smoganim.enabled = false;
	}

	public override void Progress()
	{
		Sally();
		Move();
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
			smoganim.enabled = true;
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
		if (pos.y > 0.961f) pos.y = 0.961f;
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
