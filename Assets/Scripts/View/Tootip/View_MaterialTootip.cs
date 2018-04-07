using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_MaterialTootip : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text itemName;
    [SerializeField] private Text materialAttributeOutside; //外面的文本控制大小
    [SerializeField] private Text materialAttributeInside; //里面的文本控制显示
    [SerializeField] private Text describeOutside; //外面的文本控制大小
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
            materialAttributeOutside.text = item.MateriaTootip() + "\n";
            materialAttributeInside.text = item.MateriaTootip();
        }

        describeOutside.text = item.description + "\n\n";
        describeInside.text = item.description;
    }
}