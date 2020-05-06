using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    public int DeerPopulationRequired = 0;
    public int TotalDeerToSpawn = 2;
    private Transform[] respawnPoints;

    public GameObject DeerPrefab;
        // Start is called before the first frame update
    void Start()
    {
        respawnPoints = new Transform[transform.childCount];

        int i = 0;
        foreach (Transform child in transform)
        {
            respawnPoints[i] = child;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] ai = GameObject.FindGameObjectsWithTag("Stag");

        int count = ai.Length;
        if (count == DeerPopulationRequired)
        {
            for (int i = 0; i < count + TotalDeerToSpawn; i++)
            {
                int spawnAt = Random.Range(0, respawnPoints.Length - 1);

                Instantiate(DeerPrefab, respawnPoints[spawnAt].position, respawnPoints[spawnAt].rotation);
            }
        }
    }
}
