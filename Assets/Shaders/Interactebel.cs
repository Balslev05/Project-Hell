using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class InteractableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public InventoryContainer container;
    public Vector3 originalScale;

    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 offset;
    private Vector3 origionalScale;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalScale = transform.localScale;
        transform.DOScale(originalScale * 1.1f, 0.1f);

        // Calculate offset from mouse to object's center
        Vector3 worldPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform, eventData.position, canvas.worldCamera, out worldPoint);
        offset = rectTransform.position - worldPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPoint;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform, eventData.position, canvas.worldCamera, out worldPoint))
        {
            rectTransform.position = worldPoint + offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.DOScale(originalScale, 0.1f);

        container.ReorderItem(this);

        int index = container.InventoryList.IndexOf(this);
        if (index >= 0 && index < container.slotPositions.Count)
        {
            MoveTo(container.slotPositions[index]);
        }
    }

    public void MoveTo(Vector2 targetPos)
    {
        rectTransform.DOAnchorPos(targetPos, 0.25f).SetEase(Ease.OutExpo);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (container.SelectedGun == this.gameObject){
            this.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f).SetEase(Ease.InOutSine);
            return;
        }
        container.SelectGun(this.gameObject);
    }
    
}
