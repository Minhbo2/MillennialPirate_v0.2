using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    private Player playerScript;

    public Slider hp;

    public int maxHealth = 5;
    public static int currentHealth = 5;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.Find("GameController").GetComponent<Player>();

        hp = GameObject.Find("HP_2").GetComponent<Slider>();

        hp.maxValue = maxHealth;
        hp.value = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        hp.value = currentHealth;

        if(Input.GetKeyDown(KeyCode.Y))
        {
            currentHealth--;
        }

        if(currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
	}

    IEnumerator Death ()
    {
        playerScript.playerAnim.SetBool("Idle", false);
        playerScript.playerAnim.SetBool("Dead", true);

        yield return new WaitForSeconds(3.0f);

        HUDSet.Inst.EndGameCondition("Lose");

        maxHealth = 3;
        currentHealth = 3;

    }
}
