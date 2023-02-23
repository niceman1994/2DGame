using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class smallEnemy5 : Object
{
    [SerializeField] private GameObject BulletPoint;
    GameObject bullet;

    bool inScene;
    DirType dir;
    float attackDelay;

    public override void Initialize()
    {
        base.Name = "smallEnemy5";
        base.Hp = 0;
        base.Speed = 2.5f;
        base.ObjectAnim = GetComponent<Animator>();

        ObjectAnim.speed = 0;
        attackDelay = 0.0f;
        inScene = false;
        GetDirType(1.8f, -1.8f);
    }

    public override void Progress()
    {
        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
           GameManager.Instance.CoinCanvas.activeInHierarchy == false)
        {
            if (transform.position.x <= Camera.main.transform.position.x + BackgroundManager.Instance.xScreenHalfSize)
            {
                inScene = true;
                ShootBullet();
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
        if (collision.gameObject.tag == "Bullet")
        {
            ObjectAnim.SetTrigger("destroy");
            transform.GetComponent<BoxCollider2D>().enabled = false;
            SoundManager.Instance.PlaySE("smallEnemyDestroySound");
            GameManager.Instance.Score += Random.Range(5, 6) * 10;

            StartCoroutine(ReturnObject());
        }
    }

    void GetDirType(float y1, float y2)
    {
        if (transform.position.y >= y1)
        {
            dir.type = 1;
            dir.pos = transform.position;
        }

        if (transform.position.y <= -y2)
        {
            dir.type = 2;
            dir.pos = transform.position;
        }
    }

    public IEnumerator UpDown()
    {
        yield return null;

        WaitForSeconds waitForSeconds = new WaitForSeconds(9.0f);

        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
        {
            yield return waitForSeconds;

            if (gameObject.activeInHierarchy == true)
            {
                ObjectAnim.speed = 1;
                transform.DOMove(new Vector3(26.0f, transform.position.y + Random.Range(-2.0f, 2.0f), 0.0f), 3.0f).SetEase(Ease.Linear);

                if (transform.position.x == 26.0f)
                    ShootBullet();
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

    void ShootBullet()
    {
        if (attackDelay <= 5.0f)
            attackDelay += Time.deltaTime;
        else
        {
            for (int i = 0; i < 5; ++i)
            {
                bullet = Instantiate(EnemyManager.Instance.BullterPrefab);
                bullet.name = "EnemyBullet";
                bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 30.0f - (i * 15.0f));
                bullet.transform.position += new Vector3(
                    BulletPoint.transform.position.x - Speed * 1.2f * Time.deltaTime,
                    BulletPoint.transform.position.y,
                    BulletPoint.transform.position.z);

                attackDelay = 0.0f;
            }
        }
    }
}
