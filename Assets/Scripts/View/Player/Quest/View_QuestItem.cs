using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_QuestItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject activeOverlay; //滑动显示
    [SerializeField] private GameObject selectOverlay; //选中显示
    [SerializeField] private Text questItemText; //任务名称
    [SerializeField] private GameObject questOver; //任务完成标志

    public GameObject SelectOverlay
    {
        get { return selectOverlay; }

        set { selectOverlay = value; }
    }
    /// <summary>
    /// 显示任务Item的显示名称
    /// </summary>
    public void InitQuestItem()
    {
        questItemText.text = GetComponent<Ctrl_QuestItem>().Quest.questBriefIntroduction;
    }
    /// <summary>
    /// 滑出取消选中标识
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        activeOverlay.SetActive(false);
    }

    /// <summary>
    /// 滑动显示
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!SelectOverlay.activeSelf)
        {
            activeOverlay.SetActive(true);
        }
    }

    /// <summary>
    /// 选中显示标识(只有选中才会显示)
    /// </summary>
    public void ShowSelectOverlay()
    {
        Ctrl_QuestItemManager.Instance.HideAllSelectOverlay();
        SelectOverlay.SetActive(true);
    }
    /// <summary>
    ///   显示隐藏任务已完成的标识,默认隐藏
    /// </summary>
    /// <param name="value"></param>
    public void SHOverMaker(bool value)
    {
        questOver.SetActive(value);
    }
}