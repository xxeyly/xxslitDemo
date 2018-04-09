using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_MakeSlot : MonoBehaviour
{
    [SerializeField] private Image icon; //制作物品图片
    [SerializeField] private Text makeCount;
    [SerializeField] private Scrollbar makeProgress; //制作进度
    /// <summary>
    /// 更新进度UI
    /// </summary>
    /// <param name="progress"></param>
    public void UpdateMakeProgress(float progress)
    {
        makeProgress.size = progress;
    }
    /// <summary>
    /// 更新制作个数
    /// </summary>
    /// <param name="count"></param>
    public void UpdateMakeCount(int count)
    {
        makeCount.text = count + "X";
    }
  
    /// <summary>
    /// UI初始化
    /// </summary>
    /// <param name="item"></param>
    public void InitView(Model_Item item,int count)
    {
        gameObject.SetActive(true);
        icon.sprite = Resources.Load<Sprite>(item.sprite);
        makeCount.text = count + "X";
        makeProgress.size = item.makeTime / item.makeTime; //设置当前制作进度为满
    }

}