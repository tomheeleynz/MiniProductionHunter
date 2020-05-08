using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] gos;
    public AudioClip deer;
    void Start()
    {
        
        gos = GameObject.FindGameObjectsWithTag("Stag");
        foreach (GameObject go in gos)
        {
           
        }
    }
    float Timer = 0;


    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer>5)
        {
            Timer = 0;
            AudioSource.PlayClipAtPoint(deer, gos[0].transform.position);
           
        }
    }
}
