using UnityEngine;

public class EnemyBuffDuck : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        Debug.Log(currentHealth);
    }

    private void Update()
    {
        base.Move();
    }
}
