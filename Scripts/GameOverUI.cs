using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI finalWaveText;
    public Button retryButton; // Inspector���� �����ϰų�, �ڵ忡�� Find ����
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
