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
    private bool InProduction;
    private Ctrl_MakeSlot makeSlot;
    float makeTimer = 0;
    private int makeAmount = 0;

    private void Awake()
    {
        makeSlot = GetComponent<Ctrl_MakeSlot>();
    }
    /// <summary>
    /// UI初始化
    /// </summary>
    /// <param name="item"></param>
    public void InitView(Model_Item item)
    {
        icon.sprite = Resources.Load<Sprite>(item.sprite);
        makeCount.text = 1 + "X";
        makeProgress.size = item.makeTime / item.makeTime; //设置当前制作进度为满
    }
    /// <summary>
    /// 开始制作协程
    /// </summary>
    /// <param name="count"></param>
    public void StartMake(int count)
    {
        makeAmount = count;
        makeCount.text = makeAmount + "X";
        StartCoroutine(Make());
    }

    private void Update()
    {
        Debug.Log(makeAmount);
        if (InProduction)
        {
            makeTimer += Time.deltaTime;
            makeProgress.size = 1 - (makeTimer / makeSlot.Item.makeTime);
        }
    }
    /// <summary>
    /// 制作协成(稍后在做详细补充-----未完成)
    /// </summary>
    /// <returns></returns>
    IEnumerator Make()
    {
        InProduction = true;
        yield return new WaitForSeconds(makeSlot.Item.makeTime);
        makeProgress.size = 1;
        makeTimer = 0;
        InProduction = false;
        makeAmount--;
        if (makeAmount <= 0)
        {
            StopAllCoroutines();
            InProduction = false;
        }
        else
        {
            StartCoroutine(Make());
        }
    }
}