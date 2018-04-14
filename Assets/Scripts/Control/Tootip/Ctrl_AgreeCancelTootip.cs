using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_AgreeCancelTootip : MonoBehaviour
{
    [SerializeField] private Text titleContent;
    public GlobalParametr.del_AgreeCancel del_AgreeCancel;

    /// <summary>
    /// 执行委托,隐藏弹窗
    /// </summary>
    public void OnAgree()
    {
        del_AgreeCancel();
        Ctrl_TootipManager.Instance.HideAgreeCancel();
    }

    /// <summary>
    /// 取消当前有关的委托,并隐藏当前弹窗
    /// </summary>
    public void OnCancel()
    {
        del_AgreeCancel = null;
        Ctrl_TootipManager.Instance.HideAgreeCancel();
    }

    /// <summary>
    /// 更改标题内容
    /// </summary>
    /// <param name="value"></param>
    public void ChangeTitleContent(string value)
    {
        titleContent.text = value;
    }
}