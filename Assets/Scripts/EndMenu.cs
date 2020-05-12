using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    PlayerControls _controls;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.UI.Select.started += ctx => RestartGame();
        _controls.UI.Quit.started += ctx => EndGame();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _controls.UI.Enable();
    }

    private void OnDisable()
    {
        _controls.UI.Disable();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void EndGame()
    {
        Application.Quit();
    }
}
