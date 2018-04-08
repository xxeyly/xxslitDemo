using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_ProductionNeeds : MonoBehaviour
{
    /// <summary>
    /// 获得所有的制作格子
    /// </summary>
    public List<Ctrl_DemandItem> DemandItems
    {
        get { return new List<Ctrl_DemandItem>(GetComponentsInChildren<Ctrl_DemandItem>()); }
    }

    private int itemId = -1; //上一次图纸的ID

    /// <summary>
    /// 显示制作所需材料
    /// </summary>
    /// <param name="drawing"></param>
    public void DisplayMaterial(Model_Item item, int Multiple = 1)
    {
        //如果要制作的图纸跟上次制作的图纸不一样,清空制作列表
        if (itemId != item.id)
        {
            ClearmakeItem();
            itemId = item.id; //将现在的图纸ID赋值
            //循环遍历所有的所需物品
            foreach (Model_Item.Drawing draw in item.drawing)
            {
                //初始化
                GameObject demandItem = Resources.Load<GameObject>("prefabs/UI/Make/DemandItem");
                demandItem = Instantiate(demandItem);
                demandItem.GetComponent<RectTransform>().localScale = Vector3.one;
                demandItem.transform.parent = this.transform;
                //将所需物品的ID和数量赋值到一个新的类中 防止地址引用错误 
                Model_Item.Drawing newDraw = new Model_Item.Drawing
                {
                    itemId = draw.itemId,//当前需求物品的ID
                    itemCount = draw.itemCount//当前需求物品的个数
                };
                //赋值后更新UI显示
                demandItem.GetComponent<Ctrl_DemandItem>().Drawing = newDraw;
            }
        }
        //如果这次要制作的物品和上次是一样的,就直接使用上次的数据,但是要注意一点,因这次二次使用,当个数累加时,应乘以当前物品
        //需求的原始值
        else
        {
            foreach (Ctrl_DemandItem demandItem in DemandItems)
            {
                //取得当前需求物品的ID,以及原始需求个数乘以倍数
                Model_Item.Drawing newDraw = new Model_Item.Drawing
                {
                    itemId = demandItem.Drawing.itemId,
                    itemCount = demandItem.OriginalQuantity * Multiple
                };
                //更新UI
                demandItem.Drawing = newDraw;
            }
        }
    }

    /// <summary>
    /// 清除MakeItens
    /// </summary>
    public void ClearmakeItem()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
    }
}