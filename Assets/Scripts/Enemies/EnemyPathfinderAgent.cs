using UnityEngine;
using Pathfinding;

public class EnemyPathfinderAgent : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistanceToTargetThreshold;
    private float distanceToTarget;

    private void Start()
    {
        path = GetComponent<AIPath>();
    }

    private void Update()
    {
        path.maxSpeed = moveSpeed;

        distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget > stopDistanceToTargetThreshold) {
            path.destination = target.position;
        }
        else {
            path.destination = transform.position;
        }
    }
}
