using UnityEngine.UI;
using UnityEngine;

public class HUDSet : Set {

    public static HUDSet Inst {
        get { return m_inst; }
    } static HUDSet m_inst;

    public GameObject   HealthBarsAnchor;
    public Image        progressBar;
    public Text         progressText;
    public GameObject   winScreen;
    public GameObject   loseScreen;
    public GameObject   pauseScreen;


    private void Start()
    {
        if (m_inst == null)
            m_inst = this;

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
        LevelManager.LoadLevel(LevelManager.levelIndex);
    }





    public void GameCondition(string condition)
    {
        switch (condition)
        {
            case "Win":
                winScreen.SetActive(true);
                break;
            case "Lose":
                loseScreen.SetActive(true);
                break;
            case "Pause":
                bool isActive = pauseScreen.activeInHierarchy;
                pauseScreen.SetActive(!isActive);
                break;
        }
        Pausing();
    }



    public void PausingInLevel()
    {
        GameCondition("Pause");
    }
}
