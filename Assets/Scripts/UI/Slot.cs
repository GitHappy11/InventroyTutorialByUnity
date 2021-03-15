using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image imgitem;
    public Text itemNum;

    public string slotInfo;

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
