using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_PlayerCharacter : Singleton<Ctrl_PlayerCharacter>
{
    [SerializeField] private Ctrl_EquipmentSlot[] equipmentSlots;
    [SerializeField] private Dictionary<string, Ctrl_EquipmentSlot> dicEquipmentSlot;

    public Dictionary<string, Ctrl_EquipmentSlot> DicEquipmentSlot
    {
        get
        {
            return dicEquipmentSlot;
        }
    }

    public Ctrl_EquipmentSlot GetCtrlEquipmentSlot(string equipmentName)
    {
        Ctrl_EquipmentSlot equipment;
        DicEquipmentSlot.TryGetValue(equipmentName, out equipment);
        return equipment;
    }

    protected override void Awake()
    {
        base.Awake();
//        equipmentSlots = transform.GetComponentsInChildren<Ctrl_EquipmentSlot>();
    }

    private void Start()
    {
        dicEquipmentSlot = new Dictionary<string, Ctrl_EquipmentSlot>();
        DicEquipmentSlot.Add("Head", equipmentSlots[0]);
        DicEquipmentSlot.Add("Neck", equipmentSlots[1]);
        DicEquipmentSlot.Add("Shoulders", equipmentSlots[2]);
        DicEquipmentSlot.Add("Chest", equipmentSlots[3]);
        DicEquipmentSlot.Add("Back", equipmentSlots[4]);
        DicEquipmentSlot.Add("Bracer", equipmentSlots[5]);
        DicEquipmentSlot.Add("Gloves", equipmentSlots[6]);
        DicEquipmentSlot.Add("Belt", equipmentSlots[7]);
        DicEquipmentSlot.Add("Pants", equipmentSlots[8]);
        DicEquipmentSlot.Add("Boots", equipmentSlots[9]);
        DicEquipmentSlot.Add("Finger", equipmentSlots[10]);
        DicEquipmentSlot.Add("Trinket", equipmentSlots[11]);
        DicEquipmentSlot.Add("Weapon", equipmentSlots[12]);
        DicEquipmentSlot.Add("Shield", equipmentSlots[13]);
    }

}