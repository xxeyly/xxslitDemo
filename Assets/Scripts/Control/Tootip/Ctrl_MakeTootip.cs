using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_MakeTootip : MonoBehaviour
{
    [SerializeField] private Ctrl_ProductionNeeds productionNeeds;
    private Model_Item item;

    private int multiple = 1;

    public Model_Item Item
    {
        get { return item; }

        set
        {
            //如果两次点击的设计图为同一张,不做处理

            //为空直接赋值
            if (Item != null)
            {
                //不为空,检测是否跟上次点击的为同一张图纸,不相同在赋值
                if (Item.id != value.id)
                {
                    InitModel(value);
                }
            }
            else
            {
                InitModel(value);
            }
        }
    }

    //初始化数据 
    public void InitModel(Model_Item value)
    {
        item = value;
        GetComponent<View_MakeTootip>().InitView(value); //初始化UI显示,(头像,物品名称,制作个数)
        productionNeeds.DisplayMaterial(value); //初始化显示制作所需的材料
        GetComponent<View_MakeTootip>().UpdateMakeCount(1); //切换图纸后重新设置制作的个数为1
    }

    /// <summary>
    /// 添加制作个数
    /// </summary>
    public void AddMakeCount()
    {
        //如果小于99,代表可以添加个数
        if (multiple < 99)
        {
            multiple++; //制作的倍数
            GetComponent<View_MakeTootip>().UpdateMakeCount(multiple); //更新制作个数UI
            productionNeeds.DisplayMaterial(Item, multiple);
        }
    }

    /// <summary>
    /// 减少制作个数
    /// </summary>
    public void RemoveMakeCount()
    {
        //如果大于1,代表可以减少个数 其他和增加操作一致
        if (multiple > 1)
        {
            multiple--;
            GetComponent<View_MakeTootip>().UpdateMakeCount(multiple);
            productionNeeds.DisplayMaterial(Item, multiple);
        }
    }

    /// <summary>
    /// 开始制作
    /// </summary>
    public void StartMake()
    {
        //如果满足制作条件
        if (PlentyOfGoods() && Ctrl_MakeQueueManager.Instance.MakeSlots.Count <= 7)
        {
            //TUDO 铁匠台减去制作需求的物品
            Ctrl_MakeSlot makeSlot = new Ctrl_MakeSlot
            {
                Item = Item,
                MakeCount = multiple
            };
            for (int i = 0; i < multiple; i++)
            {
                Ctrl_BlacksmithStation.Instance.RemoveItem(makeSlot.Item.drawing);
            }

            //如果正在制作的格子不为空
            if (Ctrl_MakeQueueManager.Instance.GetInProduction() != null)
            {
                Ctrl_MakeQueueManager.Instance.AddMakeSlot(makeSlot);
            }
            //新建一个制作格子,并且开始制作
            else
            {
                Ctrl_MakeQueueManager.Instance.StartMake(makeSlot);
            }
        }
    }

    /// <summary>
    /// 铁匠台的物品是否够制作物品
    /// </summary>
    /// <returns></returns>
    public bool PlentyOfGoods()
    {
        bool abundant = true;
        foreach (Ctrl_DemandItem demandItem in productionNeeds.DemandItems)
        {
            //如果"铁匠台"内的物品小于制作所需物品,设置值为false,只要有一个不满足,就会为False
            if (Ctrl_BlacksmithStation.Instance.GetItemCountById(demandItem.Drawing.itemId) <
                demandItem.Drawing.itemCount)
            {
                abundant = false;
            }
        }

        return abundant;
    }
}