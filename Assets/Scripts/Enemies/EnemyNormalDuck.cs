using UnityEngine;

public class EnemyNormalDuck : EnemyBase
{
    private void Start() 
    {
        base.Start();
        Debug.Log(currentHealth);
    }

    private void Update()
    {
        base.Move();
    }
}
