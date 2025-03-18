using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events; 

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Hover Settings")]
    public float hoverScale = 1.2f; 
    public float hoverDuration = 0.2f; 
    
    [Header("Click Settings")]
    public AudioSource audioSource;
    public float clickScale = 0.9f; 
    public float clickDuration = 0.1f; 
    public float clickBounceScale = 1.1f; 
    [Header("Event")]
    public UnityEvent OnClick;
    private Vector3 originalScale; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalScale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * hoverScale, hoverDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, hoverDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        Sequence clickSequence = DOTween.Sequence();
        clickSequence.Append(transform.DOScale(originalScale * clickScale, clickDuration).SetEase(Ease.OutQuad));
        clickSequence.Append(transform.DOScale(originalScale * clickBounceScale, clickDuration).SetEase(Ease.OutBounce));
        clickSequence.Append(transform.DOScale(originalScale, clickDuration).SetEase(Ease.OutBack)).onComplete =()=> OnClick.Invoke();
        clickSequence.Play();
        if (audioSource != null) 
            audioSource.Play();
    }

    public void QUIT()
    {
        Application.Quit();
    }
}
