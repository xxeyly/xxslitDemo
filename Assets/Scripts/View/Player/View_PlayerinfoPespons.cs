using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_PlayerinfoPespons : Singleton<View_PlayerinfoPespons>
{
    [SerializeField] private GameObject goPlayerSkillPanel; //技能面板
    [SerializeField] private GameObject goPlayerMissionPanel; //任务系统
    [SerializeField] private GameObject goPlayerMarketPanel; //商城系统
    [SerializeField] private GameObject goPlayerPackagePanel; //装备/背包系统
    [SerializeField] private GameObject goPlayerCharacterPanel; //角色属性
    [SerializeField] private GameObject goPlayerCampfirePanel; //烹饪面板
    [SerializeField] private GameObject goPlayerAnvilPanel; //铁匠台
    [SerializeField] private GameObject goRetraitDeploiement;

    [SerializeField] private GameObject btnPlayerSkill;
    [SerializeField] private GameObject btnPlayerMission;
    [SerializeField] private GameObject btnPlayerMarket;
    [SerializeField] private GameObject btnPlayerPackage;
    [SerializeField] private GameObject btnPlayerCharacter;
    [SerializeField] private GameObject btnCampfire; //烹饪按钮
    [SerializeField] private GameObject btnAnvil; //烹饪按钮
    [SerializeField] private bool isShow = true;
    [SerializeField] private Sprite[] _HideOrShow = new Sprite[2];

    private bool packageActivityState;

    public bool PackageActivityState
    {
        get { return packageActivityState; }

        set { packageActivityState = value; }
    }

    public bool PackageAnvilState
    {
        get { return packageAnvilState; }

        set { packageAnvilState = value; }
    }

    private bool packageAnvilState;

    public float GetPlayerSkillPanelAlpha()
    {
        return goPlayerSkillPanel.GetComponent<CanvasGroup>().alpha;
    }

    public float GetPlayerMissionPanelAlpha()
    {
        return goPlayerMissionPanel.GetComponent<CanvasGroup>().alpha;
    }

    public float GetPlayerMarketPanelAlpha()
    {
        return goPlayerMarketPanel.GetComponent<CanvasGroup>().alpha;
    }

    public float GetPlayerPackagePanelAlpha()
    {
        return goPlayerPackagePanel.GetComponent<CanvasGroup>().alpha;
    }

    public float GetPlayerCharacterPanelAlpha()
    {
        return goPlayerCharacterPanel.GetComponent<CanvasGroup>().alpha;
    }

    public void DisplaySkillPanel()
    {
        goPlayerSkillPanel.GetComponent<CanvasGroup>().alpha = 1;
        goPlayerSkillPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void HideSkillPanel()
    {
        goPlayerSkillPanel.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void DisplayMissionPanel()
    {
        goPlayerMissionPanel.GetComponent<CanvasGroup>().alpha = 1;
        goPlayerMissionPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void HideMissionPanel()
    {
        goPlayerMissionPanel.GetComponent<CanvasGroup>().alpha = 0;
        goPlayerMissionPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void DisplayMarketPanel()
    {
        goPlayerMarketPanel.GetComponent<CanvasGroup>().alpha = 1;
        goPlayerMarketPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        goPlayerMarketPanel.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void HideMarketPanel()
    {
        goPlayerMarketPanel.GetComponent<CanvasGroup>().alpha = 0;
        goPlayerMarketPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        goPlayerMarketPanel.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void DisplayPackagePanel()
    {
        goPlayerPackagePanel.GetComponent<CanvasGroup>().alpha = 1;
        goPlayerPackagePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        PackageActivityState = true;
    }

    public void HidePackagePanel()
    {
        goPlayerPackagePanel.GetComponent<CanvasGroup>().alpha = 0;
        goPlayerPackagePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
//        Ctrl_TootipManager.Instance.isToolTipShow = false;
        PackageActivityState = false;
    }

    public void DisplayCharacterPanel()
    {
        goPlayerCharacterPanel.GetComponent<CanvasGroup>().alpha = 1;
        goPlayerCharacterPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void HideCharacterPanel()
    {
        goPlayerCharacterPanel.GetComponent<CanvasGroup>().alpha = 0;
        goPlayerCharacterPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void DisplayCampfirePanel()
    {
        goPlayerCampfirePanel.GetComponent<CanvasGroup>().alpha = 1;
        goPlayerCampfirePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void HideCampfirePanel()
    {
        goPlayerCampfirePanel.GetComponent<CanvasGroup>().alpha = 0;
        goPlayerCampfirePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void DisplayAnvilPanel()
    {
        goPlayerAnvilPanel.GetComponent<CanvasGroup>().alpha = 1;
        goPlayerAnvilPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        PackageAnvilState = true;
    }

    public void HideAnvilPanel()
    {
        goPlayerAnvilPanel.GetComponent<CanvasGroup>().alpha = 0;
        goPlayerAnvilPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        PackageAnvilState = false;
    }

    private void DisyAllPanel()
    {
        btnPlayerSkill.SetActive(true);
        btnPlayerMission.SetActive(true);
        btnPlayerMarket.SetActive(true);
        btnPlayerPackage.SetActive(true);
        btnPlayerCharacter.SetActive(true);
        btnCampfire.SetActive(true);
        btnAnvil.SetActive(true);
        isShow = true;
        goRetraitDeploiement.GetComponent<Image>().sprite = _HideOrShow[1];
    }

    private void HideAllPanel()
    {
        btnPlayerSkill.SetActive(false);
        btnPlayerMission.SetActive(false);
        btnPlayerMarket.SetActive(false);
        btnPlayerPackage.SetActive(false);
        btnPlayerCharacter.SetActive(false);
        btnCampfire.SetActive(false);
        btnAnvil.SetActive(false);
        goRetraitDeploiement.GetComponent<Image>().sprite = _HideOrShow[0];

        isShow = false;
    }

    public void HideOrShow()
    {
        if (isShow)
        {
            HideAllPanel();
        }
        else
        {
            DisyAllPanel();
        }
    }
}