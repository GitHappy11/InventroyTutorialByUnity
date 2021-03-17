using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//每一个Slot相当于一个背包格，记录了这个背包格的信息，item则是物品
public class Slot : MonoBehaviour
{
    //该背包格装的是什么物品
    public Item item;
    //物品的图标
    public Image imgitem;
    //物品的数量
    public Text itemNum;
    //物品的信息
    public string slotInfo;

    //该背包格的ID  以便记录
    public int bagID;

    public GameObject itemInSlot;

    private void Awake()
    {
        
    }

    public void SetUpSlot(Item item)
    {
        if (item==null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        imgitem.sprite = item.img;
        itemNum.text = item.num.ToString();
        slotInfo = item.info;
    }

    public void ItemOnClick()
    {
        BagManager.UpdateInfo(slotInfo);
    }

}
