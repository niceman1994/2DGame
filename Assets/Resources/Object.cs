using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Object : MonoBehaviour, Interface
{
	protected string Name;
	protected int Hp;
	protected float Speed;
	protected Animator ObjectAnim;
	public GameObject _Object;

	protected void Start()
	{
		Initialize();
	}

	protected void Update()
	{
		Progress();
	}

	// ���� ���� �Լ� Initialize(), Progress(), Release()
	public abstract void Initialize();
	public abstract void Progress();
	//public abstract void Release();
}
