using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_MakeQueueManager : Singleton<Ctrl_MakeQueueManager>
{
    private List<Ctrl_MakeSlot> makeSlots; //制作的格子
    private List<Ctrl_MakeSlot> makeCache = new List<Ctrl_MakeSlot>();


    /// <summary>
    /// 制作的格子
    /// </summary>
    public List<Ctrl_MakeSlot> MakeSlots
    {
        get { return new List<Ctrl_MakeSlot>(GetComponentsInChildren<Ctrl_MakeSlot>()); }
    }

    /// <summary>
    /// 缓存的格子
    /// </summary>
    public List<Ctrl_MakeSlot> MakeCache
    {
        get { return makeCache; }
    }

    /// <summary>
    /// 开始缓存制作
    /// </summary>
    public void StartCacheMake()
    {
        MakeCache[0].StartMake();
    }

    /// <summary>
    /// 创建新的制作格子
    /// </summary>
    /// <param name="makeSlot"></param>
    public Ctrl_MakeSlot CreateNewMakeSlot(Ctrl_MakeSlot makeSlot)
    {
        GameObject goMakeSlot = Resources.Load<GameObject>("Prefabs/UI/Make/Make Slot");
        goMakeSlot = Instantiate(goMakeSlot);
        goMakeSlot.transform.parent = transform;
        goMakeSlot.transform.localScale = Vector3.one;
        Ctrl_MakeSlot NewMakeSlot = goMakeSlot.GetComponent<Ctrl_MakeSlot>();
        NewMakeSlot.gameObject.SetActive(true);
        NewMakeSlot.Item = makeSlot.Item;
        NewMakeSlot.MakeCount = makeSlot.MakeCount;
        NewMakeSlot.InitMake();
        return NewMakeSlot;
    }

    /// <summary>
    /// 增加队列制作物品
    /// </summary>
    /// <param name="makeSlot"></param>
    public void AddMakeSlot(Ctrl_MakeSlot makeSlot)
    {
        makeCache.Add(CreateNewMakeSlot(makeSlot));
    }

    /// <summary>
    /// 初始化并开始制作(后续有修改)
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void StartMake(Ctrl_MakeSlot makeSlot)
    {
        CreateNewMakeSlot(makeSlot).StartMake();
    }

    /// <summary>
    /// 获得正在制作中的格子
    /// </summary>
    public Ctrl_MakeSlot GetInProduction()
    {
        foreach (Ctrl_MakeSlot makeSlot in MakeSlots)
        {
            if (makeSlot.Item != null && makeSlot.MakeCount >= 1)
            {
                return makeSlot;
            }
        }

        return null;
    }
}