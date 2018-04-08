using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_MakeSlot : MonoBehaviour
{
    private Model_Item item;
    private int makeCount;

    public Model_Item Item
    {
        get { return item; }

        set
        {
            item = value;
            GetComponent<View_MakeSlot>().InitView(value);
        }
    }

    public int MakeCount
    {
        get { return makeCount; }

        set
        {
            makeCount = value;
            GetComponent<View_MakeSlot>().StartMake(makeCount);
        }
    }
}