using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    // Maps 
    public Dictionary<int, Vector2> moveMap;
    public Dictionary<int, Vector2> rotationMap;

    void Awake()
    {
        moveMap = new Dictionary<int, Vector2>();
        rotationMap = new Dictionary<int, Vector2>();
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
