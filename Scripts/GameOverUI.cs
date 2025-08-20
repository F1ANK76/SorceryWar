using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI finalWaveText;
    public Button retryButton; // Inspector에서 연결하거나, 코드에서 Find 가능
    void Start()
    {
        int finalWave = PlayerPrefs.GetInt("FinalWave", 0);
        finalWaveText.text = "Final Wave " + finalWave;

        retryButton.onClick.AddListener(OnRetryClicked);
    }

    void OnRetryClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
}
