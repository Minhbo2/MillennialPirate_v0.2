using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {

    [SerializeField]
    private Image       hp_Slider;
    private float       enemyMaxHealth      = 10;
    private float       enemyCurrentHealth  = 0;


    private void Start()
    {
        if (hp_Slider)
            hp_Slider = hp_Slider.GetComponent<Image>();

        enemyCurrentHealth = enemyMaxHealth;
    }


    public void EnemyTakingDamage(int damageAmount)
    {
        enemyCurrentHealth -= damageAmount;
        float healthPercentage = enemyCurrentHealth / enemyMaxHealth;
        hp_Slider.fillAmount = healthPercentage;

        if (enemyCurrentHealth <= 0)
            Destroy(gameObject);
    }
}
