using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestView : MonoBehaviour
{
    public List<GameObject> quests;
    public GameObject blockingPanel;


    public GameObject questDeletePage;
    public GameObject questCompletePage;
    [Header("Quest Detail Page Fields")]
    public GameObject questDetailPage;
    public TextMeshProUGUI detailTitle;
    public TextMeshProUGUI detailDesc;
    public TextMeshProUGUI detailTimeLeft;


    private string questIdToDelete;
    private string questIdToComplete;
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
    public void ShowDeleteQuestPage(string questId)
    {
        questIdToDelete = questId;
        Debug.Log("delete quest page: with id: " + questIdToDelete);
        questDeletePage.SetActive(true);
        blockingPanel.SetActive(true);
    }
    public void ShowCompleteQuestPage(string questId)
    {
        questIdToComplete = questId;
        Debug.Log("Complete quest page: with id: " + questId);
        questCompletePage.SetActive(true);
        blockingPanel.SetActive(true);
    }
    public void CompleteConfirmCallback()
    {
        FindObjectOfType<QuestManager>().CompleteQuest(questIdToComplete);
        questCompletePage.SetActive(false);
        blockingPanel.SetActive(false);
    }
    public void CompleteCancelCallback()
    {
        questIdToComplete = "canceled"; // some invalid id
        questCompletePage.SetActive(false);
        blockingPanel.SetActive(false);
    }
    public void DeleteCancelCallback()
    {
        questIdToDelete = "canceled"; //some invalid id.
        questDeletePage.SetActive(false);
        blockingPanel.SetActive(false);
    }
    public void DeleteConfirmCallback()
    {
        Debug.Log("delete confirm with id: " + questIdToDelete);

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
