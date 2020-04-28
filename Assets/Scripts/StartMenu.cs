using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    PlayerControls _controls;
    public Button[] btnArray;
    public GameObject[] arrowSprites;
    private int currentBtnSelected;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.UI.MoveUpMenu.performed += ctx => MoveUpMenu();
        _controls.UI.MoveDownMenu.performed += ctx => MoveDownMenu();
        _controls.UI.Select.performed += ctx => SelectButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        btnArray[0].onClick.AddListener(StartGame);
        btnArray[1].onClick.AddListener(EndGame);
        currentBtnSelected = 0;

        arrowSprites[currentBtnSelected].SetActive(true);
        arrowSprites[currentBtnSelected + 1].SetActive(false);

    }

    private void OnEnable()
    {
        _controls.UI.Enable();
    }

    private void OnDisable()
    {
        _controls.UI.Disable();
    }

    private void MoveUpMenu()
    {
        currentBtnSelected -= 1;
        
        if (currentBtnSelected < 0)
        {
            currentBtnSelected = 0;
        }

        arrowSprites[0].SetActive(true);
        arrowSprites[1].SetActive(false);
    }

    private void MoveDownMenu()
    {
        currentBtnSelected += 1;

        if (currentBtnSelected > btnArray.Length - 1)
        {
            currentBtnSelected = btnArray.Length - 1;
        }
        arrowSprites[1].SetActive(true);
        arrowSprites[0].SetActive(false);

    }

    private void SelectButton()
    {
        if (currentBtnSelected == 0)
        {
            StartGame();
        }
        else if (currentBtnSelected == 1)
        {
            EndGame();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("MainLevel");
    }

    private void EndGame()
    {
        Application.Quit();
    }
}
