using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCanvasController : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM("Title");
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
            SoundManager.Instance.StopBGM("Title");
            anim.enabled = false;

            GameManager.Instance.CoinCanvas.SetActive(true);
            SoundManager.Instance.PlayBGM("Select");
            SoundManager.Instance.PlaySE("Credit");
        }
    }
}
