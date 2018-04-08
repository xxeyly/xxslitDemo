using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_DemandItem : MonoBehaviour
{
    [SerializeField] private Text itemName;
    [SerializeField] private Text demandCount;
    [SerializeField] private Text currentCount;
    /// <summary>
    /// 初始化UI
    /// </summary>
    /// <param name="drawing"></param>
    public void InitView(Model_Item.Drawing drawing)
    {
        itemName.text = Ctrl_InventoryManager.Instance.GetItemById(drawing.itemId).itemName;
        demandCount.text = drawing.itemCount.ToString();
        currentCount.text = Ctrl_BlacksmithStation.Instance.GetItemCountById(drawing.itemId).ToString();
    }

}