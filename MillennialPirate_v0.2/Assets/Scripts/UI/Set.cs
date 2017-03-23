using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour {

    protected void CloseSet()
    {
        SetManager.CloseSet(this);
    }
}
