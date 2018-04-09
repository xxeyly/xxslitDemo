using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_MakeSlot : MonoBehaviour
{
    private Model_Item item;
    private int makeCount;
    private bool inProduction; //是否正在制作中,如果为True,制作进度条会走动
    private float makeTime; //制作的时间
    private View_MakeSlot V_MakeSlot;

    private void Start()
    {
        V_MakeSlot = GetComponent<View_MakeSlot>();
    }

    /// <summary>
    /// 制作类型
    /// </summary>
    public Model_Item Item
    {
        get { return item; }

        set { item = value; }
    }

    /// <summary>
    /// 制作数量
    /// </summary>
    public int MakeCount
    {
        get { return makeCount; }

        set { makeCount = value; }
    }

    /// <summary>
    /// 初始化制作
    /// </summary>
    public void InitMake()
    {
        GetComponent<View_MakeSlot>().InitView(Item, MakeCount);
    }

    /// <summary>
    /// 开始制作物品
    /// </summary>
    public void StartMake()
    {
        StartCoroutine(Make());
    }

    IEnumerator Make()
    {
        inProduction = true; //已经开始制作,可以更新制作进度了
        yield return new WaitForSeconds(Item.makeTime); //延迟的时间为当前设计图的制作时间
        Ctrl_BlacksmithStation.Instance.AddItem(Item.drawingItemId); //铁匠台添加物品
        //当制作完成后,初始化一些数据
        makeCount--; //当前制作个数-1
        inProduction = false; //取消进度循环
        makeTime = 0; //并将当前制作时间归0
        if (makeCount <= 0) //如果当前制作的格式为0,停止制作协程
        {
            StopAllCoroutines();
            //停止当前制作后调用制作管理类,询问是否有待制作的物品,然后销毁当前对象
            gameObject.SetActive(false);
            Item = null;
            if (Ctrl_MakeQueueManager.Instance.MakeCache.Contains(this))
            {
                Ctrl_MakeQueueManager.Instance.MakeCache.Remove(this);
            }

            Destroy(this.gameObject);
            if (Ctrl_MakeQueueManager.Instance.MakeCache.Count >= 1)
            {
                Ctrl_MakeQueueManager.Instance.StartCacheMake();
            }
        }
        //如果还有,继续制作
        else
        {
            //更新制作个数的UI
            V_MakeSlot.UpdateMakeCount(makeCount);
            StartCoroutine(Make());
        }
    }

    /// <summary>
    /// 更新进度显示
    /// </summary>
    private void Update()
    {
        if (inProduction)
        {
            makeTime += Time.deltaTime;
            V_MakeSlot.UpdateMakeProgress(1 - (makeTime / Item.makeTime));
        }
    }
}