using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI timeLeftText;

    public Button detailButton;
    public Button deleteButton;

    public string title => data.title;
    public string description => data.desc;
    public DateTime deadline;
    public QuestType type => data.type;
    public int id => data.id;

    private QuestData data;
    private QuestView questView;

    private void Awake()
    {
        detailButton.onClick.AddListener(DetailButtonCallback);
        deleteButton.onClick.AddListener(DeleteButtonCallback);
    }
    private void Start()
    {
        questView = FindObjectOfType<QuestView>();
    }
    //public void Setup(string title, string desc, DateTime deadline = default, QuestType type = QuestType.Main)
    //{
    //    this.data = new QuestData(title, desc, deadline, type, id);
    //    Debug.LogFormat("Added new quest with id: {0}", id);

    //    SetupDisplay();
    //}
    public void Setup(QuestData data)
    {
        this.data = data;
        deadline = new DateTime(data.deadline);
        SetupDisplay();
    }
    public void SetupDisplay() 
    {
        titleText.text = title;
        timeLeftText.text = GetTimeLeftDisplay();
    }

    public void DetailButtonCallback() 
    {
        questView.ShowDetailPage(data, GetTimeLeftDisplay());
    }
    public void DeleteButtonCallback()
    {
        questView.ShowDeleteQuestPage(id);
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
    Misc
};
