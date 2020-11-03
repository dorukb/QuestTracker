using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI timeLeftText;

    public Button detailButton;
    public Button deleteButton;
    public Button completeButton;

    [SerializeField] QuestData data;
    public string title => data.title;
    public string description => data.desc;
    public DateTime deadline;
    public QuestType type => data.type;
    public string id => data.id;

    private QuestView questView;

    private void Awake()
    {
        detailButton.onClick.AddListener(DetailButtonCallback);
        deleteButton.onClick.AddListener(DeleteButtonCallback);
        completeButton.onClick.AddListener(CompleteButtonCallback);
    }
    public void Setup(QuestData data, QuestView parentView)
    {
        this.data = data;
        questView = parentView;
        deadline = new DateTime(data.deadline);
        SetupDisplay();
    }
    public void ChangeParentView(QuestView parentView)
    {
        questView = parentView;
        SetupDisplay();
    }
    public void SetupDisplay() 
    {
        titleText.text = title;
        timeLeftText.text = GetTimeLeftDisplay();
        if (data.isCompleted)
        {
            timeLeftText.text = "";
            completeButton.gameObject.SetActive(false);
        }
    }

    public void DetailButtonCallback() 
    {
        questView.ShowDetailPage(data, GetTimeLeftDisplay());
    }
    public void DeleteButtonCallback()
    {
        questView.ShowDeleteQuestPage(id);
    }
    public void CompleteButtonCallback()
    {
        questView.ShowCompleteQuestPage(id);
    }
    public string GetTimeLeftDisplay()
    {

        int daysLeft = (deadline - DateTime.Today).Days;

        if(daysLeft < 1)
        {
            int hoursLeft = (deadline - DateTime.Now).Hours;
            return string.Format("{0} hours", hoursLeft);
        }
        return string.Format("{0} days", daysLeft);
    }
    // add functionality for edit and remove
}

public enum QuestType
{
    Main,
    Side,
    Misc,
};
