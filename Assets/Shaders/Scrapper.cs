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
    [System.Serializable]
    public struct RarityChances
    {
        [Range(0, 100)] public float Common;
        [Range(0, 100)] public float Rare;
        [Range(0, 100)] public float Epic;
        [Range(0, 100)] public float Legendary;

        public float Total => Common + Rare + Epic + Legendary;

        public float GetPercent(float value)
        {
            return Total == 0 ? 0 : (value / Total) * 100f;
        }
    }

    [Header("Debug Rarity Chances (Read Only)")]
    public RarityChances currentChances;

    public void AddGun(Gun gun)
    {
        scrappedGuns.Add(gun);
        CalculateRarityChances();
        scrapperUIManager?.RefreshUI();
    }

    public void Clear()
    {
        scrappedGuns.Clear();
        CalculateRarityChances();
        scrapperUIManager?.RefreshUI();
    }

    public void Scrap()
    {
        if (scrappedGuns.Count == 0 && allPossibleGuns.Count == 0)
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

        Gun reward = GenerateReward();
        OnScrapResult?.Invoke(reward);
        gunManager.AddGun(reward);
        Clear();
    }

    private Gun GenerateReward()
    {
        RarityChances chances = CalculateRarityChances();
        float roll = UnityEngine.Random.Range(0f, chances.Total);

        Gun.GunRarity rolledRarity = Gun.GunRarity.Common;

        if (roll <= chances.Legendary) rolledRarity = Gun.GunRarity.Legendary;
        else if (roll <= chances.Legendary + chances.Epic) rolledRarity = Gun.GunRarity.Epic;
        else if (roll <= chances.Legendary + chances.Epic + chances.Rare) rolledRarity = Gun.GunRarity.Rare;

        List<Gun> possible = allPossibleGuns.FindAll(g => g.rarity == rolledRarity);
        if (possible.Count == 0) possible = allPossibleGuns;

        Gun chosen = Instantiate(possible[UnityEngine.Random.Range(0, possible.Count)]);
        ApplyRandomBuffs(chosen, rolledRarity);

        return chosen;
    }

    void ApplyRandomBuffs(Gun gun, Gun.GunRarity rarity)
    {
        float dmgMult = 1f + UnityEngine.Random.Range(0f, 0.05f * ((int)rarity + 1));
        float critMult = 1f + UnityEngine.Random.Range(0f, 0.02f * ((int)rarity + 1));

        gun.damage = Mathf.RoundToInt(gun.damage * dmgMult);
        gun.BaseDamage = Mathf.RoundToInt(gun.BaseDamage * dmgMult);
        gun.criticalchange = Mathf.Clamp(gun.criticalchange + Mathf.RoundToInt(gun.criticalchange * critMult), 0, 100);

        gun.name += $" [{rarity}]";
    }

    public RarityChances CalculateRarityChances()
    {
        int itemCount = scrappedGuns.Count;

        currentChances.Common = Mathf.Clamp(60 - (itemCount * 5f), 5f, 100f);
        currentChances.Rare = Mathf.Clamp(25 + (itemCount * 2.5f), 0f, 100f);
        currentChances.Epic = Mathf.Clamp(10 + (itemCount * 1.5f), 0f, 100f);
        currentChances.Legendary = Mathf.Clamp(5 + (itemCount * 1f), 0f, 100f);

        return currentChances;
    }
}
