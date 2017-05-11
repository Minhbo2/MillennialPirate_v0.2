using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    private Player playerScript;

    public Image hp;

    public float maxHealth = 5;
    public static float currentHealth = 5;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.Find("GameController").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        float health = currentHealth / maxHealth;
        hp.fillAmount = health;

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

        GameObject winTexture = ResourceManager.Create("Prefab/Misc/LoseCutScene");
        Renderer r = winTexture.GetComponent<Renderer>();
        MovieTexture movie = (MovieTexture)r.material.mainTexture;
        if (winTexture && r)
        {
            movie.Play();
            movie.loop = true;
            HUDSet.Inst.GameCondition("Lose");
        }

        maxHealth = 3;
        currentHealth = 3;

    }
}
