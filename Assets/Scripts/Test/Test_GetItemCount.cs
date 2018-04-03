using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_GetItemCount : MonoBehaviour {

    [SerializeField] private InputField ItemId;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetItemCount()
    {
        int itemID = Int32.Parse(ItemId.text);
        Debug.Log(Ctrl_InventoryManager.Instance.GetItemCount(itemID));
    }

}
