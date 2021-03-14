using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//每个人的背包都不一样
[CreateAssetMenu(fileName = "New Bag", menuName = "背包内容/新背包创建")]
public class Bag : ScriptableObject
{
    //物品保存列表
    public List<Item> itemList=new List<Item>();

}
