using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertCoin : MonoBehaviour
{
    [SerializeField] private GameObject YellowPlane;
    [SerializeField] private GameObject RedPlane;

    Animator yellow;
    Animator red;

    private void Start()
    {
        yellow = YellowPlane.GetComponent<Animator>();
        red = RedPlane.GetComponent<Animator>();
    }

    private void Update()
    {
        SceneChange();
        PlayPlaneAnim();
        PlayGame();
    }
    
    void SceneChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (SoundManager.Instance.audioSourceBGM.isPlaying == true)
                SoundManager.Instance.StopBGM("Select");

            SoundManager.Instance.PlayBGM("Select");

            foreach (AudioSource element in SoundManager.Instance.audioSourceEffects)
            {
                if (element.name == "Credit" && element.isPlaying)
                    SoundManager.Instance.StopSE("Credit");
            }

            SoundManager.Instance.PlaySE("Credit");

            //GameManager.Instance.Coin += 1;
            //
            //if (GameManager.Instance.Coin <= 99)
            //    GameManager.Instance.CoinText.text = GameManager.Instance.Coin.ToString();
        }
    }

    void PlayPlaneAnim()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (yellow.isActiveAndEnabled == true)
                yellow.Play("yellowPlaneup", -1, 0f);

            if (red.isActiveAndEnabled == true)
                red.Play("redPlaneup", -1 ,0f);
        }
    }

    void PlayGame()
	{
        if (Input.GetKeyDown(KeyCode.Alpha1) && GameManager.Instance.Coin > 0)
        {
            gameObject.SetActive(false);
            SoundManager.Instance.StopBGM("Select");
            SoundManager.Instance.StopSE("Credit");
            SoundManager.Instance.PlayBGM("Seaside Front");
            GameManager.Instance.Coin -= 1;
            GameManager.Instance.PlayerCanvas.SetActive(true);
        }
	}
}
