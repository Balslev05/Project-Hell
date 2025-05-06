using UnityEngine;
using DG.Tweening;
using System.Collections;
using DG.Tweening;

public class EffectAnimate : MonoBehaviour
{

    public Sprite[] frames;
    public SpriteRenderer spriteRenderer;
    public float TimeBetweenFrames = 0.1f;

    /* public void Animate()
    {
        int i = 0;
        while(i < frames.Length)
        {
            spriteRenderer.sprite = frames[i];
            i++;
            DOTween.To(() => spriteRenderer.color, x => spriteRenderer.color = x, Color.white, TimeBetweenFrames).OnComplete(() => Animate());
        }
    } */

   public IEnumerator Animate()
    {
        int i = 0;
        while(i < frames.Length)
        {
            spriteRenderer.sprite = frames[i];
            i++;
            yield return new WaitForSeconds(TimeBetweenFrames);
        }
        // scale down and fade out
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
        spriteRenderer.DOFade(0, 0.3f).SetEase(Ease.InOutSine).onComplete += () => Destroy(this.gameObject);
   }

    void Start()
    {
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
