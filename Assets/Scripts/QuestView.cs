using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestView : MonoBehaviour
{
    public List<GameObject> quests;
    public GameObject blockingPanel;


    public GameObject questDeletePage;

    [Header("Quest Detail Page Fields")]
    public GameObject questDetailPage;
    public TextMeshProUGUI detailTitle;
    public TextMeshProUGUI detailDesc;
    public TextMeshProUGUI detailTimeLeft;


    private int questIdToDelete;
    public void UpdateView()
    {

    }
    public void ShowDetailPage(QuestData data, string timeLeft)
    {
        blockingPanel.SetActive(true);
        questDetailPage.SetActive(true);
        detailTitle.text = data.title;
        detailDesc.text = data.desc;
        detailTimeLeft.text = timeLeft + " left";
    }
    public void ShowDeleteQuestPage(int questId)
    {
        questIdToDelete = questId;
        questDeletePage.SetActive(true);
        blockingPanel.SetActive(true);
    }
    public void DeleteCancelCallback()
    {
        questIdToDelete = 9999; //some invalid id.
        questDeletePage.SetActive(false);
        blockingPanel.SetActive(false);

    }
    public void DeleteConfirmCallback()
    {
        FindObjectOfType<QuestManager>().DeleteQuest(questIdToDelete);
        questDeletePage.SetActive(false);
        blockingPanel.SetActive(false);
    }
    public void CloseDetailPageCallback()
    {
        questDetailPage.SetActive(false);
        blockingPanel.SetActive(false);
    }
}
