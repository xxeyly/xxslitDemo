using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_EquipmentSlot : MonoBehaviour
{
    public GlobalParametr.EquipmentType Equipment;
    private Model_Item item;
    public GlobalParametr.del_Equipment delEquipment;

    public Model_Item Item
    {
        get { return item; }

        set
        {
            item = value;
            if (value == null)
            {
                Ctrl_HeroProperty.Instance.UpEquipmentData(Equipment);
            }
            else
            {
                Ctrl_HeroProperty.Instance.UpEquipmentData(item);
            }

            delEquipment();
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}