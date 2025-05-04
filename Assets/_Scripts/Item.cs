using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 1)]
public class Item : ScriptableObject
{
    enum Type {passive,active, GunSpecifik};
    public string itemName;
    [TextArea(2,2)]
    public string description;
    public Sprite icon;
    public int price = 100;
    
    public List<Effect> effects = new List<Effect>();
    
}
