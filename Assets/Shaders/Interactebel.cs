using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InteractableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public Vector2 originalScale;
    public InventoryContainer container;

    [Header("Animation Settings")]
    public float returnDuration = 0.25f;
    public Ease returnEase = Ease.OutBack;

    [Header("Drag Settings")]
    public float Xoffset = 5f;
    public float Yoffset = 5f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvas == null)
        {
            Debug.LogError("InteractableUI: Must be a child of a Canvas.");
        }

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        container.SelectGun(this.gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform.SetAsLastSibling();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out var pointerOffset
        );

        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;

        DOTween.Kill(rectTransform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out var localPoint
        );

        rectTransform.anchoredPosition = localPoint + new Vector2(Xoffset, Yoffset);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        container.ReorderItem(this.GetComponent<InteractableUI>());
    }

    public void MoveTo(Vector2 newPosition, float duration = 0.25f, Ease easeType = Ease.OutQuad)
    {
        rectTransform.DOAnchorPos(newPosition, duration).SetEase(easeType);
    }
}
