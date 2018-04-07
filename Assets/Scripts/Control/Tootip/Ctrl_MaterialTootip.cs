using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_MaterialTootip : MonoBehaviour
{
    private Model_Item item;

    public Model_Item Item
    {
        get { return item; }

        set
        {
            item = value;
            GetComponent<View_MaterialTootip>().InitView(value);
        }
    }
}