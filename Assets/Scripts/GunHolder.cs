using UnityEngine;

public class GunHolder : MonoBehaviour
{
   public Gun gun; 


   void Start()
   {
     this.GetComponent<SpriteRenderer>().sprite = gun.GunSprite;
     this.gameObject.name = gun.name;
   }
}
