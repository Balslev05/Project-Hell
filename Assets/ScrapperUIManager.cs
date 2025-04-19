using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScrapperUIManager : MonoBehaviour
{
    [Header("References")]
    public Scrapper scrapper;
    public Transform slotHolder;
    public GameObject slotPrefab;
    public Image resultImage;
    public TMP_Text resultText;
    public TMP_Text rollingText;

    [Header("Rarity Texts")]
    public TMP_Text commonChanceText;
    public TMP_Text rareChanceText;
    public TMP_Text epicChanceText;
    public TMP_Text legendaryChanceText;

    private void OnEnable()
    {
        scrapper = GameObject.FindGameObjectWithTag("Scrapper").GetComponent<Scrapper>();
        RefreshUI();
        scrapper.OnScrapStarted += HandleScrapStart;
        scrapper.OnScrapResult += HandleScrapResult;
    }

    private void OnDisable()
    {
        scrapper.OnScrapStarted -= HandleScrapStart;
        scrapper.OnScrapResult -= HandleScrapResult;
    }

    public void RefreshUI()
    {
        foreach (Transform child in slotHolder)
            Destroy(child.gameObject);

        foreach (Gun gun in scrapper.scrappedGuns)
        {
            GameObject slot = Instantiate(slotPrefab, slotHolder);
            Image gunImage = slot.GetComponentInChildren<Image>();
            gunImage.sprite = gun.GunSprite;
            RectTransform rectTransform = gunImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(gunImage.sprite.rect.width, gunImage.sprite.rect.height);
        }

        UpdateRarityUI();
    }

    public void OnScrapButtonClicked()
    {
        scrapper.Scrap();
    }

    void HandleScrapStart()
    {
        resultImage.gameObject.SetActive(false);
        resultText.text = "";

        rollingText.DOFade(1, 0.3f).SetEase(Ease.InOutSine);
        rollingText.text = "Rolling...";
    }

    void HandleScrapResult(Gun reward)
    {
        StartCoroutine(DisplayReward(reward));
    }

    IEnumerator DisplayReward(Gun reward)
    {
        yield return new WaitForSeconds(1.2f);

        rollingText.DOFade(0, 0.3f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(0.3f);

        resultImage.sprite = reward.GunSprite;
        resultImage.color = Color.white;
        resultImage.transform.localScale = Vector3.zero;
        resultImage.gameObject.SetActive(true);

        resultImage.transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutBack);
        resultText.text = reward.name;
        resultText.alpha = 0;
        resultText.DOFade(1, 0.5f).SetEase(Ease.InOutSine);
    }

    void UpdateRarityUI()
    {
        var chances = scrapper.CalculateRarityChances();

        AnimateText(commonChanceText, chances.GetPercent(chances.Common), "Common");
        AnimateText(rareChanceText, chances.GetPercent(chances.Rare), "Rare");
        AnimateText(epicChanceText, chances.GetPercent(chances.Epic), "Epic");
        AnimateText(legendaryChanceText, chances.GetPercent(chances.Legendary), "Legendary");
    }

    void AnimateText(TMP_Text text, float value, string label)
    {
        float rounded = Mathf.Round(value * 10f) / 10f;
        text.text = $"{label}: {rounded}%";}
}
