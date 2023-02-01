using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasController : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        SoundManager.instance.PlayBGM("Title");
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
            SoundManager.instance.StopBGM("Title");
            anim.enabled = false;

            GameManager.Instance.CoinCanvas.SetActive(true);
            SoundManager.instance.PlayBGM("Select");
            SoundManager.instance.PlaySE("Credit");
        }
    }
}
