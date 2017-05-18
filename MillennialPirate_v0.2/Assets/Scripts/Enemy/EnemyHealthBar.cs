using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {

    [SerializeField]
    private Image       hp_Slider;
    public float       enemyMaxHealth;
    public float       enemyCurrentHealth;


    private void Start()
    {
        if (!hp_Slider)
            hp_Slider = hp_Slider.GetComponent<Image>();

        enemyCurrentHealth = enemyMaxHealth;
    }


    private void Update()
    {
        float healthPercentage  = enemyCurrentHealth / enemyMaxHealth;
        hp_Slider.fillAmount    = healthPercentage;
    }


    public void EnemyTakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
    }
}
