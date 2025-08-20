using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Skill
{
    public int level = 0;
    public TextMeshProUGUI levelText;   // 스킬 UI
}

public class SkillManager : MonoBehaviour
{
    public Skill[] skills;
    public Button upgradeButton;
    public int upgradeCost = 20;        // 업그레이드 비용
    public Player player;               // Player 스크립트 참조

    void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeRandomSkill);
        UpdateAllUI();
    }

    void UpgradeRandomSkill()
    {
        // 골드 부족 체크
        if (player.gold < upgradeCost)
        {
            Debug.Log("골드 부족! 업그레이드 불가");
            return;
        }

        // 골드 차감
        player.gold -= upgradeCost;
        player.UpdateGoldUI();

        // 랜덤 스킬 1개 레벨업
        int rand = Random.Range(0, skills.Length);
        skills[rand].level += 1;
        UpdateUI(skills[rand]);
    }

    void UpdateUI(Skill skill)
    {
        if (skill.levelText != null)
            skill.levelText.text = "Lv." + skill.level;
    }

    void UpdateAllUI()
    {
        foreach (var skill in skills)
            UpdateUI(skill);
    }
}
