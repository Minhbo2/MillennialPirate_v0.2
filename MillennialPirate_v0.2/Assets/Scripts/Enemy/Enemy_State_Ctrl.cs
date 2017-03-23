using UnityEngine;
using System.Collections;

public enum EnemyState
{
    Enemy_Idle_Left,
    Enemy_Idle_Right,
    Enemy_Attack,
    Enemy_Hit,
    Enemy_Dead
}

public class Enemy_State_Ctrl : MonoBehaviour
{

    private GameObject melee_enemy;
	void Start ()
    {
        melee_enemy = GameObject.Find("Melee_Enemy");
	}

    EnemyState CurrentState = EnemyState.Enemy_Idle_Left;

    void Update()
    {
        switch (CurrentState)
        {
            case EnemyState.Enemy_Idle_Left:
                break;

            case EnemyState.Enemy_Idle_Right:
                break;

            case EnemyState.Enemy_Attack:
                break;

            case EnemyState.Enemy_Hit:
                break;

            case EnemyState.Enemy_Dead:
                break;
        }

    }

    void SwitchEnemyState (EnemyState new_State)
    {
        CurrentState = new_State;
    }

    public void _FaceLeft()
    {
        melee_enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        SwitchEnemyState(EnemyState.Enemy_Idle_Left);
    }

    public void _FaceRight()
    {
        melee_enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
        SwitchEnemyState(EnemyState.Enemy_Idle_Right);
    }

    public void _Attack()
    {
        SwitchEnemyState(EnemyState.Enemy_Attack);
    }

    public void _Hit()
    {
        SwitchEnemyState(EnemyState.Enemy_Hit);
    }

    public void _Dead()
    {
        SwitchEnemyState(EnemyState.Enemy_Dead);
    }
}
