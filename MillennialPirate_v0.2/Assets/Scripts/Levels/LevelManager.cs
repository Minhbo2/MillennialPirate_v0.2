using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Set {

    public static int levelComplete;
    public static HUDSet HudSet; 


    private void Start()
    {
        LoadLevel(levelComplete);
    }

    public static void LoadLevel(int level)
    {
        ResourceManager.Create("Prefab/Level/Level" + level);
        HudSet = SetManager.OpenSet<HUDSet>();
    }
}
