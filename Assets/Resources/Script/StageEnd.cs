using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageEnd : MonoBehaviour
{
    [SerializeField] private Text ClearText;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject Boss;
    private Color panelColor;

    void Start()
    {
        panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, panelColor.a);
    }

    void Update()
    {
        BossDisappear();
        StageEndText();
    }

    void BossDisappear()
    {
        if (Boss == null)
            Boss = GameObject.FindGameObjectWithTag("Boss");
        else
            return;
    }

    void StageEndText()
    {
        if (Boss.activeInHierarchy == false)
        {
            panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, 0.0f <= 255.0f ? 0.0f + Time.deltaTime : 255.0f);

            if (panelColor.a >= 255.0f)
                ClearText.gameObject.SetActive(true);
        }
    }
}
