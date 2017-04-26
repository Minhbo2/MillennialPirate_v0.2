
using UnityEngine;

public class LevelIntro : MonoBehaviour {


    [SerializeField]
    private Animator mAnim;
    [SerializeField]
    private GameObject levelBtn;

    private void LoadButtonOnCompleteIntro()
    {
        mAnim.SetBool("IntroIsDone", true);
        levelBtn.SetActive(true);
    }
}
