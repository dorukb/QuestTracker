using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject questPrefab;
    public List<Quest> quests;

    public TabManager tabManager;
    private SaveManager saveManager;
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        tabManager = FindObjectOfType<TabManager>();
        // load all quests from disk if there are any.
        List<QuestData> savedQuests = saveManager.GetSavedQuests();
        QuestView activeView = tabManager.GetTabView(Tabs.Active);
        QuestView completedView = tabManager.GetTabView(Tabs.Completed);

        //Display quests.
        if (savedQuests != null)
        {
            foreach (var questData in savedQuests)
            {
                if (questData.isCompleted)
                {
                    CreateQuest(questData, completedView);
                }
                else
                {
                    CreateQuest(questData, activeView);
                }
            }
        }
    }

    public void CompleteQuest(string id)
    {
        //remove from active tab, add to completed tab. (with modified visual rep.??)
        Quest quest = quests.Find(q => q.id == id);
        if (quest != null)
        {
            //change its parent to completed tab view.
            QuestView completedView = tabManager.GetTabView(Tabs.Completed);
            quest.gameObject.transform.SetParent(completedView.transform);
            saveManager.UpdateSaveDataOf(id, true);
            saveManager.Save();

            quest.ChangeParentView(completedView);
        }
    }
    public void DeleteQuest(string id)
    {
        Quest quest = quests.Find(q => q.id == id);
        if (quest != null) 
        {
            quests.Remove(quest);
            Destroy(quest.gameObject);
            //UpdateQuestView();
        }
        saveManager.RemoveSaveData(id);
        saveManager.Save();
    }
    public void AddNewQuest(string title, string desc, string daysLeft) 
    {
        int days = Int32.Parse(daysLeft);
        DateTime deadline = DateTime.Today.AddDays(days);

        QuestData data = new QuestData(title, desc, deadline, QuestType.Main, GenerateNewID());

        QuestView currentView= tabManager.GetTabView(Tabs.Active);
        CreateQuest(data,currentView);
        saveManager.AddSaveData(data);
        saveManager.Save();

        //UpdateQuestView();
    }

    private void CreateQuest(QuestData questData, QuestView parent)
    {
        Quest quest = Instantiate(questPrefab, parent.transform).GetComponent<Quest>();
        quest.Setup(questData, parent);
        quests.Add(quest);
        Debug.LogFormat("Added quest:{0} with state{1}", questData.id, questData.isCompleted);
    }
  

    private string GenerateNewID()
    {
        return Guid.NewGuid().ToString();
    }
}
