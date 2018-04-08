using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_LockedDrawing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image _ImgHoverOverlay;
    [SerializeField] private Image _ImgIcon;
    /// <summary>
    /// 点击触发弹窗
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        Ctrl_TootipManager.Instance.ShowMakeInfo(GetComponent<Ctrl_LockedDrawing>().Item);
    }
    /// <summary>
    /// 进入触发显示物品信息
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _ImgHoverOverlay.gameObject.SetActive(true);
        Ctrl_TootipManager.Instance.isToolTipShow = true;
        Ctrl_TootipManager.Instance.ShowItemInfo(GetComponent<Ctrl_LockedDrawing>().Item);
    }
    /// <summary>
    /// 移除隐藏显示
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        _ImgHoverOverlay.gameObject.SetActive(false);
        Ctrl_TootipManager.Instance.isToolTipShow = false;
    }
    /// <summary>
    /// UI的初始化
    /// </summary>
    /// <param name="item"></param>
    public void InitView(Model_Item item)
    {
        _ImgIcon.gameObject.SetActive(true);
        _ImgIcon.sprite = Resources.Load<Sprite>(item.sprite);
    }
}