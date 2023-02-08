using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : ManagerSingleton<BackgroundManager>
{
	[SerializeField] private Transform[] Background;
	float Speed;

	float leftPosX = 0f;
	float rightPosX = 0f;
	float xScreenHalfSize;
	float yScreenHalfSize;

	private void Start()
	{
		Speed = 6.5f;

		yScreenHalfSize = Camera.main.orthographicSize;
		xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

		leftPosX = -(xScreenHalfSize) * 2;
		rightPosX = xScreenHalfSize * Background.Length;
	}

	private void LateUpdate()
	{
		BackgroundMove();
	}

	void BackgroundMove()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
			Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(22.0f, 0.0f, -1.0f), 0.0125f);

			if (Camera.main.transform.position.x >= 22.0f)
			{
				Background[0].position = new Vector3(Background[0].position.x + (-Speed * Time.deltaTime), 0.0f, 0.0f);

				for (int i = 1; i < Background.Length - 1; ++i)
				{
					Background[i].position = new Vector3(Background[i].position.x + (-Speed * Time.deltaTime), 0.0f, 0.0f);

					if (Background[i].position.x < leftPosX)
					{
						Vector3 nextPos = Background[i].position;
						nextPos = new Vector3(nextPos.x + rightPosX + (rightPosX * 0.5f), nextPos.y, nextPos.z);
						Background[i].position = nextPos;
					}
				}
			}

			Background[5].position = new Vector3(Background[5].position.x + (-Speed * 0.98f * Time.deltaTime), 0.0f, 0.0f);
		
			if (Background[5].position.x < leftPosX)
            {
				Vector3 nextPos = Background[5].position;
				nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
				Background[5].position = nextPos;
            }
		}
		else if (GameManager.Instance.PlayerLife == 0) Speed = 0.0f;
	}
}
