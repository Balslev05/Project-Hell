using UnityEngine;
using TMPro;
using DG.Tweening;

public class PopUp : MonoBehaviour
{
    public TMP_Text PopUpText;
    private VertexGradient colorGradient; // Declare colorGradient
    public Gradient Crit;
    public Gradient BaseGradient; // Renamed to avoid "base" keyword issue
    public Gradient NonCrit;

    void Start()
    {
        PopUpText = this.transform.GetComponent<TMP_Text>();
        this.GetComponent<RectTransform>().localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void ShowPopUp(string text, Camera cam)
    {
        PopUpText = this.transform.GetComponent<TMP_Text>();
        PopUpText.enabled = true;
        
        PopUpText.text = text;
        PopUpText.transform.localScale = Vector3.zero;

        // Scale up with bounce effect
        PopUpText.transform.DOScale(0.015f, 0.5f)
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
    }

    public void FadeOut()
    {
        Sequence fadeOutSequence = DOTween.Sequence();
        fadeOutSequence.SetDelay(1f)
            .Append(PopUpText.DOFade(0, 0.3f))
            .Join(PopUpText.transform.DOScale(0, 0.3f).SetEase(Ease.InBack));

        fadeOutSequence.OnComplete(() => {
            Destroy(this.gameObject);
        });
    }

    private void ApplyGradient(Gradient gradient)
    {
        if (PopUpText == null)
        {
            PopUpText = GetComponent<TMP_Text>();
            if (PopUpText == null)
            {
                Debug.LogError("PopUpText is STILL null! Ensure the TMP_Text component is attached.");
                return;
            }
        }
        PopUpText.enableVertexGradient = true;
        PopUpText.colorGradient = new VertexGradient(
            gradient.Evaluate(1f), // Top Left
            gradient.Evaluate(1f), // Top Right
            gradient.Evaluate(0f), // Bottom Left
            gradient.Evaluate(0f)  // Bottom Right
    );
}

    public void SetBadSilverGradient()
    {
        ApplyGradient(NonCrit);
    }

    public void SetRedGradient()
    {
        ApplyGradient(Crit);
    }

    public void SetWhiteGradient()
    {
        ApplyGradient(BaseGradient);
    }
}
