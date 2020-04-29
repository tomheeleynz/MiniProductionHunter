using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFuncs : MonoBehaviour
{
    public float fadeTime = 0.0f;
    public GameObject fadePanel = null;

    private bool isFading = false;
    private float totalFade = 0.0f;
    private string sceneName;
    private void Update()
    {
        if (isFading)
        {
            totalFade += Time.deltaTime;

            if (totalFade >= fadeTime)
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Color newCol = fadePanel.GetComponent<Image>().color;
                newCol.a = totalFade / fadeTime;
                fadePanel.GetComponent<Image>().color = newCol;
            }
        }
    }

    public void LoadScene(string _sceneToLoad)
    {
        if (fadeTime > 0.0f && fadePanel != null)
        {
            sceneName = _sceneToLoad;
            isFading = true;
        }
        else
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
