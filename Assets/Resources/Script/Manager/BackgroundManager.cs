using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : ManagerSingleton<BackgroundManager>
{
	public Transform[] Background;
	public float Speed;
	public float xScreenHalfSize;
	public float yScreenHalfSize;

	float leftPosX = 0f;
	float rightPosX = 0f;

	private void Start()
	{
		Speed = 6.5f;

		yScreenHalfSize = Camera.main.orthographicSize;
		xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

		leftPosX = -(xScreenHalfSize) * 2;
		rightPosX = xScreenHalfSize * 2.5f * Background.Length;
	}

	private void Update()
	{
		BackgroundMove();
		StopBackground();
	}

	void BackgroundMove()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false && Time.timeScale == 1)
		{
			Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(22.0f, 0.0f, -1.0f), 0.012f); // �������� 0.012f, �п������� 0.033f

			if (Camera.main.transform.position.x >= 22.0f)
			{
				if (Background[0].position.x >= -73.2f)
					Background[0].position = new Vector3(Background[0].position.x + (-Speed * Time.deltaTime), 0.0f, 0.0f);

				for (int i = 1; i < Background.Length; ++i)
				{
					Background[i].position = new Vector3(Background[i].position.x + (-Speed * Time.deltaTime), 0.0f, 0.0f);

					if (Background[i].position.x <= leftPosX && 
						GameManager.Instance.GameCount <= 50.0f && GameManager.Instance.GameCount > 7.0f)
					{
						Vector3 nextPos = Background[i].position;
						nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
						Background[i].position = nextPos;
					}
					else if (Background[i].position.x <= leftPosX && GameManager.Instance.GameCount <= 7.0f
						&& GameManager.Instance.GameCount > 6.5f)
					{
						Background[i].gameObject.SetActive(false);
						Background[i].position = new Vector3(-70.0f, 0.0f, 0.0f);
					}
				}
			}
		}

		if (GameManager.Instance.PlayerLife == 0) Speed = 0.0f;
	}

	void StopBackground()
	{
		if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 1)
			Time.timeScale = 0;
		else if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 0)
			Time.timeScale = 1;
	}
}
