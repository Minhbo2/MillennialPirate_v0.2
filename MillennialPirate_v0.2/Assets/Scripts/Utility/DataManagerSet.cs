using UnityEngine;

public class DataManagerSet : Set {

    public int levelUnlocked = 0;
    public int levelSelected = 0;

    private void Start()
    {
        DataUtility.LoadData();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Game.Inst.dataManager.levelUnlocked = 0;
            DataUtility.SaveData();
        }
    }
}
