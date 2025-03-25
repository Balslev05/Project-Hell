using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using JetBrains.Annotations;


public class ShopCanvas : MonoBehaviour
{
    SwitchTabs ThisTab = SwitchTabs.Shop;
    public GameManager manager;
    public Image FadeCanvas;
    public GameObject nextRoundButton;
    [Header("Shop")]
    public int RerollCost = 1;
    public GameObject RerollButton;
    public TMP_Text RerollCostText;
    public GameObject BoxContainer;
    public List<Transform> ShopBoxes = new List<Transform>();
    [Header("ShopAnimation")]
    private int StartBox_XPos = -175;
    private int OffSet = 150;
    [Header("Shop")]
    public GameObject ShopTab;
    [Header("Inventory&Stats")]
    public GameObject InventoryTab;
    [Header("Scrapper")]
    public GameObject scapperTab;
    public enum SwitchTabs { Shop, Inventory, Scrapper };

    void Start()
    {
      RerollCostText.text = "Reroll:" + RerollCost.ToString();
      RerollButton.SetActive(false);
      nextRoundButton.SetActive(false);
      foreach (Transform child in BoxContainer.transform)
      {
        ShopBoxes.Add(child.transform);
        child.GetComponent<RectTransform>().anchoredPosition = new Vector2(-3000, 0.5f);
      }
      StartCoroutine(ShowShop());
    }
    
    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        StartCoroutine(HideShop());
      }
    }
    public void UpdateChildrenCount()
    {
        ShopBoxes.Clear();
        foreach (Transform child in BoxContainer.transform)
        {     
          ShopBoxes.Add(child.transform);
        }
    }

    public IEnumerator ShowShop()
    {
      FadeCanvas.DOFade(0, 0).SetEase(Ease.OutExpo);
      yield return new WaitForSeconds(0.25f);
      FadeCanvas.DOFade(1, 0.5f).SetEase(Ease.OutExpo);
      yield return new WaitForSeconds(0.5f);
      StartCoroutine(AnimateShopBox());
    }
    public IEnumerator AnimateShopBox()
    {
      int i = 0;
      foreach (Transform child in ShopBoxes)
      {
        child.gameObject.SetActive(true);
        child.GetComponent<RectTransform>().anchoredPosition = new Vector2(-3000, 0.5f);
        child.GetComponent<ShopCard>().SetCard(manager.GetRandomGun());
        child.GetComponent<RectTransform>().DOAnchorPosX(StartBox_XPos + (OffSet * i), 0.5f).SetEase(Ease.OutExpo);
        i++;
        yield return new WaitForSeconds(0.1f);
      }
      RerollButton.SetActive(true);
      nextRoundButton.SetActive(false);
    }
    
    public IEnumerator HideShop()
    {
      RerollButton.SetActive(false);
      nextRoundButton.SetActive(false);    
      StartCoroutine(CloseShop());
      yield return new WaitForSeconds(1f);
      FadeCanvas.DOFade(0, 0.5f).SetEase(Ease.OutExpo);
    }

    public IEnumerator CloseShop()
    {
      UpdateChildrenCount();
      foreach (Transform child in ShopBoxes)
      {
        child.GetComponent<RectTransform>().DOAnchorPosX(-550, 0.5f).SetEase(Ease.InQuint).onComplete = ()=> child.gameObject.SetActive(false);        
        yield return new WaitForSeconds(0.1f);
      }
    }

    public void Reroll()
    {
      foreach (Transform child in ShopBoxes)
      {
        child.gameObject.SetActive(true);
        child.GetComponent<ShopCard>().SetCard(manager.GetRandomGun());
      }
    }


    public void SwitchTab(SwitchTabs newTab)
    {
      if (ThisTab == newTab) return; // Avoid switching to the same tab
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
        ThisTab = newTab;
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
