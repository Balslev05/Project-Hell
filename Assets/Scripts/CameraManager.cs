using System.Drawing;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Camera camera;
    [SerializeField] private float smoothTime;
    [SerializeField] private float threshold;
    [SerializeField] private GameObject DebugPrefab;
    GameObject Debug;
    private Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        Debug = Instantiate(DebugPrefab);
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = (player.position + camera.ScreenToWorldPoint(Input.mousePosition)) / 2f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -threshold + player.position.x, threshold + player.position.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -threshold + player.position.y, threshold + player.position.y);

        Debug.transform.position = targetPosition;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    public void ZoomCamera(float zoom)
    {
        camera.orthographicSize = zoom;
    }
}