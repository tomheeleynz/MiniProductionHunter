using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAudioHandler : MonoBehaviour
{
    private AudioSource thisSource;

    public AudioClip WalkClip;
    public AudioClip RunClip;
    public AudioClip AlertClip;

    private Deer_AI thisAI;

    public bool debugMode = false;
    public enum AudioStates {None, Idle, Walking, Running, Alert }
    private AudioStates currentAudio = AudioStates.None;

    private float TimeElapsed = 0.0f; 
    private bool Alerted = false;
    public AudioSource EnviromentalSource;
    
    private float startVol = 0.2f;

    public float FadeOutTime = 1.0f;
    public float QuietTime = 10.0f;
    public float FadeInTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        thisSource = this.GetComponent<AudioSource>();
        thisAI = this.GetComponent<Deer_AI>();
        startVol = EnviromentalSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Alerted)
        {
            if (EnviromentalSource.volume > 0.0f)
            {
                EnviromentalSource.volume -= startVol * Time.deltaTime / FadeOutTime;
            }
            else
            {
                Alerted = false;
                TimeElapsed = 0.0f;
            }
        }
        else //Fade the crickets
        {
            TimeElapsed += Time.deltaTime;
           // Debug.Log(TimeElapsed);
            if (EnviromentalSource.volume < 1.0f && TimeElapsed >= QuietTime)
            {
                EnviromentalSource.volume += startVol * Time.deltaTime / FadeInTime;
            }
        }

        if (debugMode){ AudioTester();}
        else
        {
            switch (thisAI.currentState) //Check AI state and make sure sound has not played yet
            {
                case Deer_AI.AIStates.Roaming:
                    if (currentAudio != AudioStates.Walking)
                    {
                        thisSource.loop = true;
                        thisSource.clip = WalkClip;
                        thisSource.Play();
                        currentAudio = AudioStates.Walking;
                    }
                    break;
                //case Deer_AI.AIStates.Standing:
                //    if (currentAudio != AudioStates.Idle)
                //    {

                //    }
                //    break;
                case Deer_AI.AIStates.Running:
                    if (currentAudio != AudioStates.Running)
                    {
                        thisSource.loop = true;
                        thisSource.clip = RunClip;
                        thisSource.Play();
                        currentAudio = AudioStates.Running;
                    }
                    break;
                case Deer_AI.AIStates.Alert:
                    if (currentAudio != AudioStates.Alert)
                    {
                        thisSource.loop = false;
                        thisSource.clip = AlertClip;
                        thisSource.Play();

                        Alerted = true;
                        //EnviromentalSource.Stop();

                        currentAudio = AudioStates.Alert;
                    }
                    break;
                default:
                    thisSource.Stop();
                    thisSource.clip = null;
                    break;
            }
        }
        
    }

    private void OnDestroy()
    {
        EnviromentalSource.volume = startVol;
    }

    void AudioTester()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            thisSource.loop = true;
            thisSource.clip = WalkClip;
            thisSource.Play();
            currentAudio = AudioStates.Walking;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            thisSource.loop = true;
            thisSource.clip = RunClip;
            thisSource.Play();
            currentAudio = AudioStates.Running;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            thisSource.loop = false;
            thisSource.clip = AlertClip;
            thisSource.Play();

            Alerted = true;
            //EnviromentalSource.Stop();

            currentAudio = AudioStates.Alert;
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            thisSource.Stop();
            thisSource.clip = null;
        }

    }
}
