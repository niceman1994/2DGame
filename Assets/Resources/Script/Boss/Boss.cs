using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : Object
{
    [SerializeField] private Animator[] WeaponAnim = new Animator[5];

    public override void Initialize()
    {
        base.Name = "Boss";
        base.Hp = 200;
        base.Speed = 0.0f;
        base.ObjectAnim = null;

        StartCoroutine(Move());
    }

    public override void Progress()
    {
        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
            if (GameManager.Instance.countDown <= 5.0f)
            {
                SoundManager.Instance.StopBGM("Seaside Front");
                SoundManager.Instance.PlayBGM("Ruins");
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(27.0f, transform.position.y), 0.04f);

                if (WeaponAnim[0].enabled == true &&
                    WeaponAnim[1].enabled == true)
                {
                    SoundManager.Instance.StopBGM("Ruins");
                    SoundManager.Instance.PlayBGM("Boss");
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(30.0f, 0.05f), 0.04f);
                }
            }
        }
    }

    public override void Release()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        foreach (Animator ani in WeaponAnim)
        {
            if (collision.gameObject.CompareTag("Bullet") && ani.enabled == true)
            {
                Hp -= 10;
                SoundManager.Instance.PlaySE("hitSound");

                if (Hp <= 0)
                {
                    Hp = 0;
                    transform.DOPath(new[] { transform.position,
                    new Vector3(transform.position.x + 2.0f, transform.position.y - 6.0f, 0.0f)}, 3.0f, PathType.Linear).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                    });
                }
            }
        }
	}

	IEnumerator Move()
	{
        while (true)
        {
            yield return null;

            if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
                GameManager.Instance.CoinCanvas.activeInHierarchy == false)
            {
                if (transform.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize)
                {
                    transform.DOPath(new[] { new Vector3(transform.position.x, 6.33f, 0.0f),
                    new Vector3(transform.position.x, 6.33f - 0.5f, 0.0f),
                    new Vector3(transform.position.x, 6.33f , 0.0f) }, 2.0f, PathType.Linear).SetLoops(-1);
                }
                else
                    transform.DOKill();
            }
            else
                break;
        }
    }
}
