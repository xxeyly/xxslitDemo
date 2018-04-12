using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_EquipmentTootip : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text itemName;
//    [SerializeField] private Text DescribeOutside; //外面的文本控制大小
    [SerializeField] private Text DescribeInside; //里面的文本控制显示
//    [SerializeField] private Text EquipmentAttributeOutside; //装备/物品属性外围控制大小
    [SerializeField] private Text EquipmentAttributeInside; //装备/物品属性里面控制大小

    public void InitView(Model_Item item)
    {
        icon.sprite = Resources.Load<Sprite>(item.sprite);
        itemName.text = item.itemName;
//        DescribeOutside.text = item.description;
        DescribeInside.text = item.description;
//        EquipmentAttributeOutside.text = item.EquipTootip() + item.ConsumableTootip() + "\n";
        EquipmentAttributeInside.text = item.EquipTootip() + item.ConsumableTootip();
    }
}