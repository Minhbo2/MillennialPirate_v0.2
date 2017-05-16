using UnityEngine.UI;
using UnityEngine;

public class HUDSet : Set {

    public GameObject   HealthBarsAnchor;
    public Image        progressBar;
    public Text         progressText;
    public GameObject[] conditionScreen;


    private void Start()
    {
        NextBackButton.SetNextBackBtnFunction(PausingInLevel);
    }





    public void Pausing()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }




    public void BackToLevelSelect()
    {
        GameObject[] sceneGO = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in sceneGO)
        {
            if (obj.tag != "GameController")
                Destroy(obj);
        }

        Game.Inst.CurrentState = GameState.GAME_INITIALIZING;
        Pausing();
    }




    public void Retry()
    {
        Reset();
        PlayerHealth.currentHealth          = PlayerHealth.maxHealth;
        PlayerHealth.reset                  = false;
        Game.Inst.levelManager.currentTime  = 0;
        Pausing();
    }





    public void GameCondition(string condition)
    {
        for (var i = 0; i < conditionScreen.Length; i++)
        {
            if (conditionScreen[i].name == condition)
            {
                bool isActive = conditionScreen[i].activeInHierarchy;
                conditionScreen[i].SetActive(!isActive);
            }
        }
        Pausing();
    }



    private void Reset()
    {
        foreach (GameObject obj in Game.Inst.levelManager.enemyObj)
            Destroy(obj);

        foreach (GameObject screen in conditionScreen)
        {
            if (screen.activeInHierarchy)
                screen.SetActive(false);
        }

        Destroy(Game.Inst.levelManager.cutScene);
    }



    public void PausingInLevel()
    {
        GameCondition("Pause");
    }

    public void OnLeftPressed()
    {
        LevelManager.level.CurrentPlayer._LeftButtonDown();
    }

    public void OnLeftUp()
    {
        LevelManager.level.CurrentPlayer._LeftButtonUp();
    }

    public void OnRightPressed()
    {
        LevelManager.level.CurrentPlayer._RightButtonDown();
    }

    public void OnRightUp()
    {
        LevelManager.level.CurrentPlayer._RightButtonUp();
    }

    public void OnDodgePressed()
    {
        LevelManager.level.CurrentPlayer._Dodge();
    }

    public void OnLightAttackPressed()
    {
        LevelManager.level.CurrentPlayer._LightAttack();
    }

    public void OnHeavyAttackPressed()
    {
        LevelManager.level.CurrentPlayer._HeavyAttack();
    }
}
