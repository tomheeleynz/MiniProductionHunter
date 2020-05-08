using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    PlayerControls _controls;
    private int currentBtnSelected;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.UI.Select.performed += ctx => SelectButton();
    }

    private void OnEnable()
    {
        _controls.UI.Enable();
    }

    private void OnDisable()
    {
        _controls.UI.Disable();
    }

    private void SelectButton()
    {
        StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("MainTerrainScene");
    }

    private void EndGame()
    {
        Application.Quit();
    }
}
