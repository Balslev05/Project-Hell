using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public GameObject ControlsMenu;
    public Image Darken;
    void Start()
    {
        Darken.DOFade(0.6f, 0);
        ControlsMenu.transform.localScale = Vector3.zero;
    }

    void Update()
    {

    }

    public void OpenControlsMenu()
    {
        ControlsMenu.SetActive(true);
        ControlsMenu.transform.localScale = Vector3.zero;
        Darken.DOFade(0.9f, 0.5f);
        ControlsMenu.transform.DOScale(1, 0.5f).SetEase(Ease.OutCubic);
    }
    public void CloseControlsMenu()
    {
        Darken.DOFade(0.6f, 0.5f);
        ControlsMenu.transform.DOScale(0, 0.5f).SetEase(Ease.InCubic).OnComplete(() => ControlsMenu.SetActive(false));
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
