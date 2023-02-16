using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : ManagerSingleton<GameManager>
{
    public GameObject IntroCanvas;
    public GameObject CoinCanvas;
    public GameObject PlayerCanvas;
    public GameObject Panel;

    public Text InsertCoinText;
    public Text GameOverText;
    public Text PhaseInfoText;

    public Text ScoreText;
    public int Score;

    public Text CoinText;
    public int Coin;

    public Text PlayerLifeText;
    public int PlayerLife;

    float timer = 0.0f;
    string message;

	private void Start()
	{
        ScoreText.text = "00";
        message = "PHASE  1\n SEASIDE   FRONT";
        CoinCanvas.SetActive(false);
        PlayerCanvas.SetActive(false);
        StartCoroutine(PhaseNotice(message, 0.15f));
    }

	private void Update()
	{
        insertCoin();
        ChangeText();
        GameStop();
        PlayerLifeText.text = PlayerLife.ToString();

        if (Score != 0)
            ScoreText.text = Score.ToString();

        if (PlayerLife <= 0) PlayerLife = 0;
    }

    void ChangeText()
	{
        if (PlayerCanvas.activeInHierarchy == true)
        {
            timer += Time.deltaTime;

            if (timer >= 1.0f)
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

    IEnumerator PhaseNotice(string message, float speed)
	{
        yield return new WaitForSeconds(5.8f);
        PhaseInfoText.gameObject.SetActive(true);

        for (int i = 0; i < message.Length; ++i)
		{
            PhaseInfoText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
		}

        yield return new WaitForSeconds(1.0f);
        PhaseInfoText.gameObject.SetActive(false);
    }

    void GameStop()
	{
        if (PlayerLife == 0)
            Panel.gameObject.SetActive(true);
        else if (Time.timeScale == 0)
            Panel.gameObject.SetActive(true);
        else
            Panel.gameObject.SetActive(false);
	}
}
