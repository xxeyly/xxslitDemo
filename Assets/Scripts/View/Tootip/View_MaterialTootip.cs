using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_MaterialTootip : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text itemName;
    [SerializeField] private Text materialAttributeInside; //里面的文本控制显示
    [SerializeField] private Text describeInside; //里面的文本控制显示
    [SerializeField] private GameObject materialAttribute; //材料的属性 如果没有,就不显示

    public void InitView(Model_Item item)
    {
        icon.sprite = Resources.Load<Sprite>(item.sprite);
        itemName.text = item.itemName;
        if (item.MateriaTootip() == "")
        {
            materialAttribute.SetActive(false);
        }
        else
        {
            materialAttribute.SetActive(true);
            materialAttributeInside.text = item.MateriaTootip();
        }

        describeInside.text = item.description;
    }
}