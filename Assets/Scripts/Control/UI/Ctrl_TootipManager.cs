using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_TootipManager : Singleton<Ctrl_TootipManager>
{
    [SerializeField] private GameObject Notification; //普通弹窗
    [SerializeField] private GameObject NotificationLevel; //升级弹出
    [SerializeField] private GameObject ShopTootip; //商店购买弹窗
    [SerializeField] private GameObject QuestTootip; //任务弹窗
    [SerializeField] private GameObject MakeTootip; //谈话弹窗
    [SerializeField] private GameObject AgreeCancelTootip; //保留还是丢弃弹窗
    [SerializeField] private GameObject PlayerReadingTootip; //保留还是丢弃弹窗
    public GameObject PickUpItem;
    public bool IsPickedItem { get; set; }

    public GameObject AgreeCancel
    {
        get { return AgreeCancelTootip; }
    }

    [SerializeField] private Canvas canvas;
    public Transform UICanvas
    {
        get
        {
            return canvas.transform;
        }
    }
    //Tootip偏移
    [SerializeField] private Vector2 toolTipPosionOffset;
    [SerializeField] public GameObject itemTootip;
    public bool isToolTipShow = false;

    /// <summary>
    /// 显示商店购选Tootip
    /// </summary>
    /// <param name="item"></param>
    /// <param name="position"></param>
    public void ShowShopTootip(Model_Item item, Vector2 position)
    {
        ShopTootip.SetActive(true);
        ShopTootip.GetComponent<View_BuyTootip>().BuyItem = item;
//        Tootip.GetComponent<View_ToolTip>().SetLocalPotion(position + toolTipPosionOffset);
        ShopTootip.transform.localPosition = position;
    }

    /// <summary>
    /// 隐藏商店购选
    /// </summary>
    public void HideShopTootip()
    {
        ShopTootip.SetActive(false);
    }

    /// <summary>
    /// 显示普通弹窗 金币不足 魔法不足 使用该弹窗
    /// </summary>
    /// <param name="contentStr">内容</param>
    /// <param name="showTime">显示时间</param>
    /// <param name="headStr">标题,默认是"系统提示"</param>
    public void ShowNotification(string contentStr, float showTime = GlobalParametr.DEFAULTSHOWTIME,
        string headStr = "系统提示")
    {
        Notification.GetComponent<View_Notification>().ShowNotification(contentStr, showTime, headStr);
    }

    /// <summary>
    /// 显示升级弹窗 
    /// </summary>
    /// <param name="contentStr">内容</param>
    /// <param name="showTime">显示时间</param>
    /// <param name="headStr">标题,默认是"你升级了"</param>
    public void ShowNotificationLevel(string contentStr, float showTime = GlobalParametr.DEFAULTSHOWTIME,
        string headStr = "你升级了")
    {
        NotificationLevel.GetComponent<View_NotifcationLevel>().ShowNotification(contentStr, showTime, headStr);
    }

    /// <summary>
    /// 显示任务对话弹窗,并赋值任务对话详情
    /// </summary>
    /// <param name="quest"></param>
    public void ShowQuest(Model_Quest quest)
    {
        QuestTootip.gameObject.SetActive(true);
        QuestTootip.GetComponent<Ctrl_QuestTootip>().Quest = quest;
    }

    /// <summary>
    /// 仅仅显示任务对话 任务中与NPC谈话使用
    /// </summary>
    public void ShowQuest()
    {
        QuestTootip.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏任务对话
    /// </summary>
    public void HideQuest()
    {
        QuestTootip.gameObject.SetActive(false);
    }

    /// <summary>
    /// NPC对话弹窗 只用于NPC任务谈话
    /// </summary>
    /// <param name="questNpc">NPC</param>
    public void TalkInMission(Model_Quest.QuestNPC questNpc)
    {
        QuestTootip.GetComponent<Ctrl_QuestTootip>().TalkInMission(questNpc);
    }

    private void Update()
    {
        if (IsPickedItem)
        {
            //如果我们捡起了物品，我们就要让物品跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);
            PickUpItem.GetComponent<Ctrl_PickUp>().SetLocalPosition(position + toolTipPosionOffset);
        }

        if (isToolTipShow)
        {
            //控制提示面板跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera, out position);
            itemTootip.GetComponent<Ctrl_ItemTootip>().SetLocalPotion(position + toolTipPosionOffset);
        }
        else
        {
            itemTootip.GetComponent<Ctrl_ItemTootip>().HideItemTootip();
        }
    }

    /// <summary>
    ///  显示物品信息
    /// </summary>
    /// <param name="item"></param>
    public void ShowItemInfo(Model_Item item)
    {
        itemTootip.GetComponent<Ctrl_ItemTootip>().ShowItemInfo(item);
    }

    /// <summary>
    /// 显示制作需要的材料
    /// </summary>
    /// <param name="item"></param>
    public void ShowMakeInfo(Model_Item item)
    {
        ShowMakeTootip();
        MakeTootip.GetComponent<Ctrl_MakeTootip>().Item = item;
    }

    /// <summary>
    /// 制作界面是否显示
    /// </summary>
    /// <returns></returns>
    public bool MakeTootipIsShow()
    {
        return MakeTootip.activeSelf;
    }

    /// <summary>
    /// 显示制作面板
    /// </summary>
    public void ShowMakeTootip()
    {
        MakeTootip.SetActive(true);
    }

    /// <summary>
    /// 隐藏制作面板
    /// </summary>
    public void HideMakeTootip()
    {
        MakeTootip.SetActive(false);
    }

    /// <summary>
    /// 显示保留/丢弃弹窗
    /// </summary>
    public void ShowAgreeCancel(string titleContent)
    {
        AgreeCancelTootip.GetComponent<Ctrl_AgreeCancelTootip>().ChangeTitleContent(titleContent);
        AgreeCancelTootip.SetActive(true);
    }

    /// <summary>
    /// 隐藏保留/丢弃弹窗
    /// </summary>
    public void HideAgreeCancel()
    {
        AgreeCancelTootip.SetActive(false);
    }

    /// <summary>
    /// 显示存取档界面
    /// </summary>
    /// <param name="isArchiving"></param>
    public void ShowPlayerReadingTootip(bool isArchiving)
    {
        PlayerReadingTootip.SetActive(true);
        PlayerReadingTootip.GetComponent<Ctrl_PlayerAccessFile>().IsArchiving = isArchiving;
    }

    /// <summary>
    /// 隐藏存取档界面
    /// </summary>
    public void HidePlayerReadingTootip()
    {
        PlayerReadingTootip.SetActive(false);
    }
}