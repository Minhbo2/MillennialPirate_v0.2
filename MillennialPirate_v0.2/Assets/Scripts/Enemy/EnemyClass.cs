using UnityEngine;
using System.Collections;


public class EnemyClass : MonoBehaviour
/*{
    public enum EnemyState
    {
        ENEMY_ATTACK,
        ENEMY_WALKING,
        ENEMY_DEATH,
        ENEMY_HIT,
        ENEMY_IDLE
    }
    EnemyState CurrentState = EnemyState.ENEMY_WALKING;

    public static int m_health = 0;
    public static float m_speed = 0.0f;

    private Animator enemy_Anim;
    private Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy_Anim = GetComponent<Animator>();
    }








    protected float DistanceToTarget(Vector2 target)
    {
        float distanceToTarget = Vector2.Distance(target, transform.position);
        return distanceToTarget;
    }






    protected IEnumerator MoveTowardTarget(Vector2 target)
    {
        while (DistanceToTarget(target) >= 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, m_speed * Time.deltaTime);
            yield return DistanceToTarget(target);
        }
    }





    protected void Flip()
    {
        if (player)
        {
            float playerXPos = player.position.x;
            float transformXPos = transform.position.x;
            float yRot = transform.eulerAngles.y;

            if (transformXPos < playerXPos)
            {
                if (yRot == 0)
                {
                    yRot = 180;
                    transform.eulerAngles = new Vector2(0, yRot);
                }
            }
        }
    }






    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAttackBox")
        {
            CurrentState = EnemyState.ENEMY_HIT;
        }

        //if (other.gameObject.tag == "Player")
        //{
        //    inRange = true;
        //    CurrentState = EnemyState.ENEMY_ATTACK;
        //}
    }
}


