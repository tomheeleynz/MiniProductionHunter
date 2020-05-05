using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeerProximity : MonoBehaviour
{
    AudioClip alert;
    
    void Start()
    {
       alert = AssetDatabase.LoadAssetAtPath("Assets/SFX/Deer/alertwfade.mp3", typeof(AudioClip)) as AudioClip;

       TimeWait = Random.Range(5.0f, 8.0f);
    }

    float Timer = 0;
    float TimeWait = 5;
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > TimeWait)
        {
            TimeWait = Random.Range(5.0f, 8.0f);
            Timer = 0;
            AudioSource.PlayClipAtPoint(alert, gameObject.transform.position);

        }
    }
}
