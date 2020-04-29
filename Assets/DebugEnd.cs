using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnd : MonoBehaviour
{
    public bool bDebug = false;
    public KeyCode LoadSceneKey;
    public string SceneToLoad;

    MenuFuncs menuScript;
    // Start is called before the first frame update
    void Start()
    {
        menuScript = GetComponent<MenuFuncs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bDebug && Input.GetKeyDown(LoadSceneKey))
        {
            menuScript.LoadScene(SceneToLoad);
        }
    }
}
