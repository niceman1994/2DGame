using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallEnemy3 : Object
{
	public override void Initialize()
	{
		base.Name = "smallEnemy3";
		base.Hp = 0;
		base.Speed = 2.0f;
		base.ObjectAnim = gameObject.GetComponent<Animator>();
	}

	public override void Progress()
	{
		
	}

	public override void Release()
	{
		
	}
}
