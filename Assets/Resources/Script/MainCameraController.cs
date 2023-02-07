using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
	private void LateUpdate()
	{
		CameraMove();
	}

	void CameraMove()
	{
		if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
			GameManager.Instance.CoinCanvas.activeInHierarchy == false)
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(22.0f, 0.0f, -1.0f), 0.015f);
	}
}
