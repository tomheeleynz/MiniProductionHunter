using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject outline;
    public GameObject textObj;

    private Interactable interactable;
    public bool TextActive = false;

    public bool inRange = false;
    public GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, playerObj.transform.position) < 5.0f)
        {
            outline.SetActive(true);
            inRange = true;
        }
        else
        {
            outline.SetActive(false);
            inRange = false;
        }
    }

    public void buttonPress()
    {
        if (inRange && !textObj.transform.parent.gameObject.activeSelf)
        {
            textObj.SetActive(true);
            textObj.transform.parent.gameObject.SetActive(true);
        }
        else if (textObj.activeSelf)
        {
            textObj.SetActive(false);
            textObj.transform.parent.gameObject.SetActive(false);
        }
        //if  (!toggle && GetComponent<Interactable>().hoverOver)
        //{
        //    //textObj.SetActive(true);
        //    textObj.transform.parent.gameObject.SetActive(true);
        //    //GetComponent<Interactable>().bActive = false;
        //    toggle = true;
        //}
        //else
        //{
        //    toggle = false;
        //    textObj.transform.parent.gameObject.SetActive(false);
        //}
    }
}
