using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class TransestionManager : MonoBehaviour
{
    [Header("Transestion")]
    [SerializeField] private GameObject transestion;
    [SerializeField] private float time;
    [SerializeField] private int Scale = 30;
    [SerializeField] private bool isStart = false;


    public void Transestion(int SceneIndex)
    {
        StartCoroutine(FadeOutTransestion(SceneIndex));
    }

    IEnumerator FadeOutTransestion(int SceneIndex)
    {
        transestion.transform.DOScale(Scale, time);
        yield return new WaitForSeconds(time);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneIndex);
    }

    public IEnumerator FadeInTransestion()
    {
        transestion.transform.DOScale(Vector3.zero, time).SetEase(Ease.InBack);
        yield return new WaitForSeconds(time);
    }




    void Start()
    {
        if (isStart)
        {
            StartCoroutine(FadeInTransestion());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
