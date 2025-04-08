using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
    private GunManager gunManager;
    public GameObject InventoryBoxHolderPrefab;
    public int XAmount = 0;
    public int Xoffset = 0;
    public int Yoffset = 0;
    private int XCurrentOffset;
    private int YCurrentOffset;
    [SerializeField] private int StartXOffset;
    [SerializeField] private int StartYOffset;


    void Start()
    {
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
        UpdateInventory();
        XCurrentOffset = StartXOffset;
        YCurrentOffset = StartYOffset;
    }

    void Update()
    {
        
    }


    public void UpdateInventory(){
        for (int i = 0; i < gunManager.GunList.Count; i++){
            if (i < XAmount)
            {
                GameObject inventoryBox = Instantiate(InventoryBoxHolderPrefab, transform);
                //inventoryBox.transform.position = Vector3.zero;
                inventoryBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(-180, 45, 0);
                //inventoryBox.transform.localPosition = new Vector3(XCurrentOffset * i, YCurrentOffset, 0);
                inventoryBox.GetComponent<ShopCard>().SetCard(gunManager.GunList[i]);
                XCurrentOffset += Xoffset;
             } else
            {
                GameObject inventoryBox = Instantiate(InventoryBoxHolderPrefab, new Vector3(XCurrentOffset * i, YCurrentOffset, 0), Quaternion.identity, transform);
                inventoryBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(-180, 45, 0);
               /* inventoryBox.GetComponent<ShopCard>().SetCard(gunManager.GunList[i]);
                YCurrentOffset += Yoffset;
                XCurrentOffset = 0; */
            } 
        }
    }
}
