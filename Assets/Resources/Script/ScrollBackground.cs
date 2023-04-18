using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private BackgroundBuilding[] scrollBackground;
	[HideInInspector] public int loopCount;

	private float[] blueLeftPosX = new float[2];
	private float[] blueRightPosX = new float[2];
	private float[] redLeftPosX = new float[2];
	private float[] redRightPosX = new float[2];

	private void Start()
	{
		for (int i = 0; i < scrollBackground.Length - 2; ++i)
		{
			Vector2 vect = scrollBackground[i].spritebg.sprite.rect.size /
				scrollBackground[i].spritebg.sprite.pixelsPerUnit;

			blueLeftPosX[i] = -(vect.x) * 10.0f;
			blueRightPosX[i] = vect.x * 10.0f;
		}

		for (int i = 2; i < scrollBackground.Length; ++i)
		{
			Vector2 vect = scrollBackground[i].spritebg.sprite.rect.size /
				scrollBackground[i].spritebg.sprite.pixelsPerUnit;

			redLeftPosX[i - 2] = -(vect.x) * 10.0f;
			redRightPosX[i - 2] = vect.x * 10.0f;
		}

		loopCount = 0;
	}

	void Update()
    {
		Scrolling();
		RuinScrolling();
	}
	
    void Scrolling()
	{
		if (Camera.main.transform.position.x == 22.0f)
		{
			for (int i = 0; i < scrollBackground.Length - 2; ++i)
			{
				scrollBackground[i].gameObject.transform.position = new Vector3(
					scrollBackground[i].gameObject.transform.position.x + (-BackgroundManager.Instance.Speed * Time.deltaTime), 0.0f, 0.0f);
		
				if (scrollBackground[i].gameObject.transform.position.x < blueLeftPosX[i] * 2.91f) // 움직이는 배경이 카메라에서 완전히 벗어났을 때의 x 좌표 : -115.84
				{
					Vector3 nextPos = scrollBackground[i].gameObject.transform.position;
					nextPos = new Vector3(nextPos.x + blueRightPosX[i] + 38.3993f, nextPos.y, nextPos.z);
					scrollBackground[i].gameObject.transform.position = nextPos;
					loopCount += 1;

					if (GameManager.Instance.GameCount <= 35.0f && scrollBackground[i].transform.position == nextPos)
					{
						for (int j = 16; j < scrollBackground[i].transform.childCount; ++j)
							scrollBackground[i].transform.GetChild(j).gameObject.SetActive(true);
					}
				}

				if (GameManager.Instance.GameCount <= 10.0f && GameManager.Instance.GameCount > 7.0f)
				{
					for (int j = 3; j < 16; ++j)
						scrollBackground[i].transform.GetChild(j).gameObject.SetActive(true);
				}
				else if (GameManager.Instance.GameCount <= 7.0f)
				{
					scrollBackground[i].gameObject.SetActive(false);
					scrollBackground[i].transform.position = Vector3.zero;
				}
			}
		}
	}

	// TODO : 추후 수정
	void RuinScrolling()
	{
		if (scrollBackground[0].gameObject.activeInHierarchy == false &&
			scrollBackground[1].gameObject.activeInHierarchy == false)
		{
			for (int i = 2; i < scrollBackground.Length; ++i)
			{
				scrollBackground[i].gameObject.SetActive(true);

				scrollBackground[i].gameObject.transform.position = new Vector3(
					scrollBackground[i].transform.position.x + (-BackgroundManager.Instance.Speed * Time.deltaTime), 0.0f, 0.0f);

				if (scrollBackground[i].gameObject.transform.position.x < redLeftPosX[i - 2] * 3.75f)
				{
					Vector3 nextPos = scrollBackground[i].gameObject.transform.position;
					nextPos = new Vector3(nextPos.x + redRightPosX[i - 2] + 28.32f, nextPos.y, nextPos.z);
					scrollBackground[i].gameObject.transform.position = nextPos;
				}
			}
		}
	}
}
