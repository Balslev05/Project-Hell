using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events; 

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
    [Header("Click Settings")]
    public AudioSource audioSource;
    public float clickScale = 0.9f; 
    public float clickDuration = 0.1f; 
    public float clickBounceScale = 1.1f; 
    [Header("Event")]
    public UnityEvent OnClick;
    private Vector3 originalScale; 
    [Header("Hover Settings")]
    public float hoverScale = 1.2f; 
    public float hoverDuration = 0.2f; 
    [Header("------------------------------------------------------------")]
    [Header("IGNORE THIS IF NOT A TAB BUTTON")]
    public SwitchTabs ThisTab = SwitchTabs.None;
    public enum SwitchTabs { Shop, Inventory, Scrapper,None };
    [Header("Shop")]
    public GameObject ShopTab;
    [Header("Inventory&Stats")]
    public GameObject InventoryTab;
    [Header("Scrapper")]
    public GameObject scapperTab;

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

    public void SwitchTab()
    {
      if (ThisTab == SwitchTabs.None) return; // Avoid switching to the same tab
      // Get the current active tab
      GameObject currentTab = GetActiveTab();
        
      // Scale down the current tab before switching
      if (currentTab != null)
      {
        currentTab.transform.DOScale(0.8f, 0.2f).SetEase(Ease.InOutQuad).OnComplete(() =>{
        // Disable all tabs after shrinking animation
        ShopTab.SetActive(false);
        InventoryTab.SetActive(false);
        scapperTab.SetActive(false);

        // Update active tab
        GameObject newActiveTab = GetActiveTab();
        newActiveTab.SetActive(true);

        // Scale up the new active tab
        newActiveTab.transform.localScale = Vector3.one * 0.8f; // Start small
        newActiveTab.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);});
      }
    }

    private GameObject GetActiveTab()
    {
        switch (ThisTab)
        {
            case SwitchTabs.Shop: return ShopTab;
            case SwitchTabs.Inventory: return InventoryTab;
            case SwitchTabs.Scrapper: return scapperTab;
            default: return null;
        }
    }
}
