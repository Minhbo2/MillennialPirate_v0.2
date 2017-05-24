using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataUtility : MonoBehaviour {

    // Use this for initialization
    public static void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/game.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open((Application.persistentDataPath + "/game.dat"), FileMode.Open);
            Data loadedData = (Data)bf.Deserialize(file);
            file.Close();

            Game.Inst.dataManager.levelUnlocked = loadedData.levelCompleted;
        }
        else
            Debug.Log("File does not exist");
    }

    public static void SaveData()
    {
        Data newData = new Data();
        newData.levelCompleted  = Game.Inst.dataManager.levelUnlocked;

        BinaryFormatter bf      = new BinaryFormatter();
        FileStream file         = File.Create(Application.persistentDataPath + "/game.dat");
        bf.Serialize(file, newData);
        file.Close();
    }

}
