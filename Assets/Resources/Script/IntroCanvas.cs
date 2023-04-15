using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCanvas : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        SoundManager.Instance.audioSourceBGM[0].Play();
    }

    private void Update()
    {
        Pressnum0();
    }

    void Pressnum0()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            gameObject.SetActive(false);
            SoundManager.Instance.audioSourceBGM[0].Stop();
            anim.enabled = false;

            GameManager.Instance.CoinCanvas.SetActive(true);
            SoundManager.Instance.audioSourceBGM[1].Play();
            SoundManager.Instance.PlaySE("Credit");
        }
    }
}
