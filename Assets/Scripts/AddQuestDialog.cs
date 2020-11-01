using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddQuestDialog : MonoBehaviour
{
    public TMP_InputField title;
    public TMP_InputField desc;
    public TMP_InputField timeLeft;

    public GameObject blockingPanel;
    
    private QuestManager questManager;

    public void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void OnEnable()
    {
        if(blockingPanel != null) blockingPanel.SetActive(true);
    }
    public void OnDisable()
    {
        if(blockingPanel != null) blockingPanel.SetActive(false);
    }
    public void ConfirmButtonCallback()
    {
        string questName = title.text;
        string descText = desc.text;
        string daysLeft = timeLeft.text;

        bool empty = string.IsNullOrEmpty(questName) || string.IsNullOrEmpty(descText);
        if (empty) 
        { 
            this.gameObject.SetActive(false);
            title.text = desc.text = timeLeft.text = "";
            return; 
        }

        if (string.IsNullOrEmpty(daysLeft))
            daysLeft = "-1";
        //Debug.LogFormat("name: {0}, desc: {1}, daysLeft: {2}", questName, descText, daysLeft);
        questManager.AddNewQuest(questName, descText, daysLeft);

        title.text = "";
        desc.text = "";
        timeLeft.text = "";
        this.gameObject.SetActive(false);
    }

}
