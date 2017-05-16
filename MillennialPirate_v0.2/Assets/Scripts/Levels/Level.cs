using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField]
    private GameObject[]    spawnPos;
    private GameObject      currentBG = null;
    public  Player          CurrentPlayer;
    public  GameObject[]    bgArr;
    public  GameObject      mainBG;





    private void Awake()
    {
        int levelSelected   = Game.Inst.dataManager.levelSelected;
        currentBG           = bgArr[levelSelected];
        GameObject objBG    = Instantiate(currentBG);
        objBG.transform.SetParent(mainBG.transform, false);
    }





    /// <summary>
    /// getting a random location from the array
    /// remember to assign locations for every level prefab
    /// </summary>
    public GameObject RandonSpawnPosition()
    {
        GameObject randomPos = null;

        if (spawnPos.Length > 0)
            randomPos = spawnPos[Random.Range(0, spawnPos.Length)];

        return randomPos;
    }
}
