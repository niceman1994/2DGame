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
        PlayCreditAnim();
        PlayGame();
    }
    
    void SceneChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (SoundManager.Instance.audioSourceBGM[0].isPlaying == true)
                SoundManager.Instance.audioSourceBGM[0].Stop();

            SoundManager.Instance.audioSourceBGM[1].Play();

            foreach (AudioSource element in SoundManager.Instance.audioSourceEffects)
            {
                if (element.name == "Credit" && element.isPlaying)
                    SoundManager.Instance.StopSE("Credit");
            }

            SoundManager.Instance.PlaySE("Credit");
        }
    }

    void PlayCreditAnim()
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
            SoundManager.Instance.audioSourceBGM[1].Stop();
            SoundManager.Instance.StopSE("Credit");
            SoundManager.Instance.audioSourceBGM[2].Play();
            GameManager.Instance.Coin -= 1;
            GameManager.Instance.PlayerCanvas.SetActive(true);
        }
	}
}
