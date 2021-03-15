using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//一些事件的监听API   这里使用了鼠标的监听
using UnityEngine.EventSystems;
//增加三个接口 鼠标开始拖拽 正在拖拽  和停止拖拽 
public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //标明你再拖拽之前，你是哪一个背包格的
    public Transform originalparent;

    //这里的eventData 就是鼠标的一些数据
    public void OnBeginDrag(PointerEventData eventData)
    {
        //标明当前背包格
        originalparent = transform.parent;
        //更改物品的父级 使它一直在层级上方，不被其他UI遮挡
        //物品位置与鼠标保持一致
        transform.position = eventData.position;
        //让物品处于父级的父级，也就是上升一级 这里上升两级  因为上升一级会处于Grid的势力范围，导致会被强制排版，所以要脱离Grid
        transform.SetParent(transform.parent.parent.parent);

        //让物品别挡住鼠标射线 此射线只会检测UI组件
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //物品位置与鼠标保持一致
        transform.position = eventData.position;
        //输出正下方的游戏对象是谁 
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        //判断当前鼠标下面的游戏对象名是什么
        if (eventData.pointerCurrentRaycast.gameObject.name=="imgItem")
        {
            //如果拖拽的背包格有装备，自己先占住位置，然后把原本在这的装备换到之前的位置，具体方法就是设置父级，设置位置【其他的Grid会帮您办好的】
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

            //把原本在这的装备推到我之前的位置  先设置好它父级的位置，然后设置它的父级
            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalparent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalparent);
        }
        //没有物品的话就直接然自己进去
        else if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        }
        //都不是的话，说明拖到其他地方去了，直接回弹
        else
        {
            transform.SetParent(originalparent);
            transform.position = originalparent.position;
        }

        //拖拽完毕后，就可以监听了 以便下次拖拽
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
