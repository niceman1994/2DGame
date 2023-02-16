using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private BackgroundBuilding[] scrollBackground;

	private float[] leftPosX = new float[2];
	private float[] rightPosX = new float[2];

	private void Start()
	{
		for (int i = 0; i < scrollBackground.Length - 2; ++i)
		{
			Vector2 vect = scrollBackground[i].spritebg.sprite.rect.size /
				scrollBackground[i].spritebg.sprite.pixelsPerUnit;
		
			leftPosX[i] = -(vect.x) * 10.0f;
			rightPosX[i] = vect.x * 10.0f;
		}
	}

	void Update()
    {
		Scrolling();
	}
	
    void Scrolling()
	{
		if (Camera.main.transform.position.x >= 22.0f)
		{
			for (int i = 0; i < scrollBackground.Length - 2; ++i)
			{
				scrollBackground[i].gameObject.transform.position = new Vector3(
					scrollBackground[i].gameObject.transform.position.x + (-BackgroundManager.Instance.Speed * Time.deltaTime), 0.0f, 0.0f);
		
				if (scrollBackground[i].gameObject.transform.position.x < leftPosX[i] * 2.91f) // 움직이는 배경이 카메라에서 완전히 벗어났을 때의 x 좌표 : -115.84
				{
					Vector3 nextPos = scrollBackground[i].gameObject.transform.position;
					nextPos = new Vector3(nextPos.x + rightPosX[i] + 38.3993f, nextPos.y, nextPos.z);
					scrollBackground[i].gameObject.transform.position = nextPos;
				}
			}
		}
	}
}
