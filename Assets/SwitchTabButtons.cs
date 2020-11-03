using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTabButtons : MonoBehaviour
{
    public Image buttonImg;
    public Color selectedColor;
    public Color notSelectedColor;
    public Tabs tabType;

    private bool isSelected = false;
    private TabManager tabManager;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SelectTab);
        TabManager.TabChanged += OnTabChanged;
        tabManager = FindObjectOfType<TabManager>();
    }

    private void OnTabChanged(Tabs newTab)
    {
        isSelected = newTab == tabType;
        if (isSelected)
            buttonImg.color = selectedColor;
        else
            buttonImg.color = notSelectedColor;
    }
    private void SelectTab()
    {
        tabManager.SwitchTabTo(tabType);
    }
}
