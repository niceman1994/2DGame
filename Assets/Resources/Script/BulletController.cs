using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Object
{
	float BulletSpeed;

	public override void Initialize()
	{
		base.Name = "Bullet";
		base.Hp = 0;
		base.Atk = 10;
		base.ObjectAnim = null;
	}

	public override void Progress()
	{
		
	}

	public override void Release()
	{
		
	}
}
