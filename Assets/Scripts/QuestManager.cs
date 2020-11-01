using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject questPrefab;
    public List<Quest> quests;
    public QuestView questView;

    private SaveManager saveManager;
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        // load all quests from disk if there are any.
        List<QuestData> savedQuests = saveManager.GetSavedQuests();
        if(savedQuests != null)
        {
            foreach (var questData in savedQuests)
            {
                CreateQuest(questData);
            }
            UpdateQuestView();
        }
        //Display quests.
    }

    public void DeleteQuest(int id)
    {
        Quest quest = quests.Find(q => q.id == id);
        if (quest != null) 
        {
            quests.Remove(quest);
            Destroy(quest.gameObject);
            UpdateQuestView();
        }
        saveManager.RemoveSaveData(id);
        saveManager.Save();
    }
    public void AddNewQuest(string title, string desc, string daysLeft) 
    {
        int days = Int32.Parse(daysLeft);
        DateTime deadline = DateTime.Today.AddDays(days);

        QuestData data = new QuestData(title, desc, deadline, QuestType.Main, GenerateNewID());
        CreateQuest(data);
        saveManager.AddSaveData(data);
        saveManager.Save();

        UpdateQuestView();
    }

    private void CreateQuest(QuestData questData)
    {
        Quest quest = Instantiate(questPrefab, questView.transform).GetComponent<Quest>();
        quest.Setup(questData);
        quests.Add(quest);
    }
    private void UpdateQuestView()
    {
    }

    private int GenerateNewID()
    {
        return UnityEngine.Random.Range(-10000, 10000);
    }
}
