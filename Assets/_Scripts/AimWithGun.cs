using UnityEngine;

public class AimWithGun : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;
    
    private Vector3 mousePosition;
    private SpriteRenderer playerSprite;
    private Transform cachedTransform;
    private Vector3 cachedScale;
    
    private void Awake()
    {
        if (cam == null) cam = Camera.main;
        if (player == null) player = transform.parent.gameObject;
        
        playerSprite = player.GetComponent<SpriteRenderer>();
        cachedTransform = transform;
        cachedScale = cachedTransform.localScale;
    }

    private void Update()
    {
        HandleFlipping();
        HandleAiming();
    }

    private void HandleFlipping()
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
    }
}
