using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : ManagerSingleton<GameManager>
{
    public GameObject CoinCanvas;
    public GameObject YellowPlane;
    public GameObject RedPlane;
    public Text CoinText;

    public int Coin;
}
