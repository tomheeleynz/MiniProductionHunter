using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer_Vision_Cone : MonoBehaviour
{
    public Transform target;
    public Transform Deer;
    public float DeerMaxVision = 20f;

    Deer_AI DeerAI;

    void Awake()
    {
        DeerAI = Deer.GetComponent<Deer_AI>();
    }


    // Update is called once per frame
    void Update()
    {
        DeerAI.findClosestPlayer();
        target = DeerAI.VisionConePlayer;
        Vector3 targetDir = target.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(targetDir, forward);

        //Debug.Log(angle);
        if (angle < 80.0f)
        {

            float dist = Vector3.Distance(target.position, transform.position);
            if (dist < DeerMaxVision)
            {
                //Debug.Log("In angle!");
                RaycastHit hit;

                if (Physics.Raycast(transform.position, (target.position - transform.position), out hit, DeerMaxVision))
                {
                    Debug.DrawRay(transform.position, (target.position - transform.position), Color.green, 8);
                    //Debug.Log(hit.collider.name);
                    if (hit.collider.tag == "Player")
                    {
                        if (DeerAI.PlayerChecked == false)
                        {
                            DeerAI.currentState = Deer_AI.AIStates.Running;
                            DeerAI.player = target;
                            DeerAI.PlayerChecked = true;
                        }
                        //Debug.Log(GettingCurrentStat.currentState);

                        //Debug.Log("I see you!!");
                    }
                }
            }
        }


    }
}
