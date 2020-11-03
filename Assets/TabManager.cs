using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    static public Action<Tabs> TabChanged;
    public List<TabGameObjectPair> tabSetups;
    public Tabs defaultTab;

    private int activeTabIndex;
    private QuestView currentView;
    void Start()
    {
        SwitchTabTo(defaultTab);
    }

    public void SwitchTabTo(Tabs tab)
    {
        foreach (var setup in tabSetups)
        {
            if (setup.tabType == (Tabs)tab)
            {
                setup.tabView.SetActive(true);
                currentView = setup.tabView.GetComponent<QuestView>();
            }
            else setup.tabView.SetActive(false);
        }
        TabChanged?.Invoke(tab);
        activeTabIndex = (int)tab;
    }
    public GameObject GetCurrentQuestView()
    {
        GameObject currentActiveTab = null;
        foreach(var setup in tabSetups)
        {
            if (setup.tabType == (Tabs)activeTabIndex)
            {
                currentActiveTab = setup.tabView;
                break;
            }
        }
        return currentActiveTab;
    }

    public QuestView GetTabView(Tabs type)
    {
        foreach(var setup in tabSetups)
        {
            if (setup.tabType == type) return setup.tabView.GetComponent<QuestView>();
        }
        return null;
    }

    public void CompleteConfirmCallback()
    {
        currentView.CompleteConfirmCallback();
    }
    public void CompleteCancelCallback()
    {
        currentView.CompleteCancelCallback();
    }
    public void DeleteCancelCallback()
    {
        currentView.DeleteCancelCallback();
    }
    public void DeleteConfirmCallback()
    {
        currentView.DeleteConfirmCallback();
    }
    public void CloseDetailPageCallback()
    {
        currentView.CloseDetailPageCallback();
    }
}

[System.Serializable]
public struct TabGameObjectPair
{
    public Tabs tabType;
    public GameObject tabView;
}
public enum Tabs
{
    Active = 0,
    Completed = 1,
    Main = 2,
    Side = 3,
    Misc = 4,
}
