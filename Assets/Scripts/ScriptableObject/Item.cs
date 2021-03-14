using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//扩展创建菜单  第一个参数为创建出来的文件名  后面为该功能键的路径
[CreateAssetMenu(fileName ="New Item",menuName ="背包内容/新物品创建")]
public class Item : ScriptableObject  //无需被游戏对象附加，依旧可以使用Awak-Destroy需要依附游戏对象的脚本,适合做一些服务类，以及扩展编辑器
{
    public string itemName;
    public Sprite img;
    public int num;
    [TextArea] //增加可书写行数
    public string info;
    public bool isUse;
    public ItemType itemType;
    


}

public enum ItemType
{
    Liquid,
    Equip,
}
