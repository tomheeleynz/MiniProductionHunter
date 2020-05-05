using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAnimControl : MonoBehaviour
{
    public void PauseAnimFullDraw()
    {
        GetComponent<Animator>().enabled = false;
    }

    public void PauseAnimNoDraw()
    {
        GetComponent<Animator>().enabled = false;
    }
}
