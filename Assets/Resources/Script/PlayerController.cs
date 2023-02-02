using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Object
{
	[SerializeField] private GameObject SkillBar;

	private void Start()
	{
		Initialize();
	}

	private void Update()
	{
		Progress();
	}

	public override void Initialize()
	{
		base.Name = "Player";
		base.Hp = 10;
		base.ObjectAnim = _Object.GetComponent<Animator>();
	}

	public override void Progress()
	{
		Move();
	}

	public override void Release()
	{
		
	}

	void Move()
	{
		if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
		{
			ObjectAnim.SetBool("up", true);
			ObjectAnim.SetBool("down", false);
		}
		else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
		{
			ObjectAnim.SetBool("down", true);
			ObjectAnim.SetBool("up", false);
		}
		else
		{
			ObjectAnim.SetBool("idle", true);
			ObjectAnim.SetBool("up", false);
			ObjectAnim.SetBool("down", false);
		}
	}
}
