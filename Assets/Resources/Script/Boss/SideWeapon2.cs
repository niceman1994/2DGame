using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWeapon2 : Object
{
	public override void Initialize()
	{
		base.Name = "SideWeapon2";
		base.Hp = 80;
		base.Speed = 0.0f;
		base.ObjectAnim = GetComponent<Animator>();
	}

	public override void Progress()
	{
		
	}

	public override void Release()
	{
		
	}
}
