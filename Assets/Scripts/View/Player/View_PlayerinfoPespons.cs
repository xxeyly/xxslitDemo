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
    [SerializeField] private GameObject goRetraitDeploiement;

    [SerializeField] GameObject btnPlayerSkill;
    [SerializeField] private GameObject btnPlayerMission;
    [SerializeField] private GameObject btnPlayerMarket;
    [SerializeField] private GameObject btnPlayerPackage;
    [SerializeField] private GameObject btnPlayerCharacter;
    [SerializeField] private bool isShow = true;
    [SerializeField] private Sprite[] _HideOrShow = new Sprite[2];

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
    }

    public void HideMarketPanel()
    {
        goPlayerMarketPanel.GetComponent<CanvasGroup>().alpha = 0;
        goPlayerMarketPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void DisplayPackagePanel()
    {
        goPlayerPackagePanel.GetComponent<CanvasGroup>().alpha = 1;
        goPlayerPackagePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void HidePackagePanel()
    {
        goPlayerPackagePanel.GetComponent<CanvasGroup>().alpha = 0;
        goPlayerPackagePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        Ctrl_InventoryManager.Instance.isToolTipShow = false;
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

    private void DisyAllPanel()
    {
        btnPlayerSkill.SetActive(true);
        btnPlayerMission.SetActive(true);
        btnPlayerMarket.SetActive(true);
        btnPlayerPackage.SetActive(true);
        btnPlayerCharacter.SetActive(true);
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

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}