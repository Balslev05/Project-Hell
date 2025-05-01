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




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
