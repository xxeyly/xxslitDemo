using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_CookingSchedule : MonoBehaviour
{
    [SerializeField] private Image schedule; //烧烤进度
    private float time = 0;

    void Start()
    {
    }
    /// <summary>
    /// 烤肉进度
    /// </summary>
    void Update()
    {
        //首先判断当前是否是开火状态
        if (Ctrl_CampfireManager.Instance.IsCombustion)
        {
            //再次判断当前食材格子是否有食材 如果有增加时间,到达一块烹饪时常重新计算
            if ((Ctrl_CampfireManager.Instance.IngredientsList[0].StartTheTimer ||
                 Ctrl_CampfireManager.Instance.IngredientsList[1].StartTheTimer))
            {
                time += Time.deltaTime;
                schedule.fillAmount = time / GlobalParametr.STEAMINGTIME;

                if (time >= GlobalParametr.STEAMINGTIME)
                {
                    time = 0;
                }
            }
            else
            {
                time = 0;
            }
        }
        else
        {
            time -= 0;
            schedule.fillAmount = time;
        }
    }
}