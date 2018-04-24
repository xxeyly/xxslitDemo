using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_PickUp : MonoBehaviour
{
    [SerializeField] private Image _ImgIcon;
    [SerializeField] private Text _Amount;
    private Model_Item item;
    private Model_Skill skill;

    public void InitUI()
    {
        item = GetComponent<Ctrl_PickUp>().Item;
        skill = GetComponent<Ctrl_PickUp>().Skill;
        if (item != null)
        {
            ShowIcon();
            _ImgIcon.sprite = Resources.Load<Sprite>(item.sprite);
            _Amount.text = item.currentNumber.ToString();
        }
        else if (skill != null)
        {
            ShowIcon();
            _ImgIcon.sprite = Resources.Load<Sprite>(skill.skillSprite);
            _Amount.text = skill.skillCurrentLv.ToString();
        }

        if (item == null && skill == null)
        {
            HideIcon();
        }
    }

    /// <summary>
    /// 显示物品
    /// </summary>
    public void ShowIcon()
    {
        _ImgIcon.gameObject.SetActive(true);
        _Amount.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏物品
    /// </summary>
    public void HideIcon()
    {
        _ImgIcon.gameObject.SetActive(false);
        _Amount.gameObject.SetActive(false);
        Ctrl_TootipManager.Instance.HidePickUp();
    }

    public void UpdateAmount()
    {
        _Amount.text = item.currentNumber.ToString();
    }

    public void ChangeImg(Sprite sprite)
    {
        _ImgIcon.sprite = sprite;
    }

    public void ChangeAmount(int amount)
    {
        _Amount.text = amount.ToString();
    }

    public void ChangeItem(Model_Item item)
    {
        ChangeImg(Resources.Load<Sprite>(item.sprite));
        ChangeAmount(item.currentNumber);
        this.item = item;
    }

    public void ChangeSkill(Model_Skill skill)
    {
        if (skill == null)
        {
            _ImgIcon.gameObject.SetActive(false);
        }
        else
        {
            ChangeImg(Resources.Load<Sprite>(skill.skillSprite));
            ChangeAmount(skill.skillCurrentLv);
        }
    }

    public void OnSetPickRaycast(bool isRaycast)
    {
        _ImgIcon.raycastTarget = isRaycast;
    }
}