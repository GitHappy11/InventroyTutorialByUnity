using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image imgitem;
    public Text itemNum;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            BagManager.UpdateInfo(item.info);
        });
    }

}
