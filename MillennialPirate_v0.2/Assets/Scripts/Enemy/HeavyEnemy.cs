
using UnityEngine;



public class HeavyEnemy : Melee_Enemy_01
{

    private void Start()
    {
        yOffset = 0.3f;
    }

    public override void Update()
    {
        transform.position = new Vector2(transform.position.x, yOffset);

        switch (CurrentState)
        {
            case EnemyState.ENEMY_WALKING:
                if (DistanceToTarget(player.position) <= 4f)
                {
                    StopAllCoroutines();
                    CurrentState = EnemyState.ENEMY_ATTACK;
                }
                break;

            case EnemyState.ENEMY_IDLE:
                StartCoroutine(IdleDelay());
                break;

            case EnemyState.ENEMY_ATTACK:
                StartCoroutine(AttackDelay());
                break;

            case EnemyState.ENEMY_HIT:
                enemy_Anim.SetBool("isHit", true);
                break;

            case EnemyState.ENEMY_DEATH:
                enemy_Anim.SetBool("hasBeenKilled", false);
                enemy_Anim.SetBool("isHit", false);
                enemy_Anim.SetBool("isIdle", false);
                enemy_Anim.SetBool("hasBeenKilled", true);
                break;

        }

        if (health <= 0)
        {
            CurrentState = EnemyState.ENEMY_DEATH;
            //Destroy(gameObject);
        }
    }
}
