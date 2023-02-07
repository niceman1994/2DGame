using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : ManagerSingleton<GameManager>
{
    public GameObject IntroCanvas;
    public GameObject CoinCanvas;
    public GameObject PlayerCanvas;

    public Text InsertCoinText;
    public Text GameOverText;

    public Text CoinText;
    public int Coin;

    public Text ScoreText;
    public int Score;

    public Text PlayerLifeText;
    public int PlayerLife;
    public float startTime = 0.0f;

    float timer = 0.0f;

	private void Start()
	{
        CoinCanvas.SetActive(false);
        PlayerCanvas.SetActive(false);
    }

	private void Update()
	{
        startTime += Time.deltaTime;
        insertCoin();
        ChangeText();
	}

    void ChangeText()
	{
        if (CoinCanvas.activeInHierarchy == true)
        {
            timer += Time.deltaTime;

            if (timer >= 1.5f)
            {
                timer = 0.0f;

                if (InsertCoinText.gameObject.activeInHierarchy == true)
                {
                    InsertCoinText.gameObject.SetActive(false);
                    GameOverText.gameObject.SetActive(true);
                }
                else
                {
                    InsertCoinText.gameObject.SetActive(true);
                    GameOverText.gameObject.SetActive(false);
                }
            }
        }
	}

    void insertCoin()
	{
        if (CoinCanvas.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Coin += 1;

                if (Coin <= 99)
                    CoinText.text = Coin.ToString();
            }
        }
	}
}
