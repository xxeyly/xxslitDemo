using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class View_QuestItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject activeOverlay;
    [SerializeField] private GameObject selectOverlay;

    public GameObject SelectOverlay
    {
        get { return selectOverlay; }

        set { selectOverlay = value; }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        activeOverlay.SetActive(false);
    }

    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!SelectOverlay.activeSelf)
        {
            activeOverlay.SetActive(true);
        }
    }

    public void ShowSelectOverlay()
    {
        Ctrl_QuestItemManager.Instance.HideAllSelectOverlay();
        SelectOverlay.SetActive(true);
    }
}