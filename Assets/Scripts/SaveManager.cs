using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class SaveManager : MonoBehaviour
{
    public string saveFileName = "questSave.json";
    private string saveFilesBasePath;

    private List<QuestData> questData;
    void Awake()
    {
        saveFilesBasePath = Application.persistentDataPath;
        questData = GetSavedQuests();
    }

    public void AddSaveData(QuestData newData) 
    {
        if (questData == null) questData = new List<QuestData>();

        if(!questData.Contains(newData))
            questData.Add(newData);
    }
    public void UpdateSaveDataOf(string id, bool isCompleted)
    {
        var quest = questData.Find(q => q.id == id);
        if (quest != null)
        {
            quest.isCompleted = isCompleted;
        }
    }
    public void RemoveSaveData(string id) 
    {
        if (questData == null) return;
        questData.RemoveAll(q => q.id == id);
    }
    public List<QuestData> GetSavedQuests()
    {
        if (questData == null)
        {
            var saveData = Load<SaveData>();
            if (saveData != null) questData = saveData.questData;
            else questData = new List<QuestData>();
        }
        return questData;
    }
    public bool SaveFileExists()
    {
        return File.Exists(GetSaveFilePath());
    }
    private string GetSaveFilePath()
    {
        return Path.Combine(saveFilesBasePath, saveFileName);
    }

    public void DeleteSaveFile()
    {
        var path = GetSaveFilePath();
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
    private T Load<T>()
    {
        var path = GetSaveFilePath();
        if (SaveFileExists())
        {
            var jsonData = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(jsonData);
            return data;
        }
        return default(T);
    }

    public void Save()
    {
        Save(new SaveData(questData));
    }
    private void Save<T>(T data)
    {
        var path = GetSaveFilePath();
        var jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, jsonData);
        // Debug.Log("Saved state of items to: " + path);
    }
}

[System.Serializable]
public class SaveData
{
    public List<QuestData> questData;
    public SaveData(List<QuestData> data)
    {
        questData = data;
    }
}

[System.Serializable]
public class QuestData
{
    public bool isCompleted;
    public string title;
    public string desc;
    public string id;
    public long deadline;
    public QuestType type;

    public QuestData(string title, string desc, DateTime deadline, QuestType type, string id)
    {
        this.isCompleted = false;
        this.title = title;
        this.desc = desc;
        this.id = id;
        this.deadline = deadline.Ticks;
        this.type = type;
    }
}
