using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Object : MonoBehaviour, Interface
{
	protected string Name;
	protected int Hp;
	protected int Atk;
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

	protected void OnDestroy()
    {
		Release();
    }

    // 순수 가상 함수 Initialize(), Progress(), Release()
    public abstract void Initialize();
	public abstract void Progress();
	public abstract void Release();
}
