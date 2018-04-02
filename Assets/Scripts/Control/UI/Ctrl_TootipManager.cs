using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_TootipManager : Singleton<Ctrl_TootipManager>
{
    [SerializeField] private GameObject Notification;
    [SerializeField] private GameObject NotificationLevel;
    [SerializeField] private GameObject ShopTootip;
    [SerializeField] private GameObject QuestTootip;

    public void ShowShopTootip(Model_Item item, Vector2 position)
    {
        ShopTootip.SetActive(true);
        ShopTootip.GetComponent<View_BuyTootip>().BuyItem = item;
//        Tootip.GetComponent<View_ToolTip>().SetLocalPotion(position + toolTipPosionOffset);
        ShopTootip.transform.localPosition = position;
    }

    public void HideShopTootip()
    {
        ShopTootip.SetActive(false);
    }

    public void ShowNotification(string contentStr, float showTime = GlobalParametr.DEFAULTSHOWTIME,
        string headStr = "系统提示")
    {
        Notification.GetComponent<View_Notification>().ShowNotification(contentStr, showTime, headStr);
    }

    public void ShowNotificationLevel(string contentStr, float showTime = GlobalParametr.DEFAULTSHOWTIME,
        string headStr = "你升级了")
    {
        NotificationLevel.GetComponent<View_NotifcationLevel>().ShowNotification(contentStr, showTime, headStr);
    }

    public void ShowQuest(Model_Quest quest)
    {
        QuestTootip.gameObject.SetActive(true);
        QuestTootip.GetComponent<Ctrl_QuestTootip>().Quest = quest;
    }

    public void HideQuest()
    {
        QuestTootip.gameObject.SetActive(false);
    }
}