using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // �� ��ȯ��

[System.Serializable]
public class GuidePage
{
    public string title;               // ����
    public Sprite image;               // �̹���
    [TextArea] public string content; // ����
}

public class GuideManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public Image guideImage;
    public TextMeshProUGUI contentText;
    public Button nextButton;

    [Header("Guide Pages")]
    public GuidePage[] pages; // Inspector���� �ٷ� ����

    private int currentPage = 0;

    void Start()
    {
        ShowPage(0);

        // ��ư �ϳ��� Ŭ�� �̺�Ʈ ����
        nextButton.onClick.AddListener(OnNextButtonClick);
    }

    void ShowPage(int index)
    {
        if (index < 0 || index >= pages.Length) return;

        var page = pages[index];
        titleText.text = page.title;
        guideImage.sprite = page.image;
        contentText.text = page.content;

        // ������ ��������� ��ư �ؽ�Ʈ�� "Ȯ��"���� ����
        if (index == pages.Length - 1)
        {
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        }
        else
        {
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        }
    }

    void OnNextButtonClick()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
        else
        {
            // ������ �������� MainScene���� �� ��ȯ
            SceneManager.LoadScene("MainScene");
        }
    }
}
