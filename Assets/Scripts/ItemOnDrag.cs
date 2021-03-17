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
    //获得背包管理，得到物品时记录当前该物品在背包里的数据
    public Bag myBag;
    //获得拽起来的物品的背包格ID，以便交换记录
    public int currentBagID;

    //这里的eventData 就是鼠标的一些数据
    public void OnBeginDrag(PointerEventData eventData)
    {
        //标明当前背包格
        originalparent = transform.parent;
        //获得物品的背包格ID
        currentBagID = originalparent.GetComponent<Slot>().bagID;
        //更改物品的父级 使它一直在层级上方，不被其他UI遮挡
        //物品位置与鼠标保持一致
        transform.position = eventData.position;
        //让物品处于父级的父级，也就是上升一级 这里上升两级  因为上升一级会处于Grid的势力范围，导致会被强制排版，所以要脱离Grid
        //如果不想脱离Gird 可以添加一个组件【LayoutElement】第一选项打上√，就可以忽略原来的布局
        transform.SetParent(transform.parent.parent.parent);

        //让物品别挡住鼠标射线 此射线只会检测UI组件
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //物品位置与鼠标保持一致
        transform.position = eventData.position;
        //输出正下方的游戏对象是谁【只能获得UI组件】
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //先判断鼠标下有没有UI组件，没有的话就说明已经拖到UI外了
        if (eventData.pointerCurrentRaycast.gameObject!=null)
        {
            //判断当前鼠标下面的游戏对象名是什么
            if (eventData.pointerCurrentRaycast.gameObject.name == "imgItem")
            {
                //如果拖拽的背包格有装备，自己先占住位置，然后把原本在这的装备换到之前的位置，具体方法就是设置父级，设置位置【其他的Grid会帮您办好的】
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

                //更换两个物品的背包格ID
                //创建临时变量记录，拿起来的物品的背包格物品ID
                var temp = myBag.itemList[currentBagID];
                //获得目标物品的背包格物品ID并赋值，由于背包格是物品的父对象，所以要得到物品父对象的背包信息
                myBag.itemList[currentBagID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().bagID];
                //再把临时记录的背包格物品ID赋给目标背包格
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().bagID] = temp;

                //把原本在这的装备推到我之前的位置  先设置好它父级的位置，然后设置它的父级
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalparent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalparent);
            }
            //没有物品的话就直接然自己进去
            else if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //先看看是不是又放回了原来的位置，如果放回的是原来的位置，就不用做操作了。
                if (!myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().bagID] == myBag.itemList[currentBagID])
                {
                    //设置背包格物品ID  之前的背包格物品ID记得清空，因为物品已经被移走，没有物品
                    myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().bagID] = myBag.itemList[currentBagID];
                    //清空之前的背包格物品ID
                    myBag.itemList[currentBagID] = null;
                }
            }
            //都不是的话，说明拖到其他地方去了，直接回弹
            else
            {
                transform.SetParent(originalparent);
                transform.position = originalparent.position;
            }
        }
        //没有UI组件对象直接回弹
        else
        {
            transform.SetParent(originalparent);
            transform.position = originalparent.position;
        }
        

        //拖拽完毕后，就可以监听了 以便下次拖拽
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
