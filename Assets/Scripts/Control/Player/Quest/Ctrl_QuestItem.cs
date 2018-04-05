using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl_QuestItem : MonoBehaviour, IPointerDownHandler
{
    private Model_Quest quest;

    public Model_Quest Quest
    {
        get { return quest; }

        set
        {
            quest = value;
            GetComponent<View_QuestItem>().InitQuestItem();
        }
    }

    /// <summary>
    /// 鼠标按下,显示按下效果 并把当前任务详情复制给任务显示面板
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GetComponent<View_QuestItem>().ShowSelectOverlay();
            Ctrl_QuestItemManager.Instance.ShowQuestInfo(Quest);
        }
    }

    /// <summary>
    /// 获得当前QuestItem是否是选中状态
    /// </summary>
    /// <returns></returns>
    public bool GetSelectOverlay()
    {
        return GetComponent<View_QuestItem>().SelectOverlay.activeSelf;
    }

    /// <summary>
    /// 设置选中和不选中
    /// </summary>
    /// <param name="value"></param>
    public void SetSelectOverlay(bool value)
    {
        GetComponent<View_QuestItem>().SelectOverlay.SetActive(value);
    }

    /// <summary>
    /// 显示隐藏任务已完成的标识,默认隐藏
    /// </summary>
    /// <param name="value"></param>
    public void SHOverMaker(bool value)
    {
        GetComponent<View_QuestItem>().SHOverMaker(value);
    }
}