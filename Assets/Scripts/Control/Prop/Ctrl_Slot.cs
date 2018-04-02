using UnityEngine;

public class Ctrl_Slot : MonoBehaviour
{
    [SerializeField] private Model_Item item;
    public GlobalParametr.del_SlotUseItem slotItemInit;
    private View_Slot viewSlot;

    private void Awake()
    {
        viewSlot = GetComponent<View_Slot>();
    }

    public Model_Item Item
    {
        get { return item; }

        set
        {
            item = value;
            slotItemInit();
        }
    }

    public void UpdateAmount()
    {
        viewSlot.UpdateAmount();
    }

    public void UseItem()
    {
        if (item != null)
        {
            if (item.itemType == "Drugs")
            {
                Ctrl_HeroProperty.Instance.IncreaseHealthValues(item.useHealth);
                Ctrl_HeroProperty.Instance.IncreaseMagicValues(item.useMagic);
                Ctrl_HeroProperty.Instance.UpgradeConition(item.useExperience);
                item.currentNumber -= 1;
                UpdateAmount();
                if (item.currentNumber == 0 && item.useDestroy)
                {
                    Item = null;
                }
            }
            else if (item.itemType == "Equipment")
            {
                foreach (string equipmentname in Ctrl_PlayerCharacter.Instance.DicEquipmentSlot.Keys)
                {
                    if (equipmentname == item.equipmentType)
                    {
                        //如果身上无装备
                        if (Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(item.equipmentType).Item == null)
                        {
                            Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(item.equipmentType).Item = item;
                            Item = null;
                        } //身上有装备
                        else
                        {
                            if (Item.id == Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(item.equipmentType).Item
                                    .id)
                            {
                                return;
                            }

                            int itemID = Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(item.equipmentType).Item.id;
                            Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(item.equipmentType).Item = item;
                            Ctrl_InventoryManager.Instance.AddItem(itemID);
                            Item = null;
                        }

                        break;
                    }
                }

                //TUDO
            }
        }
    }
}