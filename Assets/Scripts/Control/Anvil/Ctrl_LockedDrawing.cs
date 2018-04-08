using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_LockedDrawing : MonoBehaviour
{

    [SerializeField]private int itemId;//要显示的图纸
    private Model_Item item;

    public Model_Item Item
    {
        get
        {
            return item;
        }

        set
        {
            item = value;
            GetComponent<View_LockedDrawing>().InitView(value);
        }
    }

    private void Start()
    {
        Item = Ctrl_InventoryManager.Instance.NewItem(itemId);
    }

}
