using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_DemandItem : MonoBehaviour
{
    private Model_Item.Drawing drawing;

    public Model_Item.Drawing Drawing
    {
        get { return drawing; }

        set
        {
            //只有在第一次实例化也就是当前值为空的时候才会把当前值赋值给原始值
            if (drawing == null)
            {
                originalQuantity = value.itemCount;
            }
            drawing = value;
            //UI更新
            GetComponent<View_DemandItem>().InitView(value);
        }
    }
    //原始值 也就是倍数为1的情况下 所需的个数
    public int OriginalQuantity
    {
        get { return originalQuantity; }
    }
    private int originalQuantity;
}