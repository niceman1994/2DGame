using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class smallEnemy5 : Object
{
    [SerializeField] private GameObject BulletPoint;

    float attackDelay;
    bool inScene;

    public override void Initialize()
    {
        base.Name = "smallEnemy5";
        base.Hp = 30;
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
            if (transform.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize)
            {
                inScene = true;
                ShootBullet(2.0f);
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
            Hp -= 10;

            if (Hp <= 0)
            {
                Hp = 0;
                ObjectAnim.SetTrigger("destroy");
                transform.GetComponent<PolygonCollider2D>().enabled = false;
                SoundManager.Instance.PlaySE("smallEnemyDestroySound");
                GameManager.Instance.Score += Random.Range(6, 7) * 10;

                StartCoroutine(ReturnObject());
            }
        }
    }

    public IEnumerator UpDown()
    {
        yield return null;

        WaitForSeconds waitForSeconds = new WaitForSeconds(43.0f);

        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
        {
            yield return waitForSeconds;

            if (gameObject.activeInHierarchy == true)
            {
                ObjectAnim.speed = 1;
                transform.DOMove(new Vector3(26.0f, transform.position.y + Random.Range(-1.0f, 1.0f), 0.0f), 3.0f).SetEase(Ease.Linear);
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

    void ShootBullet(float _time)
    {
        if (ObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("smallEnemy5"))
        {
            if (attackDelay <= _time)
                attackDelay += Time.deltaTime;
            else
            {
                for (int i = 0; i < 5; ++i)
                {
                    GameObject bullet = Instantiate(EnemyManager.Instance.BullterPrefab);
                    bullet.name = "EnemyBullet";
                    bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 30.0f - (i * 15.0f));
                    bullet.transform.position += new Vector3(
                        BulletPoint.transform.position.x - Speed * 1.1f * Time.deltaTime,
                        BulletPoint.transform.position.y,
                        BulletPoint.transform.position.z);
                }

                attackDelay = 0.0f;
            }
        }
    }
}
