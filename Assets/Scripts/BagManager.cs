using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    private static BagManager instance;
    private Dictionary<Item, Slot> slotDict=new Dictionary<Item, Slot>();
    public Bag bag;
    public Slot slotPrefab;
    public GameObject slotGrid;
    public Text txtInfo;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        RefreshUI();
    }

    public static void CreatNewItem(Item item)
    {
        //如果在背包列表中找到有这个物品，马上去检查背包UI中的这个物品
        
        if (instance.bag.itemList.Contains(item)&&item.num>1)
        {
            if (instance.slotDict.TryGetValue(item, out Slot slotItem))
            {
                slotItem.itemNum.text = item.num.ToString();
                Debug.Log(item.num);
            }
        }
        else
        {
            Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
            //把该物品挂载到背包的Grid下
            newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
            //把物品item的参数传输给这个物品栏Slot
            newItem.item = item;
            newItem.imgitem.sprite = item.img;
            newItem.itemNum.text = item.num.ToString();
            //将这个物品添加到背包UI字典中，方便查找背包中的该物品
            instance.slotDict.Add(newItem.item, newItem);
        }

    }

    private void RefreshUI()
    {
        foreach (Item item in bag.itemList)
        {
            Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
            //把该物品挂载到背包的Grid下
            newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
            //把物品item的参数传输给这个物品栏Slot
            newItem.item = item;
            newItem.imgitem.sprite = item.img;
            newItem.itemNum.text = item.num.ToString();
            //将这个物品添加到背包UI字典中，方便查找背包中的该物品
            instance.slotDict.Add(newItem.item, newItem);
        }
    }

    public static void UpdateInfo(string itemInfo)
    {
        instance.txtInfo.text = itemInfo;
    }

}
