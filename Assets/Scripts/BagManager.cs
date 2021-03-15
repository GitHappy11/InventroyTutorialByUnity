using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    private static BagManager instance;
    private Dictionary<Item, Slot> slotDict=new Dictionary<Item, Slot>();
    public Bag bag;
    //public Slot slotPrefab;
    //与不拖拽的背包区分一下，能够拖拽的背包是一开始就把背包列表填满（包括UI和ObjectScripts）然后Slot拖拽里面的按钮【以更换按钮位置来实现更换装备位置】
    public GameObject empSlot;
    public GameObject slotGrid;

    public List<GameObject> slotLst = new List<GameObject>();
    public Text txtInfo;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        RefreshUI();
    }


    #region 顺序背包。按顺序创建新的预制体（如果已存在就无需创建新预制体，直接更新数据即可）
    //public static void CreatNewItem(Item item)
    //{
    //    //如果在背包列表中找到有这个物品，马上去检查背包UI中的这个物品

    //    if (instance.bag.itemList.Contains(item)&&item.num>1)
    //    {
    //        if (instance.slotDict.TryGetValue(item, out Slot slotItem))
    //        {
    //            slotItem.itemNum.text = item.num.ToString();
    //            Debug.Log(item.num);
    //        }
    //    }
    //    else
    //    {
    //        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
    //        //把该物品挂载到背包的Grid下
    //        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
    //        //把物品item的参数传输给这个物品栏Slot
    //        newItem.item = item;
    //        newItem.imgitem.sprite = item.img;
    //        newItem.itemNum.text = item.num.ToString();
    //        //将这个物品添加到背包UI字典中，方便查找背包中的该物品
    //        instance.slotDict.Add(newItem.item, newItem);
    //    }

    //}


    //private void RefreshUI()
    //{
    //    foreach (Item item in bag.itemList)
    //    {
    //        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
    //        //把该物品挂载到背包的Grid下
    //        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
    //        //把物品item的参数传输给这个物品栏Slot
    //        newItem.item = item;
    //        newItem.imgitem.sprite = item.img;
    //        newItem.itemNum.text = item.num.ToString();
    //        //将这个物品添加到背包UI字典中，方便查找背包中的该物品
    //        instance.slotDict.Add(newItem.item, newItem);
    //    }
    //}
    #endregion

    public static void RefreshUI()
    {
        //生成这次背包之前先清空背包格
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount==0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            //清空背包信息
            instance.slotLst.Clear();
            
        }
        //生成出与(ObjectScript)背包相应的背包格数，添加进列表
        for (int i = 0; i < instance.bag.itemList.Count; i++)
        {
            //生成
            GameObject empSlot = Instantiate(instance.empSlot);
            //添加进列表进行管理
            instance.slotLst.Add(empSlot) ;
            //设置父物体
            instance.slotLst[i].transform.SetParent(instance.slotGrid.transform);
            //生成物品
            instance.slotLst[i].GetComponent<Slot>().SetUpSlot(instance.bag.itemList[i]);
        }
    }

    public static void UpdateInfo(string itemInfo)
    {
        instance.txtInfo.text = itemInfo;
    }

}
