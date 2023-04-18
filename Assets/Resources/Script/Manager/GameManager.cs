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
    public Animator ChargeEffect;

    public Text InsertCoinText;
    public Text GameOverText;
    public Text PhaseInfoText;
    public Text ClearText;

    public Text ScoreText;
    public int Score;

    public Text CoinText;
    public int Coin;

    public GameObject Defeat;
    public Text HaveCoin;
    public Text CountText;
    public int Count;

    public Text PlayerLifeText;
    public int PlayerLife;
    public bool PlayerCharge;
    public bool StageClear;
    public bool GameOver;
    public float GameCount = 60.0f;

    float timer = 0.0f;
    string message;

    private void Start()
	{
        ScoreText.text = "00";
        message = "PHASE  1\n SEASIDE   FRONT";
        CoinCanvas.SetActive(false);
        PlayerCanvas.SetActive(false);
        StartCoroutine(PhaseNotice(message, 0.15f));
        PlayerCharge = false;
        GameOver = false;
        Defeat.SetActive(false);
        StartCoroutine(CountDown());
    }

	private void Update()
	{
        insertCoin();
        ChangeText();
        ChargeCheck();
        GameStop();
        PlayerLifeText.text = PlayerLife.ToString();
        HaveCoin.text = Coin.ToString();

        if (Score != 0)
            ScoreText.text = Score.ToString();

        if (PlayerLife <= 0)
        {
            PlayerLife = 0;
            Time.timeScale = 0;
            Defeat.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Defeat.SetActive(false);
        }

        if (IntroCanvas.activeInHierarchy == false &&
            CoinCanvas.activeInHierarchy == false)
        {
            if (GameCount > 0.0f)
                GameCount -= Time.deltaTime;
            else
                GameCount = 0.0f;
        }
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

                if (Coin < 9)
                    CoinText.text = Coin.ToString();
            }
        }
        else if (Defeat.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) && Coin < 9)
                Coin += 1;

            if (Input.GetKeyDown(KeyCode.Alpha1) && Coin >= 1)
            {
                Coin -= 1;
                PlayerLife = 2;
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

    IEnumerator CountDown()
    {
        WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(1.0f);

        while (true)
        {
            yield return null;

            if (Defeat.activeInHierarchy == true)
            {
                yield return waitForSecondsRealtime;
                Count = Count >= 0 ? Count -= 1 : 0;
                CountText.text = Count.ToString();

                if (Count == -1)
                    UnityEditor.EditorApplication.isPlaying = false;
            }
            else
                Count = 9;      
        }
    }

    void ChargeCheck()
	{
        if (ChargeEffect.GetCurrentAnimatorStateInfo(0).IsName("ChargeEnd"))
            PlayerCharge = true;
        else
            PlayerCharge = false;
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
