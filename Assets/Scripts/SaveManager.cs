using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//保存ObjectScript信息
public class SaveManager : MonoBehaviour
{
    //保存背包位置信息
    public Bag bagData;
    //保存物品信息
    public Item itemData;
    //存档文件路径
    public string savePath;

    private void Awake()
    {
        //初始化存档路径
        savePath = Application.persistentDataPath + "/GameData";
    }

    public void SaveGame()
    {
        //没有游戏存档路径的话，就创建一个存档路径
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        //二进制转换
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        //创建存档文件【后缀随意，本质是txt】
        FileStream file = File.Create(savePath+"/Save.save");
        //将需要保存的类数据保存为json文件
        var json = JsonUtility.ToJson(bagData);
        //转化为二进制格式,写入文件夹中
        binaryFormatter.Serialize(file, json);
        //可以使用Using简化
        file.Close();

        Debug.Log("游戏已保存至：" + savePath);
    }

    public void LoadGame()
    {
        if (!Directory.Exists(savePath + "/Save.save"))
        {
            Debug.LogWarning("No SaveFile!");
        }
        else
        {
            //二进制转换
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            //打开文件
            FileStream file = File.Open(savePath + "/Save.save", FileMode.Open);
            //使用存档文件重写序列化的ObjectScript类
            JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), bagData);
            //可以使用Using简化
            file.Close();

        }
        

    }
}
