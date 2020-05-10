using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReallyNear : MonoBehaviour
{
    Deer_AI DeerAI;
    public Transform Deer;

    void Awake()
    {
        DeerAI = Deer.GetComponent<Deer_AI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            DeerAI.player = other.transform;
            DeerAI.currentState = Deer_AI.AIStates.Running;
        }

    }




}
