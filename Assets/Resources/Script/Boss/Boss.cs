using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Boss : Object
{
    [SerializeField] private Animator[] WeaponAnim = new Animator[5];
    private bool StageClear;

    public override void Initialize()
    {
        base.Name = "Boss";
        base.Hp = 0;
        base.Speed = 0.0f;
        base.ObjectAnim = null;

        StageClear = false;
    }

    public override void Progress()
    {
        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
            if (GameManager.Instance.GameCount <= 6.5f)
            {
                if (WeaponAnim[0].enabled == false &&
                    WeaponAnim[1].enabled == false)
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(27.0f, transform.position.y), 0.02f);
                else if (WeaponAnim[0].enabled == true &&
                    WeaponAnim[1].enabled == true)
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(31.0f, 0.05f), 0.02f);
            }
        }

        if (transform.position.x == 27.0f)
        {
            SoundManager.Instance.audioSourceBGM[2].Stop();

            if (!SoundManager.Instance.audioSourceBGM[3].isPlaying)
                SoundManager.Instance.audioSourceBGM[3].Play();
        }
    }

    public override void Release()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (WeaponAnim[0].enabled == true &&
                WeaponAnim[1].enabled == true &&
                WeaponAnim[2].enabled == true)
            {
                Hp += 1;
                SoundManager.Instance.PlaySE("hitSound");

                if (Hp >= 30)
                {
                    Hp = 30;
                    SoundManager.Instance.audioSourceBGM[3].Stop();
                    SoundManager.Instance.audioSourceBGM[4].Play();
                    transform.GetComponent<PolygonCollider2D>().enabled = false;
                    transform.DOPath(new[] { transform.position,
                    new Vector3(transform.position.x + 2.0f, transform.position.y - 8.0f, 0.0f)}, 2.5f, PathType.Linear).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                        StageClear = true;
                    });
                }
            }
        }
    }

    public bool GetBool()
    {
        bool end = StageClear;
        return end;
    }
}
