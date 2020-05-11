using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool bActive = false;

    Material[] matList;
    private void Start()
    {
        matList = GetComponent<MeshRenderer>().materials;
        HighLight(false);
    }

    public void HighLight(bool isHighLighted)
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
