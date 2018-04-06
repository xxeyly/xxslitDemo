using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_CampfireManager : Singleton<Ctrl_CampfireManager>
{
    [SerializeField] private Ctrl_FuelSlot fuelSlot; //火源格子
    [SerializeField] private List<Ctrl_Ingredients> ingredientsList;
    [SerializeField] private List<Ctrl_CookingProductsSlot> cookingProductsList;
    [SerializeField] private ParticleSystem Campfire;

    private bool isCombustion; //是否是开火状态

    /// <summary>
    /// 开火和关火时调用场景的粒子效果-----就是场景中的那个火堆
    /// </summary>
    public bool IsCombustion
    {
        get { return isCombustion; }

        set
        {
            isCombustion = value;
            if (value)
            {
                Campfire.Play();
            }
            else
            {
                Campfire.Stop();
            }
        }
    }

    /// <summary>
    /// 烹饪成品格子---熟肉格子
    /// </summary>
    public List<Ctrl_CookingProductsSlot> CookingProductsList
    {
        get { return cookingProductsList; }

        set { cookingProductsList = value; }
    }

    /// <summary>
    /// 烹饪材料格子---生肉
    /// </summary>
    public List<Ctrl_Ingredients> IngredientsList
    {
        get { return ingredientsList; }

        set { ingredientsList = value; }
    }

    /// <summary>
    /// 火源开关  
    /// 判断当前火源格子是否有火源,如果有开启火源(检测食材格子是否有食材,有的话,也开启食材的协成)
    /// </summary>
    public void OnSwitch()
    {
        if (fuelSlot.Item != null)
        {
            IsCombustion = !IsCombustion;
            if (IsCombustion)
            {
                fuelSlot.UpdateSwitch();
                fuelSlot.Fuel(IsCombustion); //开启火源消耗协程
                foreach (Ctrl_Ingredients ingredients in IngredientsList) //开启食材协程
                {
                    ingredients.Ingredients(IsCombustion);
                }
            }
            else
            {
                fuelSlot.Fuel(IsCombustion); //关闭火源消耗协程
                foreach (Ctrl_Ingredients ingredients in IngredientsList) //关闭食材协程
                {
                    ingredients.Ingredients(IsCombustion);
                }

                fuelSlot.UpdateSwitch();
            }
        }
        else
        {
            Ctrl_TootipManager.Instance.ShowNotification("你没有添加燃料!");
        }
    }
}