using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl_ShopGroupItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private Vector2 toolTipPosionOffset;
    private Vector2 position;

    private void Start()
    {
        canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //鼠标左键点击购买商品
        if (eventData.button == PointerEventData.InputButton.Left && Ctrl_TootipManager.Instance.IsPickedItem == false)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera, out position);
            position += toolTipPosionOffset;
            Ctrl_TootipManager.Instance.ShowShopTootip(gameObject.GetComponent<View_ShopGroupItem>().Item, position);
        }
    }
}