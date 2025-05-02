using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private void Awake() => Instance = this;

    private void OnShake(float duration, float strength)
    {
        StopCoroutine(Shaker(duration, strength));
        StartCoroutine(Shaker(duration, strength));
    }

    public IEnumerator Shaker(float duration, float strength)
    {
       // shake camera
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
        yield return new WaitForSeconds(duration);  
        transform.DOLocalMove(new Vector3(0,0,0), 0.1f);
        transform.DOLocalRotate(new Vector3(0,0,0), 0.1f);
    }

    public static void Shake(float duration, float strength) => Instance.OnShake(duration, strength);
}