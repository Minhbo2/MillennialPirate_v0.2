
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionSet : Set {
    [SerializeField]private Button[] levelBtn;

    [SerializeField]private Sprite[] sprite;

    public AudioClip levelSelectSound;
    public AudioClip startSound;

    private AudioSource audioSource;
    private void Start()
    {
        for (var i = 0; i < levelBtn.Length; i++)
        {
            if (i > Game.Inst.dataManager.levelUnlocked)
            {
                levelBtn[i].GetComponent<Button>().enabled = false;
                levelBtn[i].GetComponent<Image>().sprite = sprite[0];
            }
            else
                levelBtn[i].GetComponent<Image>().sprite = sprite[1];
        }

        NextBackButton.SetNextBackBtnFunction(Quit);
        audioSource = transform.GetComponent<AudioSource>();
        audioSource.clip = startSound;
        audioSource.PlayOneShot(startSound);

    }



    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = levelSelectSound;
            audioSource.PlayOneShot(levelSelectSound);
        }
    }




    public void LevelIndex(int levelIndex)
    {
        Game.Inst.dataManager.levelSelected = levelIndex;
        Game.Inst.WantsToBeInLoadingState   = true;
        CloseSet();
    }


    public void Quit()
    {
        Debug.Log("Quiting the Game!");
        Application.Quit();
    }
}
