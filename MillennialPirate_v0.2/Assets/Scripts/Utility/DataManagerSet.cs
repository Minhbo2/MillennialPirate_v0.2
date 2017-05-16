
public class DataManagerSet : Set {

    public int levelUnlocked = 0;
    public int levelSelected = 0;

    private void Start()
    {
        DataUtility.LoadData();
    }
}
