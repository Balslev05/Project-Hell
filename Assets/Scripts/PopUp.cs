using UnityEngine;
using TMPro;
using DG.Tweening;

public class PopUp : MonoBehaviour
{
    private TMP_Text PopUpText;
    private VertexGradient colorGradient; // Declare colorGradient

    public enum TextGradient
    {
        Red,
        Green,
        White
    }

    void Start()
    {
        this.GetComponent<RectTransform>().localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void ShowPopUp(string text, Camera cam)
    {
        this.GetComponent<PopUp>().enabled = false;

        PopUpText = this.transform.GetComponent<TMP_Text>();
        PopUpText.GetComponent<TMP_Text>().enabled = true;
        
        PopUpText.text = text;
        PopUpText.transform.localScale = Vector3.zero;

        // Scale up with bounce effect
        PopUpText.transform.DOScale(0.015f, 1)
            .SetEase(Ease.OutBack)
            .OnComplete(() => {
                PopUpText.transform.DOScale(0.01f, 0.2f).SetEase(Ease.InOutBack);
            });

        // Fade in text
        PopUpText.alpha = 0;
        PopUpText.DOFade(1, 0.3f);

        // Slight rotation wiggle
        PopUpText.transform.DORotate(new Vector3(0, 0, 10), 0.2f)
            .SetEase(Ease.InOutSine)
            .SetLoops(2, LoopType.Yoyo);
            FadeOut();
        // Fade out and scale down before destroying
    }

    public void FadeOut(){
        Sequence fadeOutSequence = DOTween.Sequence();
        fadeOutSequence.SetDelay(1f)
            .Append(PopUpText.DOFade(0, 0.3f))
            .Join(PopUpText.transform.DOScale(0, 0.3f).SetEase(Ease.InBack));

        fadeOutSequence.OnComplete(() => {
            Destroy(this.gameObject);
        });
    }

    public void SetCustomGradient(Color baseColor, float topBrightness = 1.0f, float bottomBrightness = 0.8f)
    {
        colorGradient.topLeft = baseColor * topBrightness;
        colorGradient.topRight = baseColor * topBrightness;
        colorGradient.bottomLeft = baseColor * bottomBrightness;
        colorGradient.bottomRight = baseColor * bottomBrightness;
    }

    public void SetBadSilverGradient()
    {
        Color badSilver = new Color(0.6f, 0.6f, 0.6f); // Dull silver, not too bright
        SetCustomGradient(badSilver, 0.9f, 0.7f); // Slightly darker gradient for a weaker feel
    }

    public void SetRedGradient()
    {
        Color red = new Color(1f, 0.2f, 0.2f); // Bright red for critical damage
        SetCustomGradient(red, 1.0f, 0.8f);
    }

    public void SetWhiteGradient()
    {
        Color white = new Color(1f, 1f, 1f); // Pure white for normal damage
        SetCustomGradient(white, 1.0f, 0.8f);
    }
}