using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarController : MonoBehaviour
{
    [SerializeField] private Sprite[] Upsprite;
    [SerializeField] private Sprite[] Downsprite;

    Animator anim;
    float chargeTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        anim.speed = 0;
        chargeTime = 0.0f;
        StartCoroutine(StartAnimation());
    }

    // TODO : 키 입력 시간에 따른 애니메이션 진행
    IEnumerator StartAnimation()
    {
        while (true)
        {
            yield return null;

            if (Input.GetKey(KeyCode.Z))
            {
                chargeTime = Time.time * 0.25f;
                Debug.Log("chargeTime : " + chargeTime);

                anim.Play("GaugeUp", 0, Time.time * 0.25f <= 1 ? chargeTime : 1.0f);
            }
        }
    }
}
