using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_ItemTootip : MonoBehaviour
{
    [SerializeField] private GameObject materialTootip;
    [SerializeField] private GameObject equipmentTootip;
    [SerializeField] private GameObject drawingTootip;

    public void ShowItemInfo(Model_Item item)
    {
        ShowTootip(item.itemType);
        switch (item.itemType)
        {
            case "Drugs":
                equipmentTootip.GetComponent<Ctrl_EquipmentTootip>().Item = item;
                break;
            case "Equipment":
                equipmentTootip.GetComponent<Ctrl_EquipmentTootip>().Item = item;
                break;
            case "Material":
                materialTootip.GetComponent<Ctrl_MaterialTootip>().Item = item;
                break;
            case "Drawing":
                drawingTootip.GetComponent<Ctrl_DrawingTootip>().Item = item;
                break;
        }
    }

    public void HideItemTootip()
    {
        equipmentTootip.SetActive(false);
        materialTootip.SetActive(false);
        drawingTootip.SetActive(false);
    }

    /// <summary>
    /// 显示指定的提示框
    /// </summary>
    /// <param name="itemType"></param>
    public void ShowTootip(string itemType)
    {
        switch (itemType)
        {
            case "Drugs":
                equipmentTootip.SetActive(true);
                materialTootip.SetActive(false);
                drawingTootip.SetActive(false);
                break;
            case "Equipment":
                equipmentTootip.SetActive(true);
                materialTootip.SetActive(false);
                drawingTootip.SetActive(false);
                break;
            case "Material":
                equipmentTootip.SetActive(false);
                materialTootip.SetActive(true);
                drawingTootip.SetActive(false);
                break;
            case "Drawing":
                equipmentTootip.SetActive(false);
                materialTootip.SetActive(false);
                drawingTootip.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 修改位置
    /// </summary>
    /// <param name="position"></param>
    public void SetLocalPotion(Vector3 position)
    {
        transform.localPosition = position;
    }
}