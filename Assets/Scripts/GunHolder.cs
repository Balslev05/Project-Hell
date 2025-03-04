using UnityEngine;

public class GunHolder : MonoBehaviour
{
   public Gun gun; 
   public SpriteRenderer InteractSprite;

   void Start()
   {
    Activate();
   }
   public void Activate()
   {
      InteractSprite.enabled = false;
      this.GetComponent<SpriteRenderer>().sprite = gun.GunSprite;
      this.gameObject.name = gun.name;
   }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (InteractSprite != null)
            {
               InteractSprite.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            if (InteractSprite != null)
            {
               InteractSprite.enabled = false;
            }
        }
    }
}
