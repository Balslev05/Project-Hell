using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopCanvas : MonoBehaviour
{
    public Image FadeCanvas;
    public GameObject nextRoundButton;
    [Header("Shop")]
    public int RerollCost = 1;
    public TMP_Text RerollCostText;
    public GameObject BoxContainer;
    public GameObject ShopBoxPrefab;
    [Header("Inventory")]
    public GameObject xxx;
    [Header("Stats")]
    public GameObject xx;

    void Start()
    {

      ShowShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowShop()
    {
        BoxContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(-3000, 0.5f);
        BoxContainer.GetComponent<RectTransform>().DOAnchorPosX(-21, 0.5f).SetEase(Ease.OutExpo);
    }

}
