
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionSet : Set {
    [SerializeField]private Button[] levelBtn;
    public static int levelCompleted = 0;

    [SerializeField]private Sprite[] sprite;


    private void Awake()
    {
        DataUtility.LoadData();
    }

    private void Start()
    {
        for (var i = 0; i < levelBtn.Length; i++)
        {
            if (i > levelCompleted)
            {
                levelBtn[i].GetComponent<Button>().enabled = false;
                levelBtn[i].GetComponent<Image>().sprite = sprite[0];
            }
            else
                levelBtn[i].GetComponent<Image>().sprite = sprite[1];
        }

        NextBackButton.SetNextBackBtnFunction(Quit);
    }




    public void LevelIndex(int levelIndex)
    {
        LevelManager.LoadLevel(levelIndex);
        Game.Inst.WantsToBeInLoadingState = true;
        CloseSet();
    }


    private void Quit()
    {
        Debug.Log("Quiting the Game!");
        Application.Quit();
    }
}
