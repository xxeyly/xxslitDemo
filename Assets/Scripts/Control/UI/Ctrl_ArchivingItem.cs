using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ctrl_ArchivingItem : MonoBehaviour, IPointerDownHandler
{
    private int index;
    private Model_Archiving archiving;
    [SerializeField] private GameObject deleteBtn; //删除存档按钮

    public Model_Archiving Archiving
    {
        get { return archiving; }

        set
        {
            archiving = value;
            GetComponent<View_ArchivingItem>().UpdateIndex("存档" + (Index + 1)); //存档名称
            deleteBtn.SetActive(true);
        }
    }

    public int Index
    {
        get { return index; }

        set { index = value; }
    }

    /// <summary>
    /// 存档,读档有重复性,这里获得当前父对象的激活状态来判断当前是存档还是读档,读档和存档只会机会一个脚本
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //如果是存档模式
        if (Ctrl_PlayerAccessFile.Instance.IsArchiving)
        {
            //当前插槽没有任何数据
            if (Archiving == null)
            {
                Model_Archiving archiving = Ctrl_HeroProperty.Instance.GetHeroCurrentData(); //存档的属性
                Archiving = archiving; //存储存档赋值
                Ctrl_GameReading.Instance.SaveGame(Index); //存档操作
                GetComponent<View_ArchivingItem>().UpdateIndex("存档" + (Index + 1)); //存档名称
                deleteBtn.SetActive(true); //显示删档按钮
            }
            else
            {
                Ctrl_TootipManager.Instance.ShowAgreeCancel("是否覆盖当前存档");
                Ctrl_TootipManager.Instance.AgreeCancel.GetComponent<Ctrl_AgreeCancelTootip>().del_AgreeCancel +=
                    OverwriteArchive;
            }
        }
        //读档模式
        else
        {
            if (Archiving != null)
            {
                Ctrl_GameReading.Instance.LoadGame(Archiving);
            }
        }
    }

    /// <summary>
    /// 覆盖存档
    /// </summary>
    private void OverwriteArchive()
    {
        Model_Archiving archiving = Ctrl_HeroProperty.Instance.GetHeroCurrentData(); //存档的属性
        Archiving = archiving; //存储存档赋值
        Ctrl_GameReading.Instance.SaveGame(Index); //存档操作
        GetComponent<View_ArchivingItem>().UpdateIndex("存档" + (Index + 1).ToString());
    }

    /// <summary>
    /// 弹出删档弹窗
    /// </summary>
    public void OnDelete()
    {
        if (Archiving != null)
        {
            Ctrl_TootipManager.Instance.ShowAgreeCancel("是否删除当前存档");
            Ctrl_TootipManager.Instance.AgreeCancel.GetComponent<Ctrl_AgreeCancelTootip>().del_AgreeCancel +=
                DeleteArchiving;
        }
    }

    /// <summary>
    /// 删除存档
    /// </summary>
    private void DeleteArchiving()
    {
        Archiving = null;
        Ctrl_GameReading.Instance.fnDeleteFile(index);
        deleteBtn.SetActive(false); //显示删档按钮
        GetComponent<View_ArchivingItem>().UpdateIndex("空");
    }
}