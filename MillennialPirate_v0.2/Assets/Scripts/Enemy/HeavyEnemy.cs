
using UnityEngine;



public class HeavyEnemy : Melee_Enemy_01
{

    private void Start()
    {
        yOffset = 0.3f;
        Init();
        enemyHealth.enemyMaxHealth = 5;
    }

    public override void Update()
    {
        Vector2 hpPos = Game.Inst.UICamera.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 2.5f));
        Debug.Log(CurrentState);
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
                enemy_Anim.SetBool("isIdle", true);
                StartCoroutine(IdleDelay());
                break;

            case EnemyState.ENEMY_ATTACK:
                enemy_Anim.SetBool("attackTrigger", true);
                StartCoroutine(AttackDelay());
                break;

            case EnemyState.ENEMY_HIT:
                StopAllCoroutines();
                enemy_Anim.SetBool("isHit", true);
                break;

            case EnemyState.ENEMY_DEATH:
                StopAllCoroutines();
                enemy_Anim.SetBool("hasBeenKilled", true);
                break;

        }

        if (enemyHealth.enemyCurrentHealth <= 0)
        {
            ChangeState(EnemyState.ENEMY_DEATH);
            Destroy(enemyHealthBar);
        }
        else
            enemyHealthBar.transform.position = hpPos;
    }



    public void HeavyCheckState(float distance)
    {
        CheckState(distance);
    }

}
