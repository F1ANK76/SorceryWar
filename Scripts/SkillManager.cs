using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Skill
{
    public int level = 0;
    public TextMeshProUGUI levelText;   // ��ų UI
}

public class SkillManager : MonoBehaviour
{
    public Skill[] skills;
    public Button upgradeButton;
    public int upgradeCost = 20;        // ���׷��̵� ���
    public Player player;               // Player ��ũ��Ʈ ����

    void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeRandomSkill);
        UpdateAllUI();
    }

    void UpgradeRandomSkill()
    {
        // ��� ���� üũ
        if (player.gold < upgradeCost)
        {
            Debug.Log("��� ����! ���׷��̵� �Ұ�");
            return;
        }

        // ��� ����
        player.gold -= upgradeCost;
        player.UpdateGoldUI();

        // ���� ��ų 1�� ������
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
