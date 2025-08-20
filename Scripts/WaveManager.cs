using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;     // "Wave 1" ǥ�� UI
    public TextMeshProUGUI timerText;    // "00:20" ǥ�� UI

    public float waveDuration = 20f;     // �� ���̺� �ð�(��)
    private float remainingTime;         // ���� �ð�
    public int currentWave = 1;         // ���� ���̺� ��ȣ

    public Player player; // Player ��ũ��Ʈ ���� �߰�

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        // Ÿ�̸� ����
        remainingTime -= Time.deltaTime;

        // 0 ���� �Ǹ� ���� ���̺�
        if (remainingTime <= 0f)
        {
            currentWave++;
            StartWave();

            // ���̺� ������ �� �÷��̾�� 20 ��� �߰�
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
        // ���� �ð��� ��:�� �������� ��ȯ
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
