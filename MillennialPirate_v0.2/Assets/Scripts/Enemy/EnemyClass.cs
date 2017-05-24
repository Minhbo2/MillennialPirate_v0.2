using UnityEngine;
using System.Collections;


public class EnemyClass : MonoBehaviour
{
  public enum EnemyState
    {
        ENEMY_ATTACK,
        ENEMY_WALKING,
        ENEMY_DEATH,
        ENEMY_HIT,
        ENEMY_IDLE
    }
    public EnemyState CurrentState = EnemyState.ENEMY_WALKING;

    [SerializeField]
    protected float         m_speed         = 0.0f;

    [SerializeField]
    protected   Animator      enemy_Anim;
    protected   Transform     player          = null;
    public      GameObject    enemyHealthBar  = null;

    public float health;

    private void Awake()
    {
        player      = GameObject.Find("Player").transform;
        enemy_Anim  = GetComponent<Animator>();
    }





    public virtual void Update()
    {

    }





    protected float DistanceToTarget(Vector2 target)
    {
        float distanceToTarget = Vector2.Distance(target, transform.position);
        return distanceToTarget;
    }






    protected IEnumerator MoveTowardTarget(Vector2 target)
    {
        while (DistanceToTarget(target) >= 1.0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, m_speed * Time.deltaTime);
            yield return DistanceToTarget(target);
        }
    }





    protected void ChangingDirection()
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




    protected void EnemyHealthBar()
    {
        enemyHealthBar = ResourceManager.Create("Prefab/Enemy/EnemyHealthBar");
        enemyHealthBar.transform.SetParent(Game.Inst.hud.HealthBarsAnchor.transform, false);
        Game.Inst.hud.enemyHealthBar.Add(enemyHealthBar);
    }




    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAttackBox")
        {
            CurrentState = EnemyState.ENEMY_HIT;
        }
    }
}


