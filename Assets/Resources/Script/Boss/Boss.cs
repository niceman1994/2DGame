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
        base.Hp = 0;
        base.Speed = 0.0f;
        base.ObjectAnim = null;
    }

    public override void Progress()
    {
        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
            if (GameManager.Instance.countDown <= 5.0f)
            {
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
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Hp += 1;
                SoundManager.Instance.PlaySE("hitSound");

                if (Hp >= 70)
                {
                    Hp = 70;
                    transform.GetComponent<PolygonCollider2D>().enabled = false;
                    transform.DOPath(new[] { transform.position,
                    new Vector3(transform.position.x + 2.0f, transform.position.y - 8.0f, 0.0f)}, 3.5f, PathType.Linear).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                    });
                }
            }
        }
	}
}
