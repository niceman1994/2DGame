using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBuilding : MonoBehaviour
{
	public SpriteRenderer spritebg;
	public int loopCount;

	// TODO : 추후수정
	private void Update()
	{
		if (transform.position.x <= -111.0f)
			loopCount += 1;

		if (loopCount >= 10)
		{
			for (int i = 3; i < transform.childCount; ++i)
			{
				transform.GetChild(i).gameObject.SetActive(true);
				loopCount = 0;
			}
		}
	}
}
