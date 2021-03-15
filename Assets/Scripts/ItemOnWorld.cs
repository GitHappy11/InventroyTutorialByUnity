using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    //表示这是哪个物品
    public Item item;
    //表示这个物品能被什么背包捡起
    public Bag playerBag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //得到的Tag是Player的话就返回true
        if (collision.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    private void AddNewItem()
    {
        //检查背包中是否有这个物品，没有的话添加
        if (!playerBag.itemList.Contains(item))
        {
            //让背包得到这个物品 这里是直接添加一个背包格,并无所谓的空背包格【在拖拽模式中并不适用，因为在拖拽模式中，格子已经提前生成完毕了】
            //playerBag.itemList.Add(item);
            //item.num += 1;

            //找到空背包格，然后直接更改背包格信息
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i]==null)
                {
                    playerBag.itemList[i] = this.item;
                    break;
                }
                
            }
            
        }
        //有的话将数量+1
        else
        {
            item.num += 1;
        }
        //让UI显示(更新)这个物品 后续代码中已经改为直接刷新UI的较为粗暴的方式。
        //BagManager.CreatNewItem(item);
        BagManager.RefreshUI();
    }
}
