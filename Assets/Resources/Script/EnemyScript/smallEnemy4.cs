using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class smallEnemy4 : Object
{
	bool inScene;

    public override void Initialize()
    {
        base.Name = "smallEnemy4";
        base.Hp = 0;
        base.Speed = 2.5f;
        base.ObjectAnim = GetComponent<Animator>();

        ObjectAnim.speed = 0;
        inScene = false;
    }

    public override void Progress()
    {
        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
        {
            if (transform.position.y >= Camera.main.transform.position.y - BackgroundManager.Instance.yScreenHalfSize)
                inScene = true;
            else
            {
                if (inScene == true)
                    gameObject.SetActive(false);
            }
        }
    }

    public override void Release()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            ObjectAnim.SetTrigger("destroy");
            transform.GetComponent<BoxCollider2D>().enabled = false;
            SoundManager.Instance.PlaySE("smallEnemyDestroySound");
            GameManager.Instance.Score += Random.Range(5, 6) * 10;

            StartCoroutine(ReturnObject());
        }
    }

    public IEnumerator UpDown()
    {
        yield return null;

        WaitForSeconds waitForSeconds = new WaitForSeconds(5.0f);

        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
        {
            yield return waitForSeconds;

            if (gameObject.activeInHierarchy == true)
            {
                ObjectAnim.speed = 1;

                transform.DOPath(new[] { transform.position,
                    new Vector3(transform.position.x - 7.0f - Random.Range(2.0f, 4.0f),
                    transform.position.y + 2.0f + Random.Range(3.0f, 5.0f), 0.0f),
                    new Vector3(transform.position.x - 12.0f - Random.Range(4.0f, 14.0f), 
                    transform.position.y - 3.0f - Random.Range(3.0f, 5.0f), 0.0f)}, 3.0f, PathType.CatmullRom).SetEase(Ease.Linear);
            }
            else
                transform.DOKill();
        }
    }

    IEnumerator ReturnObject()
    {
        while (true)
        {
            yield return null;

            if (ObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                gameObject.SetActive(false);
            else if (gameObject.activeInHierarchy == false) break;
        }
    }
}
