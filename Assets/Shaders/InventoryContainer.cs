using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryContainer : MonoBehaviour
{
    [SerializeField] private GunManager gunManager;
    public GameObject InventoryBoxHolderPrefab;

    [Header("Grid Settings")]
    public int XAmount = 4;
    public int Xoffset = 150;
    public int Yoffset = -150;
    public Vector2 StartOffset = new Vector2(0, 0);

    [Header("Inventory List")]
    public List<InteractableUI> InventoryList = new List<InteractableUI>();

    [Header("Selection")]
    public GameObject SelectedGun;

    private List<Vector2> slotPositions = new List<Vector2>();

    void Start()
    {
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
        UpdateInventory();
    }

    void OnEnable()
    {
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        InventoryList.Clear();
        slotPositions.Clear();

        int currentX = 0;
        int currentY = 0;

        if (gunManager.GunList.Count == 0) return;

        for (int i = 0; i < gunManager.GunList.Count; i++)
        {
            float xPos = StartOffset.x + (currentX * Xoffset);
            float yPos = StartOffset.y + (currentY * Yoffset);
            Vector2 itemPosition = new Vector2(xPos, yPos);

            GameObject inventoryBox = Instantiate(InventoryBoxHolderPrefab, transform);
            InteractableUI interactable = inventoryBox.GetComponent<InteractableUI>();
            interactable.container = this; // ✅ assign reference
            InventoryList.Add(interactable);
            slotPositions.Add(itemPosition);

            RectTransform rt = inventoryBox.GetComponent<RectTransform>();
            rt.anchoredPosition = itemPosition;

            inventoryBox.GetComponent<ShopCard>().SetCard(gunManager.GunList[i]);

            currentX++;
            if (currentX >= XAmount)
            {
                currentX = 0;
                currentY++;
            }
        }

        UpdateGunManager();
    }

    public void SelectGun(GameObject selected)
    {
        if (SelectedGun == selected) return;

        // Deselect previous
        if (SelectedGun != null)
        {
            SelectedGun.transform.DOScale(SelectedGun.GetComponent<InteractableUI>().originalScale, 0.2f).SetEase(Ease.InOutSine);
        }

        // Select new
        SelectedGun = selected;

        if (SelectedGun != null)
        {
            var ui = SelectedGun.GetComponent<InteractableUI>();
            ui.originalScale = SelectedGun.transform.localScale;
            SelectedGun.transform.DOScale(ui.originalScale * 1.2f, 0.25f).SetEase(Ease.OutBack);
        }
    }

    public void ReorderItem(InteractableUI droppedItem)
    {
        Vector2 dropPos = droppedItem.GetComponent<RectTransform>().anchoredPosition;

        int closestIndex = 0;
        float closestDist = float.MaxValue;

        for (int i = 0; i < slotPositions.Count; i++)
        {
            float dist = Vector2.Distance(dropPos, slotPositions[i]);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestIndex = i;
            }
        }

        int currentIndex = InventoryList.IndexOf(droppedItem);
        if (currentIndex == closestIndex) return;

        InventoryList.RemoveAt(currentIndex);
        InventoryList.Insert(closestIndex, droppedItem);

        for (int i = 0; i < InventoryList.Count; i++)
        {
            InventoryList[i].MoveTo(slotPositions[i]);
        }

        UpdateGunManager();
    }

    public void UpdateGunManager()
    {
        gunManager.GunList.Clear();

        foreach (var item in InventoryList)
        {
            if (item != null)
            {
                ShopCard card = item.GetComponent<ShopCard>();
                if (card != null && card.current_Gun != null)
                {
                    gunManager.GunList.Add(card.current_Gun);
                }
            }
        }
    }

    public void SellSelectedGun()
    {
        if (SelectedGun != null)
        {
            ShopCard card = SelectedGun.GetComponentInChildren<ShopCard>();
            if (card != null && card.current_Gun != null)
            {
                gunManager.SellGun(card.current_Gun);
                Debug.Log("Sold: " + card.current_Gun.name);

                InventoryList.Remove(SelectedGun.GetComponent<InteractableUI>());
                Destroy(SelectedGun);
                SelectedGun = null;

                UpdateGunManager();
            }
        }
    }

    public void GiveToScrapper()
    {
        if (SelectedGun != null)
        {
            ShopCard card = SelectedGun.GetComponentInChildren<ShopCard>();
            Scrapper scrapper = GameObject.FindGameObjectWithTag("Scrapper").GetComponent<Scrapper>();

            if (card != null && card.current_Gun != null && scrapper != null)
            {
                scrapper.AddGun(card.current_Gun);
                Debug.Log("Given to scrapper: " + card.current_Gun.name);

                InventoryList.Remove(SelectedGun.GetComponent<InteractableUI>());
                Destroy(SelectedGun);
                SelectedGun = null;

                UpdateGunManager();
            }
        }
    }
}
