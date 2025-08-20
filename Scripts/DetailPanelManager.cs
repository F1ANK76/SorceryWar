using UnityEngine;
using TMPro;

public class DetailPanelManager : MonoBehaviour
{
    public GameObject detailPanel;      // 공용 패널
    public TextMeshProUGUI detailText;  // 패널 안 텍스트

    // 스킬 이름 받아서 설명 표시
    public void ShowSkillDetail(string skillName)
    {
        switch (skillName)
        {
            case "A":
                detailText.text = "Shoots 1 projectile every 1s\nDamage: 10 + skill level";
                break;
            case "B":
                detailText.text = "Shoots every 3s\nDamage: 5 + skill level";
                break;
            case "C":
                detailText.text = "Places 3 traps\nEvery 6s\nLasts 3s\nDamage tick: 1s\nDamage: 15 + skill level";
                break;
        }

        detailPanel.SetActive(true); // 패널 표시
    }

    public void HidePanel()
    {
        detailPanel.SetActive(false); // 패널 닫기
    }
}
