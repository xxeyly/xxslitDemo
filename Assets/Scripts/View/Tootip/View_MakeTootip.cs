using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_MakeTootip : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text itemName;
    [SerializeField] private Text makeCount;

    public void InitView(Model_Item item)
    {
        icon.sprite = Resources.Load<Sprite>(item.sprite);
        itemName.text = item.itemName;
    }

    public void UpdateMakeCount(int count)
    {
        makeCount.text = count.ToString();
    }
}