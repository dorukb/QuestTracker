using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyScroller : MonoBehaviour, IDragHandler
{
    public Canvas parentCanvas;
    public int moveSpeed = 10;
    public float startY;
    public float minY;

    public float maxDistance;


    public MyScrollView scrollView;
    void Start()
    {
        scrollView = FindObjectOfType<MyScrollView>();
        maxDistance = minY - startY;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        var myPos = transform.position;
        var pos = parentCanvas.transform.TransformPoint(movePos);
        myPos.y = pos.y;

        myPos.y = Mathf.Clamp(myPos.y, minY, startY);

        float perct = Mathf.Abs((myPos.y - startY) / maxDistance);
        scrollView.Move(perct);

        //Debug.LogFormat("myPos.y:{0}   minY:{1}, startY:{2}", myPos.y, minY, startY);
        transform.position = myPos;
    }

    //void Awake()
    //{
    //    rectTrans = GetComponent<RectTransform>();
    //    startY = rectTrans.anchoredPosition.y;
    //    maxDistance = Mathf.Abs(minY - startY);
    //}

}
