using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_FuelSlot : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    [SerializeField] private Text count;
    [SerializeField] private Button switchBtn;
    /// <summary>
    /// UI初始化
    /// </summary>
    /// <param name="item"></param>
    public void InitView(Model_Item item)
    {
        if (item != null)
        {
            icon.gameObject.SetActive(true);
            icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
            count.text = item.currentNumber.ToString();
        }
        else
        {
            icon.gameObject.SetActive(false);
            count.text = " ";
        }
    }
    /// <summary>
    /// 更新UI个数
    /// </summary>
    /// <param name="item"></param>
    public void UpdateAmount(Model_Item item)
    {
        count.text = item.currentNumber.ToString();
    }
    /// <summary>
    /// 更新开火/关火显示
    /// </summary>
    public void UpdateSwitch()
    {
        if (Ctrl_CampfireManager.Instance.IsCombustion)
        {
            switchBtn.GetComponentInChildren<Text>().text = "关火";
        }
        else
        {
            switchBtn.GetComponentInChildren<Text>().text = "开火";
        }
    }
}