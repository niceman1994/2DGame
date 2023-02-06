using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarController : MonoBehaviour
{
    Animator anim;
    float animNormalizedTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        anim.speed = 0;
        animNormalizedTime = 0.0f;
    }

	private void Update()
	{
        StartAnimation();
	}

	void StartAnimation()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GaugeUp"))
        {
            if (Input.GetKey(KeyCode.Z))
            {
                anim.speed = 1;
                animNormalizedTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            }
            else
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                {
                    animNormalizedTime -= Time.deltaTime * 1.5f;
                    anim.Play("GaugeUp", 0, animNormalizedTime >= 0.0f ? animNormalizedTime : 0.0f);
                }
                else
                {
                    anim.SetTrigger("chargeEnd");
                    animNormalizedTime = 0.0f;
                }
            }
        }
    }
}
