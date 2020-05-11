using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float CheckRange = 2.0f;
    private Transform hoverOver = null;
    public Camera AttachedCam;

    public bool bDebug = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = AttachedCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray.origin, ray.direction, out hit, CheckRange))
        {
            if (hit.transform.tag == "Interactable")
            {
                hoverOver = hit.transform;
                hoverOver.GetComponent<Interactable>().HighLight(true);
            }
            else
            {
                if (hoverOver != null)
                {
                    hoverOver.GetComponent<Interactable>().HighLight(false);
                    hoverOver = null;
                }

            }
        }
        else
        {
            if (hoverOver != null)
            {
                hoverOver.GetComponent<Interactable>().HighLight(false);
                hoverOver = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && hoverOver != null)
        {
            hoverOver.GetComponent<Interactable>().bActive = true;
        }
    }

    private void OnGUI()
    {
        if (hoverOver != null && bDebug)
        {
            Vector3 screenPos = AttachedCam.WorldToScreenPoint(hoverOver.position);
            GUI.Label(new Rect(screenPos.x, screenPos.y, 100, 70), hoverOver.name + (Vector3.Distance(transform.position, hoverOver.position).ToString()));
        }
    }
}
