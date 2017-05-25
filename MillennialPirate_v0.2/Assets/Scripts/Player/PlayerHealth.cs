using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    private Player playerScript;

    public Image hp;

    public static float maxHealth       = 5;
    public static float currentHealth   = 5;
    public static bool  reset           = false;

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

        if(currentHealth <= 0 && !reset)
        {
            StartCoroutine(Death());
        }
	}

    IEnumerator Death ()
    {
        reset = true;
        playerScript.playerAnim.SetBool("Idle", false);
        playerScript.playerAnim.SetBool("Dead", true);
        Player.isInvul = false;

        yield return new WaitForSeconds(3.0f);

        Game.Inst.levelManager.cutScene = ResourceManager.Create("Prefab/Misc/LoseCutScene");
        GameObject loseCS = Game.Inst.levelManager.cutScene;
        Renderer r = loseCS.GetComponent<Renderer>();
        MovieTexture movie = (MovieTexture)r.material.mainTexture;
        if (loseCS && r)
        {
            movie.Play();
            movie.loop = true;
            Game.Inst.hud.GameCondition("Lose");
        }
    }
}
