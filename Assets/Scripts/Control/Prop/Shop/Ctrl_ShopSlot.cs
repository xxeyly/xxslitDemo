using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_ShopSlot : Singleton<Ctrl_ShopSlot>
{
    private Model_Item item;

    public Model_Item Item
    {
        get { return item; }

        set { item = value; }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}