using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : Object
{
    float time;

    public override void Initialize()
    {
        base.Name = "Boss";
        base.Hp = 200;
        base.Speed = 0.0f;
        base.ObjectAnim = null;

        time = 0.0f;
        StartCoroutine(UpDown());
    }

    public override void Progress()
    {
        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
		{
            if (time <= 24.0f)
                time += Time.deltaTime;
            else
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(27.0f, transform.position.y), 0.04f);
        }
    }

    public override void Release()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
        {
            
        }
	}

	IEnumerator UpDown()
	{
        yield return null;

        if (GameManager.Instance.IntroCanvas.activeInHierarchy == false &&
            GameManager.Instance.CoinCanvas.activeInHierarchy == false)
        {
            if (transform.position.x == 27.0f)
            {
                transform.DOPath(new[] { new Vector3(27.0f, 6.33f, 0.0f),
                    new Vector3(27.0f, 6.33f - 0.5f, 0.0f),
                    new Vector3(27.0f, 6.33f , 0.0f) }, 2.0f, PathType.Linear).SetLoops(-1);
            }
            else
                transform.DOKill();
        }
    }
}
