using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScrollView : MonoBehaviour
{
    public GameObject scrollbar;
    public RectTransform transformToMove;

    public RectTransform topAnchor;
    public float spaceBetweenElements;
    public float childSize;

    float startY;
    float allowedMinY;
    float allowedMaxY;
    private void Start()
    {
        //maxMove = clampHigh.anchoredPosition.y- clampLow.anchoredPosition.y;
        //Debug.Log("Max allowed Y move:startY = transformToMove.anchoredPosition.y;

        startY = topAnchor.anchoredPosition.y;
        allowedMinY = startY;
        ManageScrollAndChildren();

    }

    private void ManageScrollAndChildren()
    {
        float visibleHeight = GetComponent<RectTransform>().rect.height;
        Debug.Log("My height: " + visibleHeight);
        int i = 0;
        float actualHeight = 0;
        foreach (RectTransform child in transform)
        {
            child.anchoredPosition = topAnchor.anchoredPosition - (Vector2.up * i * (childSize + spaceBetweenElements));
            actualHeight = child.anchoredPosition.y - childSize;
            i++;
        }
        float usedHeight = startY - actualHeight;

        if (usedHeight > visibleHeight)
        {
            float neededExtra = usedHeight - visibleHeight;
            allowedMinY = actualHeight - allowedMaxY;
            allowedMaxY = startY + neededExtra;
            Debug.LogFormat("needed extra is: {0} . allowedMinY: {1} ", neededExtra, allowedMinY);
            scrollbar.SetActive(true);
        }
        else
        {
            //hide scrollbar, this wont move anyways.
            scrollbar.SetActive(false);
        }
        //Debug.LogFormat("Current reach is: {0} in Y. Have {1} more to grow before scrolling.", actualHeight, visibleHeight - usedHeight);
    }

    public void OnTransformChildrenChanged()
    {
        ManageScrollAndChildren();
    }
    public void Move(float percentage)
    {
        float maxMove = transform.childCount * (childSize + spaceBetweenElements);
        var oldPos = transformToMove.anchoredPosition;
        oldPos.y = startY + (percentage * maxMove);

        oldPos.y = Mathf.Clamp(oldPos.y, allowedMinY, allowedMaxY);
        transformToMove.anchoredPosition = oldPos;
    }

}
