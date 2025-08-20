using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // 씬 전환용

[System.Serializable]
public class GuidePage
{
    public string title;               // 제목
    public Sprite image;               // 이미지
    [TextArea] public string content; // 설명
}

public class GuideManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public Image guideImage;
    public TextMeshProUGUI contentText;
    public Button nextButton;

    [Header("Guide Pages")]
    public GuidePage[] pages; // Inspector에서 바로 세팅

    private int currentPage = 0;

    void Start()
    {
        ShowPage(0);

        // 버튼 하나에 클릭 이벤트 연결
        nextButton.onClick.AddListener(OnNextButtonClick);
    }

    void ShowPage(int index)
    {
        if (index < 0 || index >= pages.Length) return;

        var page = pages[index];
        titleText.text = page.title;
        guideImage.sprite = page.image;
        contentText.text = page.content;

        // 마지막 페이지라면 버튼 텍스트를 "확인"으로 변경
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
            // 마지막 페이지면 MainScene으로 씬 전환
            SceneManager.LoadScene("MainScene");
        }
    }
}
