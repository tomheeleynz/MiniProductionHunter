using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool hoverOver = false;
    public bool bActive = false;
    private bool canHighlight = false;
    Material[] matList;
    private void Start()
    {
        matList = GetComponent<MeshRenderer>().materials;
        canHighlight = matList.Length > 1;

        HighLight(false);
    }

    public void HighLight(bool isHighLighted)
    {
        if (canHighlight)
        {
            if (isHighLighted)
            {
                GetComponent<MeshRenderer>().materials = matList;
            }
            else
            {
                Material[] newMatList = new Material[1];
                newMatList[0] = matList[0];
                GetComponent<MeshRenderer>().materials = newMatList;
            }
        }
    }
       
}
