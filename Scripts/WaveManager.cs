using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;     // "Wave 1" 표시 UI
    public TextMeshProUGUI timerText;    // "00:20" 표시 UI

    public float waveDuration = 20f;     // 각 웨이브 시간(초)
    private float remainingTime;         // 남은 시간
    public int currentWave = 1;         // 현재 웨이브 번호

    public Player player; // Player 스크립트 참조 추가

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        // 타이머 감소
        remainingTime -= Time.deltaTime;

        // 0 이하 되면 다음 웨이브
        if (remainingTime <= 0f)
        {
            currentWave++;
            StartWave();

            // 웨이브 시작할 때 플레이어에게 20 골드 추가
            if (player != null)
            {
                player.AddGold(20);
            }
        }

        UpdateUI();
    }

    void StartWave()
    {
        remainingTime = waveDuration;
        waveText.text = "Wave " + currentWave;
    }

    void UpdateUI()
    {
        // 남은 시간을 분:초 형식으로 변환
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
