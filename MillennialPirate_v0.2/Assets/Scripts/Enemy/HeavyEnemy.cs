using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState2
{
    ENEMY_ATTACK,
    ENEMY_WALKING,
    ENEMY_DEATH,
    ENEMY_HIT,
    ENEMY_IDLE
}
public class HeavyEnemy : Melee_Enemy_01
{

    private float yOffset = 0.3f;

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

                break;

            case EnemyState.ENEMY_DEATH:
                enemy_Anim.SetBool("hasBeenKilled", true);
                break;

        }

        if (m_health <= 0)
        {
            CurrentState = EnemyState.ENEMY_DEATH;
            Destroy(gameObject);
        }
    }
}
