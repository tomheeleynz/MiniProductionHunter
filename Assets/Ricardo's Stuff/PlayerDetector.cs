using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public float maxRadius = 30f;
    public GameObject Player;
    Deer_AI DeerAI;
    FirstPersonPlayerMovement PlayerMove;

    SphereCollider PDetector;

    void Awake()
    {
        PlayerMove = Player.GetComponent<FirstPersonPlayerMovement>();

        PDetector = gameObject.GetComponent<SphereCollider>();
    }


    void Update()
    {

        //Debug.Log(PlayerMove.Move.magnitude);

        PDetector.radius = maxRadius * PlayerMove.Move.magnitude;

        //Debug.Log(PDetector.radius);

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Stag")
        {
            DeerAI = other.gameObject.GetComponent<Deer_AI>();

            if (!DeerAI.alreadylooking)
            {
                DeerAI.alreadylooking = true;
                DeerAI.currentState = Deer_AI.AIStates.Alert;
                DeerAI.DeerMaxVision = 20f;
                DeerAI.actionTimer = Random.Range(5, 10);
                DeerAI.switchActions = false;
                DeerAI.SwitchAnimationState(Deer_AI.AIStates.Alert);
            }
        }
    }

}
