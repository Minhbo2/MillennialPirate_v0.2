using UnityEngine.UI;
using UnityEngine;

public class HUDSet : Set {
<<<<<<< HEAD


=======

<<<<<<< HEAD
=======
<<<<<<< HEAD


=======
>>>>>>> be533233285d8df6b6a44e06c9e593cedf862325
>>>>>>> 87a0ec749f6b9b6d7a411ed1de45fb50eddddc1e
>>>>>>> 973f77a2e56350d065316fb0b211c912f2b47475
    public static HUDSet Inst {
        get { return m_inst; }
    } static HUDSet m_inst;

    [SerializeField] public GameObject  HealthBarsAnchor;
    [SerializeField] public Image       progressBar;
    [SerializeField] public Text        progressText;
    [SerializeField] public GameObject  winScreen;
    [SerializeField] public GameObject  loseScreen;


    private void Start()
    {
        if (m_inst == null)
            m_inst = this;
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            EndGameCondition("Win");
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



    public void EndGameCondition(string condition)
    {
        if (condition == "Win")
            winScreen.SetActive(true);
        else if (condition == "Lose")
            loseScreen.SetActive(true);

        Pausing();
    }
<<<<<<< HEAD
//>>>>>>> 8d96d5c4789c647d889c9956a55bcb0f2af6094d
=======
>>>>>>> 87a0ec749f6b9b6d7a411ed1de45fb50eddddc1e
}
