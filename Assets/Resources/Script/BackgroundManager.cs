using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : ManagerSingleton<BackgroundManager>
{
	[SerializeField] private Transform[] Background;
	float Speed;

	private void Start()
	{
		Speed = 5.5f;
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
			Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(22.0f, 0.0f, -1.0f), 0.027f);

			if (Camera.main.transform.position.x >= 22.0f)
				Background[0].position = new Vector3(Background[0].position.x + (-Speed * Time.deltaTime), 0.0f, 0.0f);

			Background[3].position = new Vector3(Background[3].position.x + (-Speed * 0.98f * Time.deltaTime), 0.0f, 0.0f);
		}
		else if (GameManager.Instance.PlayerLife == 0) Speed = 0.0f;
	}
}
