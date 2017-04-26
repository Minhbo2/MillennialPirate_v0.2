using System;
using UnityEngine;

public class NextBackButton : MonoBehaviour {

    public static Action m_nextBackBtnFunction = null;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnScreenBackBtn();
        }
    }


    public static void SetNextBackBtnFunction(Action newFunction)
    {
        m_nextBackBtnFunction = newFunction;
    }

    public static void OnScreenBackBtn()
    {
        if (m_nextBackBtnFunction == null)
        {
            Debug.Log("Unassign Function");
            return;
        }

        m_nextBackBtnFunction();
    }
}
