using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class Ctrl_GameReading : Singleton<Ctrl_GameReading>
{
    private string m_sPath; // 路径
    private ArrayList m_aArray; // 文本中每行的内容
    private List<TextAsset> cars = new List<TextAsset>();

    public List<TextAsset> Cars
    {
        get { return cars; }

        set { cars = value; }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 保存进度
    /// </summary>
    public void SaveGame(int saveIndex)
    {
        m_sPath = Application.dataPath + "/Resources/" + "/Data";

        string m_sFileName = "存档" + saveIndex + ".Json";

        string gameData = JsonMapper.ToJson(Ctrl_HeroProperty.Instance.GetHeroCurrentData());
//        Debug.Log(gameData);
        fnCreateFile(m_sPath, m_sFileName, gameData);
    }

    /// <summary>
    /// 读档
    /// </summary>
    /// <param name="archiving"></param>
    public void LoadGame(Model_Archiving archiving)
    {
        Ctrl_HeroProperty.Instance.Gold = archiving.gold;
        Ctrl_HeroProperty.Instance.Diamods = archiving.diamod;
        Ctrl_HeroProperty.Instance.gameObject.transform.position = new Vector3(archiving.x, archiving.y, archiving.z);
        Ctrl_HeroProperty.Instance.SetCurrentHealth(archiving.hp);
        Ctrl_HeroProperty.Instance.SetCurrentMagic(archiving.mp);
        Ctrl_HeroProperty.Instance.SetLevel(archiving.level);
        Ctrl_HeroProperty.Instance.SetCurrentExp(archiving.exp);
        //清空现有的物品
        foreach (Ctrl_Slot slot in Ctrl_InventoryManager.Instance.ActiveSlot())
        {
            slot.Item = null;
        }

        //得到存档的物品
        foreach (Model_Archiving.Item item in archiving.items)
        {
            for (int i = 0; i < item.currentCount; i++)
            {
                Ctrl_InventoryManager.Instance.AddItem(item.itemId);
            }
        }

        foreach (Model_Archiving.Equip equip in archiving.equips)
        {
            switch (equip.equip)
            {
                case GlobalParametr.EquipmentType.None:
                    break;
                case GlobalParametr.EquipmentType.Head:


                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Head.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Head.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Neck:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Neck.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Neck.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Shoulders:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance
                                .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Shoulders.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance
                            .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Shoulders.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Chest:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance
                                .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Chest.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance
                            .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Chest.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Back:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Back.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Back.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Bracer:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance
                                .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Bracer.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance
                            .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Bracer.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Gloves:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance
                                .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Gloves.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance
                            .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Gloves.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Belt:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Belt.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance.GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Belt.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Pants:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance
                                .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Pants.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance
                            .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Pants.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Boots:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance
                                .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Boots.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance
                            .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Boots.ToString())
                            .Item = null;
                    }


                    break;
                case GlobalParametr.EquipmentType.Finger:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance
                                .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Finger.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance
                            .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Finger.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Trinket:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance
                                .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Trinket.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance
                            .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Trinket.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Weapon:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance
                                .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Weapon.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance
                            .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Weapon.ToString())
                            .Item = null;
                    }

                    break;
                case GlobalParametr.EquipmentType.Shield:
                    if (equip.itemId != -1)
                    {
                        Ctrl_PlayerCharacter.Instance
                                .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Shield.ToString())
                                .Item =
                            Ctrl_InventoryManager.Instance.NewItem(equip.itemId);
                    }
                    else
                    {
                        Ctrl_PlayerCharacter.Instance
                            .GetCtrlEquipmentSlot(GlobalParametr.EquipmentType.Shield.ToString())
                            .Item = null;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    //读取当前存档目录有多少个存档
    public void GetCarTextName()
    {
        m_sPath = Application.dataPath + GlobalParametr.SAVEPATH;
        if (Directory.Exists(m_sPath))
        {
            //获取文件信息
            DirectoryInfo direction = new DirectoryInfo(m_sPath);
            //所有的文件名字
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

//            print(files.Length);

            for (int i = 0; i < files.Length; i++)
            {
                //过滤掉临时文件
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }

//                print(files[i].Extension); //这个是扩展名
                //获取不带扩展名的文件名
                string name = Path.GetFileNameWithoutExtension(files[i].ToString());
//                print(name);
                // FileInfo.Name是返回带扩展名的名字 
                Cars.Add(Resources.Load<TextAsset>("Data/" + name));
            }
        }
    }

    /// <summary>
    /// 将字符串转换成文本数据,存储
    /// </summary>
    /// <param name="sPath">文件路径</param>
    /// <param name="sName">文件名称</param>
    /// <param name="nDate">文件数据</param>
    void fnCreateFile(string sPath, string sName, string nDate)
    {
        StreamWriter t_sStreamWriter; // 文件流信息
        FileInfo t_fFileInfo = new FileInfo(sPath + "//" + sName);
        if (!t_fFileInfo.Exists)
        {
            t_sStreamWriter = t_fFileInfo.CreateText(); // 如果此文件不存在则创建
        }
        else
        {
            t_sStreamWriter = t_fFileInfo.CreateText(); // 如果此文件存新创建一个文件
        }

        t_sStreamWriter.WriteLine(nDate); // 以行的形式写入信息 
        t_sStreamWriter.Close(); //关闭流
        t_sStreamWriter.Dispose(); // 销毁流
    }

    public void fnDeleteFile(int index)
    {
        FileInfo t_fFileInfo =
            new FileInfo(Application.dataPath + "/Resources" + "/Data" + "/" + "存档" + index + ".Json");
        //如果存在,删除存档
        if (t_fFileInfo.Exists)
        {
            t_fFileInfo.Delete();
        }
        else
        {
            Debug.LogWarning("存档丢失");
        }
    }
}