using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using JetBrains.Annotations;


public class ShopCanvas : MonoBehaviour
{
    public GameObject PlayerCanvas;
    public GameManager manager;
    public Image FadeCanvas;
    public GameObject nextRoundButton;
    [Header("Shop")]
    public List<Transform> ShopBoxes = new List<Transform>();
    public int RerollCost = 1;
    public TMP_Text RerollCostText;
    public GameObject TabObjects;
    public GameObject RerollButton;
    public GameObject BoxContainer;
    [Header("ShopAnimation")]
    private int StartBox_XPos = -175;
    private int OffSet = 150;

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
      PlayerCanvas.SetActive(false);
      TabObjects.SetActive(true);
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
      TabObjects.SetActive(false);
      PlayerCanvas.SetActive(true);
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
}
