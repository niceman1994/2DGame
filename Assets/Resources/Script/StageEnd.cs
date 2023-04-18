using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageEnd : MonoBehaviour
{
    [SerializeField] private Text ClearText;
    [SerializeField] private GameObject Panel;
    private Color panelColor;

    void Start()
    {
        panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, panelColor.a);
        StartCoroutine(StageEndText());
    }

    IEnumerator StageEndText()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(6.0f);

        while (true)
        {
            yield return null;

            if (EnemyManager.Instance.StageClear == true)
            {
                Panel.SetActive(true);
                ClearText.gameObject.SetActive(true);
                yield return waitForSeconds;
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
}
