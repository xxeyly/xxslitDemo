using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Inventory : MonoBehaviour
{
    [SerializeField] private InputField ItemId; 
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GetEmptySlot()
    {
        Debug.Log(Ctrl_InventoryManager.Instance.emptySlot.name);
    }

    public void AddItem()
    {
        Ctrl_InventoryManager.Instance.AddItem(Int32.Parse(ItemId.text));
    }
}