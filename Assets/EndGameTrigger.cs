using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public float InGameStartTime = 0500;

    private float displayedGameTime;

    public float TimeStepPerSecond = 2;

    public float TimeOfGameInSeconds = 180;
    private float ElapsedTime = 0.0f;
    private float ElapsedTimeMag = 0.0f;
    public int DeerSkinRequired = 3;
    private int DeerSkinCollected = 0;

    public bool EndGameAfterTimer = true;
    public bool Debug = false;
    public float TimeSpeedUp = 1.0f;

    private MenuFuncs SceneLoader;

    public GameObject TimeDial;
    // Start is called before the first frame update
    void Start()
    {
        displayedGameTime = InGameStartTime;
        SceneLoader = this.GetComponent<MenuFuncs>();
    }

    private float updateGameTime = 0.0f;
    // Update is called once per frame
    void Update()
    {
        ElapsedTime += (Time.deltaTime * TimeSpeedUp);

        if (ElapsedTime - 5 >= updateGameTime) //after 5 seconds
        {
            updateGameTime = ElapsedTime;
            displayedGameTime += 5;

            if (displayedGameTime >= InGameStartTime + 60)
            {
                InGameStartTime += 100;
                displayedGameTime = InGameStartTime;
            }
        }

        if (EndGameAfterTimer && ElapsedTime >= TimeOfGameInSeconds ||
            DeerSkinCollected >= DeerSkinRequired)
        {
            SceneLoader.LoadScene("EndScene");
        }


        ElapsedTimeMag = ElapsedTime / TimeOfGameInSeconds;
        RenderSettings.skybox.SetFloat("_Exposure", 0.13f + (0.37f * ElapsedTimeMag));
        RenderSettings.sun.intensity = 0.1f + (0.2f * ElapsedTimeMag);


        TimeDial.transform.rotation = Quaternion.Euler(0, 0, 85.0f - (75.0f * ElapsedTimeMag));
    }

    void OnGUI()
    {
        if (Debug)
        {

            GUI.Label(new Rect(10, 0, 50, 20), ElapsedTime.ToString("f2"));
            GUI.Label(new Rect(50, 0, 200, 20), "Ingame time: 0" + displayedGameTime.ToString("f0"));
            GUI.Label(new Rect(10, 18, 100, 20), "DeerSkin: " + DeerSkinCollected.ToString());

            if (GUI.Button(new Rect(200, 0, 50, 20), "SPEED"))
            {
                ElapsedTime += 1;
            }


            if (GUI.Button(new Rect(90, 20, 30, 15), "-1") && DeerSkinCollected > 0)
            {
                DeerSkinCollected--;
            }
            else if(GUI.Button(new Rect(120, 20, 30, 15), "+1"))
            {
                DeerSkinCollected++;
            }

        }
    }

}
