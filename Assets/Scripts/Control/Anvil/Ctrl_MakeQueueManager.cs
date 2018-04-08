using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_MakeQueueManager : Singleton<Ctrl_MakeQueueManager>
{
    private List<Ctrl_MakeSlot> makeSlots;

    public List<Ctrl_MakeSlot> MakeSlots
    {
        get { return makeSlots; }

        set { makeSlots = value; }
    }

    void Start()
    {
        makeSlots = new List<Ctrl_MakeSlot>(GetComponentsInChildren<Ctrl_MakeSlot>());
    }
    /// <summary>
    /// 获得空的格子,没有在制作的格子
    /// </summary>
    /// <returns></returns>
    public Ctrl_MakeSlot GetEmptyMakeSlot()
    {
        foreach (Ctrl_MakeSlot MakeSlot in makeSlots)
        {
            if (MakeSlot.Item == null)
            {
                return MakeSlot;
            }
        }

        return null;
    }
    /// <summary>
    /// 开始制作(后续有修改)
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void StartMake(Model_Item item, int count)
    {
        for (int i = 0; i < count; i++)
        {
            //没有合适的格子
            if (GetEmptyMakeSlot() == null)
            {
                Ctrl_TootipManager.Instance.ShowNotification("没有多余的制作空间了,等前面的做完吧,或者关闭一些");
            }
            else
            {
                GetEmptyMakeSlot().Item = item;
                GetEmptyMakeSlot().MakeCount = count;
            }
        }
    }
}