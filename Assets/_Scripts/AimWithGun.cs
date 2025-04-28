using UnityEngine;

public class AimWithGun : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer playerSprite;
    
    [SerializeField] private GameObject gunHolder;
    [SerializeField] private SpriteRenderer gunSprite;

    //private Vector3 mousePosition;
    //private Transform cachedTransform;
    //private Vector3 cachedScale;
    
    private void Start()
    {
        if (cam == null) cam = Camera.main;
        
        //if (player == null) player = transform.parent.gameObject;
        //playerSprite = player.GetComponent<SpriteRenderer>();

        //cachedTransform = transform;
        //cachedScale = cachedTransform.localScale;
    }

    private void Update()
    {
        GunLookAtMouse();
        //HandleFlipping();
        //HandleAiming();
    }

    private void GunLookAtMouse()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        gunHolder.transform.up = mousePosition - new Vector2(transform.position.x, transform.position.y);

        if (mousePosition.x < transform.position.x) { gunSprite.flipY = true; playerSprite.flipX = true; }
        else if (mousePosition.x > transform.position.x) { gunSprite.flipY = false; playerSprite.flipX = false; }
    }

    /*private void HandleFlipping()
    {
        float zRotation = cachedTransform.rotation.eulerAngles.z;
        bool shouldFlip = zRotation >= 90 && zRotation <= 270;
        
        playerSprite.flipX = shouldFlip;
        float scaleY = shouldFlip ? -Mathf.Abs(cachedScale.y) : Mathf.Abs(cachedScale.y);
        cachedTransform.localScale = new Vector3(cachedScale.x, scaleY, cachedScale.z);
    }

    private void HandleAiming()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - cachedTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        cachedTransform.rotation = Quaternion.Euler(0, 0, angle);
    }*/
}
