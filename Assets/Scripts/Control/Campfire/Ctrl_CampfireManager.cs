using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_CampfireManager : Singleton<Ctrl_CampfireManager>
{
    [SerializeField] private Ctrl_FuelSlot fuelSlot;
    [SerializeField] private Button switchBtn;
    private bool isCombustion;

    public bool IsCombustion
    {
        get { return isCombustion; }

        set { isCombustion = value; }
    }

    public void OnSwitch()
    {
        if (fuelSlot.Item != null)
        {
            isCombustion = !isCombustion;
            if (isCombustion)
            {
                switchBtn.GetComponentInChildren<Text>().text = "关火";
                StartCoroutine(Combustion());
            }
            else
            {
                switchBtn.GetComponentInChildren<Text>().text = "开火";
                StopAllCoroutines();
            }
        }
        else
        {
            Ctrl_TootipManager.Instance.ShowNotification("你没有添加燃料!");
        }
    }

    IEnumerator Combustion()
    {
        yield return new WaitForSeconds(fuelSlot.Item.consumption);
        fuelSlot.Item.currentNumber -= 1;
        if (fuelSlot.Item.currentNumber <= 0)
        {
            fuelSlot.Item = null;
            switchBtn.GetComponentInChildren<Text>().text = "开火";
            IsCombustion = false;
        }
        else
        {
            fuelSlot.UpdateAmount(fuelSlot.Item);
            StartCoroutine(Combustion());
        }
    }

    private void Update()
    {
        Debug.Log(IsCombustion);
    }
}