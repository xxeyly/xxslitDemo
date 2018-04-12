using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_DrawingTootip : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text itemName;
    [SerializeField] private Text DescribeInside; //里面的文本控制显示
    [SerializeField] private Text EquipmentAttributeInside; //装备/物品属性里面控制大小
    [SerializeField] private Text MaterialConsumptionInside; //里面的文本控制显示

    public void InitView(Model_Item item)
    {
        icon.sprite = Resources.Load<Sprite>(item.sprite);
        itemName.text = item.itemName;
        DescribeInside.text = item.description;
        MaterialConsumptionInside.text = item.DrawingTootip();
        EquipmentAttributeInside.text = item.EquipTootip() + item.ConsumableTootip();

    }
}