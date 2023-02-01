using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertCoin : MonoBehaviour
{
    Animator yellow;
    Animator red;

    private void Start()
    {
        gameObject.SetActive(false);
        yellow = GameManager.Instance.YellowPlane.GetComponent<Animator>();
        red = GameManager.Instance.RedPlane.GetComponent<Animator>();
    }

    private void Update()
    {
        Insertcoin();
        PlayPlaneAnim();
    }
    
    void Insertcoin()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (SoundManager.instance.audioSourceBGM.isPlaying == true)
                SoundManager.instance.StopBGM("Select");

            SoundManager.instance.PlayBGM("Select");

            foreach (AudioSource element in SoundManager.instance.audioSourceEffects)
            {
                if (element.name == "Credit" && element.isPlaying)
                    SoundManager.instance.StopSE("Credit");
            }

            SoundManager.instance.PlaySE("Credit");

            if (GameManager.Instance.Coin <= 99)
            {
                GameManager.Instance.Coin += 1;
                GameManager.Instance.CoinText.text = GameManager.Instance.Coin.ToString();
            }
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
}
