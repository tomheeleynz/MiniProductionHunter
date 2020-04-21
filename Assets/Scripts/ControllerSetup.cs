using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerSetup : MonoBehaviour
{
    public static ControllerSetup instance;

    // Array of players
    public GameObject[] arrayOfPlayers;

    // List of gamepads already used
    public List<int> takenIDs = new List<int>();
    public List<int> takenPlayerID = new List<int>();
 
    private void Awake()
    {
        instance = this;

        int playerID = 0;

        foreach (GameObject player in arrayOfPlayers)
        {
            int playerDeviceID = player.GetComponent<PlayerMovement>().deviceID;

            foreach (InputDevice device in InputSystem.devices)
            {
                if (playerDeviceID == 0)
                {
                    string deviceDescription = device.description.deviceClass.ToString();
                    int deviceID = device.deviceId;

                    if (!takenIDs.Contains(deviceID))
                    {
                        if (deviceDescription == "")
                        {
                            player.GetComponent<PlayerMovement>().deviceID = deviceID;
                            player.GetComponent<PlayerMovement>().playerID = playerID;
                            takenIDs.Add(deviceID);
                            takenPlayerID.Add(playerID);
                            playerID++;
                            break;
                        }
                    }
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerManager manager = PlayerManager.instance;

        foreach (int playerID in takenPlayerID)
        {
            manager.moveMap.Add(playerID, new Vector2(0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
