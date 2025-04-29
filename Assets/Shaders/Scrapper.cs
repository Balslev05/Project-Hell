using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrapper : MonoBehaviour
{
    public ScrapperUIManager scrapperUIManager;

    [Header("Scrapped Guns")]
    public List<Gun> scrappedGuns = new List<Gun>();

    [Header("Gun Pool")]
    public List<Gun> allPossibleGuns;

    [Header("Delays & FX")]
    public float scrapDuration = 2f;
    public AudioClip scrapSfx;
    public GameObject scrapVfxPrefab;

    public Action<Gun> OnScrapResult;
    public Action OnScrapStarted;
    public Action OnScrapFailed;

    private GunManager gunManager;

    void Start()
    {
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
    }

    public void AddGun(Gun gun)
    {
        scrappedGuns.Add(gun);
        scrapperUIManager?.RefreshUI();
    }

    public void Clear()
    {
        scrappedGuns.Clear();
        scrapperUIManager?.RefreshUI();
    }

    public void Scrap()
    {
        if (scrappedGuns.Count == 0 || allPossibleGuns.Count == 0)
        {
            OnScrapFailed?.Invoke();
            Debug.Log("Scrapper: Nothing to scrap.");
            return;
        }

        if (gunManager.GunList.Count != gunManager.MaxGuns)
        {
            StartCoroutine(ScrapRoutine());
        }
    }

    IEnumerator ScrapRoutine()
    {
        scrapperUIManager?.RefreshUI();
        OnScrapStarted?.Invoke();

        if (scrapSfx) AudioSource.PlayClipAtPoint(scrapSfx, transform.position);
        if (scrapVfxPrefab) Instantiate(scrapVfxPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(scrapDuration);

        Gun reward = GenerateRandomGun();
        OnScrapResult?.Invoke(reward);
        gunManager.AddGun(reward);
        Clear();
    }

    private Gun GenerateRandomGun()
    {
        Gun chosen = Instantiate(allPossibleGuns[UnityEngine.Random.Range(0, allPossibleGuns.Count)]);
        return chosen;
    }
}
