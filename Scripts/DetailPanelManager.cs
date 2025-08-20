using UnityEngine;
using TMPro;

public class DetailPanelManager : MonoBehaviour
{
    public GameObject detailPanel;      // ���� �г�
    public TextMeshProUGUI detailText;  // �г� �� �ؽ�Ʈ

    // ��ų �̸� �޾Ƽ� ���� ǥ��
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

        detailPanel.SetActive(true); // �г� ǥ��
    }

    public void HidePanel()
    {
        detailPanel.SetActive(false); // �г� �ݱ�
    }
}
