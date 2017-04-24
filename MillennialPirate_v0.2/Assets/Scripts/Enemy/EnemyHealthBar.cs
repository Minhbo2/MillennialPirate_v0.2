using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {

    [SerializeField]
    private Slider      hp_Slider;
    private int         enemyMaxHealth      = 10;
    private int         enemyCurrentHealth  = 0;
    public  GameObject  inst_RangeEnemy     = null;


    private void Start()
    {
        if (hp_Slider)
            hp_Slider = hp_Slider.GetComponent<Slider>();

        enemyCurrentHealth = enemyMaxHealth;
    }


    public void EnemyTakingDamage(int damageAmount)
    {
        enemyCurrentHealth -= damageAmount;
        hp_Slider.value     = enemyCurrentHealth;

        if (enemyCurrentHealth <= 0 && inst_RangeEnemy)
        {
            Destroy(gameObject);
            Destroy(inst_RangeEnemy.gameObject);
        }
    }
}
