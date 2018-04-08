using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_ProductionNeeds : MonoBehaviour
{
    private GridLayoutGroup grid;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetGridHeight(1);
    }

    public void SetGridHeight(int num) //每行Cell的个数  
    {
        grid = this.GetComponent<GridLayoutGroup>();
        float childCount = this.transform.childCount; //获得Layout Group子物体个数  

        float height = ((childCount + num - 1) / num) * grid.cellSize.y + 3.0f; //行数乘以Cell的高度，3.0f是微调  
        height += (((childCount + num - 1) / num) - 1) * grid.spacing.y; //每行之间有间隔  
        grid.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}