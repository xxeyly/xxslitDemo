using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Archiving
{
    public int x; //X轴
    public int y; //Y轴
    public int z; //Z轴
    public int hp; //生命
    public int mp; //魔法
    public int level; //等级
    public int exp; //经验
    public int gold; //金币
    public int diamod; //钻石
    public List<Item> items = new List<Item>();
    public List<Equip> equips = new List<Equip>();

    public class Equip
    {
        public GlobalParametr.EquipmentType equip;
        public int itemId;
    }
    public class Item
    {
        public int itemId;
        public int currentCount;
    }
}