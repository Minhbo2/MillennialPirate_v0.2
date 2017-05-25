using System.Collections;
using UnityEngine;

public class RangeEnemy : EnemyClass {

    [SerializeField]
    private Transform       arrowAnchor;
    [SerializeField]
    private GameObject      arrowPrefab;

    [SerializeField] private float startDelayTime;
    
    private float startDelayTimer = 0;

    [SerializeField] private float attackDelayTime;

    private float attackDelayTimer = 0;

    //public  float           healthbarYOffset = 5.0f;
    //private GameObject      healthBarGO;



    private void Start()
    {
        health = 1;
        m_speed     = 1.5f;

        if (!enemy_Anim)
            enemy_Anim = transform.GetComponent<Animator>();

        CurrentState    = EnemyState.ENEMY_IDLE;
        player          = GameObject.Find("Player").GetComponent<Transform>();
        ChangingDirection();
    }





    public override void Update()
    {
        switch (CurrentState)
        {
            case EnemyState.ENEMY_IDLE:
                startDelayTimer += Time.deltaTime;

                if (startDelayTimer > startDelayTime)
                    ChangeCurrentState(EnemyState.ENEMY_WALKING);

                break;

            case EnemyState.ENEMY_WALKING:
                attackDelayTimer += Time.deltaTime;

                if (attackDelayTimer > attackDelayTime)
                {
                    ChangeCurrentState(EnemyState.ENEMY_ATTACK);
                    attackDelayTimer = 0;
                }
                break;
        }

        if(health <= 0)
        {
            ChangeCurrentState(EnemyState.ENEMY_DEATH);
        }
    }




    public void OnAttackCompleteAnimEvent()
    {
        ChangeCurrentState(EnemyState.ENEMY_WALKING);
    }




    public void ChangeCurrentState(EnemyState newState)
    {
        CurrentState = newState;

        enemy_Anim.SetBool("Idle", false);
        enemy_Anim.SetBool("Attack", false);
        enemy_Anim.SetBool("Walk", false);
        enemy_Anim.SetBool("Die", false);
        enemy_Anim.SetBool("GetHit", false);

        StopAllCoroutines();

        if (newState == EnemyState.ENEMY_IDLE)
            enemy_Anim.SetBool("Idle", true);
        else if (newState == EnemyState.ENEMY_WALKING)
        {
            enemy_Anim.SetBool("Walk", true);
            StartCoroutine("MoveTowardTarget", SetTarget());
        }
        else if (newState == EnemyState.ENEMY_ATTACK)
            enemy_Anim.SetBool("Attack", true);
        else if(newState == EnemyState.ENEMY_DEATH)
            enemy_Anim.SetBool("Die", true);
        else if (newState == EnemyState.ENEMY_HIT)
            enemy_Anim.SetBool("GetHit", true);
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }






    private Vector2 SetTarget()
    {
        int moveDistance;
        float xPos = player.position.x;

        if (xPos > transform.position.x)
            moveDistance = 3;
        else
            moveDistance = -3;

        Vector2 target = new Vector2(transform.position.x + moveDistance, transform.position.y);
        return target;
    }





    public void SpawnArrow()
    {
        arrowPrefab = ResourceManager.Create("Prefab/Misc/Arrow");
        arrowPrefab.transform.position = arrowAnchor.position;
    } 
}
