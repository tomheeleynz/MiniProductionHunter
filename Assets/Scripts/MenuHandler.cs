using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Camera MenuCam;
    private Camera MainCam;
    private GameObject GameUI;
    private GameObject MMUI;
    private GameObject PauseUI;
    private GameObject EndUI;

    private bool TogglePauseMenu = true;
    public bool EscPauseEnable = true;

    public KeyCode TriggerEndKey = KeyCode.End; //I didn't know which key to make it :(

    void Start()
    {
        MainCam = Camera.main;
        GameUI = this.transform.Find("GameUI").gameObject;
        MMUI = this.transform.Find("MainMenuUI").gameObject;
        PauseUI = this.transform.Find("PauseUI").gameObject;
        EndUI = this.transform.Find("EndUI").gameObject;

        MMUI.SetActive(true);
        MenuCam.gameObject.SetActive(true);
        GameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EscPauseEnable && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(TogglePauseMenu);
            //TogglePauseMenu = !TogglePauseMenu;
        }

        if (Input.GetKeyDown(TriggerEndKey))
        {
            EndGame();
        }
    }

    //Called on mainmenu startgame button
    public void StartGame()
    {
        MenuCam.gameObject.SetActive(false);
        Time.timeScale = 1;
        MMUI.SetActive(false);
        GameUI.SetActive(true);
    }

    /// <summary>
    /// Pauses on true passed in, continues on false passed in.
    /// </summary>
    /// <param name ="_isTrue">Parameter value to pass.</param>
    public void PauseGame(bool _isTrue) 
    {
        Time.timeScale = (_isTrue) ? (0) : (1);
        GameUI.SetActive(!_isTrue);
        PauseUI.SetActive(_isTrue);

        TogglePauseMenu = !TogglePauseMenu;
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        GameUI.SetActive(false);
        EndUI.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
