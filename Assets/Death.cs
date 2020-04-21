using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private bool deaded = false;
    public bool isDead;
    public float RespawnTime = 5.0f;
    public float CountDown = 0.0f;
    public TMPro.TextMeshProUGUI text;

    public Transform[] RespawnPoints;

    private void Awake()
    {
        text.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isDead && !deaded)
        {
            this.transform.Find("crouch").gameObject.SetActive(false); //Make invisible
            this.GetComponent<PlayerMovement>().enabled = false; //Freeze movement

            int randIndex = Random.Range(0, RespawnPoints.Length);
            this.transform.position = RespawnPoints[randIndex].position; //Move to spawn position 

            text.gameObject.SetActive(true);
            CountDown = RespawnTime;


            deaded = true;
        }
        else if (isDead && deaded)
        {
            if (CountDown > 0) //Countdown
            {
                gameObject.GetComponent<Rigidbody>().useGravity = false;

                float minutes = Mathf.Floor(CountDown / 60);
                float seconds = CountDown % 60;

                string inbetween = (seconds > 10) ? (":") : (":0");

                text.text = (minutes + inbetween + Mathf.RoundToInt(seconds));
                
                CountDown -= Time.deltaTime;
            }
            else //Respawn
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;

                text.gameObject.SetActive(false);
                this.transform.Find("crouch").gameObject.SetActive(true); //Make visible
                this.GetComponent<PlayerMovement>().enabled = true; //Freeze movement
                isDead = false;
                deaded = false;
            }
        }
    }
}
