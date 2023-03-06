using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class smallEnemy4 : Object
{
    [SerializeField] private GameObject BulletPoint;

    float attackDelay;
    bool inScene;

    public override void Initialize()
    {
        base.Name = "smallEnemy4";
        base.Hp = 0;
        base.Speed = 2.5f;
        base.ObjectAnim = GetComponent<Animator>();

        ObjectAnim.speed = 0;
        attackDelay = 0.0f;
        inScene = false;
    }

    public override void Progress()
    {
        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
        {
            if (transform.position.y >= Camera.main.transform.position.y - BackgroundManager.Instance.yScreenHalfSize)
            {
                inScene = true;
                EnemyAttack(1.0f);

                //if (transform.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize)
                //    EnemyAttack(1.0f);
            }
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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ObjectAnim.SetTrigger("destroy");
            transform.GetComponent<BoxCollider2D>().enabled = false;
            SoundManager.Instance.PlaySE("smallEnemyDestroySound");
            GameManager.Instance.Score += Random.Range(3, 4) * 10;

            StartCoroutine(ReturnObject());
        }
    }

    public IEnumerator UpDown()
    {
        yield return null;

        WaitForSeconds waitForSeconds = new WaitForSeconds(35.0f);

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

    void EnemyAttack(float _time)
    {
        if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("smallEnemy4"))
        {
            if (attackDelay <= _time)
                attackDelay += Time.deltaTime;
            else
            {
                GameObject bullet = Instantiate(EnemyManager.Instance.BullterPrefab);
                bullet.name = "EnemyBullet";
                bullet.transform.position += new Vector3(
                    BulletPoint.transform.position.x - Speed * 2.0f * Time.deltaTime,
                    BulletPoint.transform.position.y,
                    BulletPoint.transform.position.z);
            }

            attackDelay = 0.0f;
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
