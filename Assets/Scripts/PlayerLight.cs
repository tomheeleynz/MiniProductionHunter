using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Light TrackLight;
    public float angle = 0.0f;
    private Transform closestStag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (GameObject.FindGameObjectsWithTag("Stag").Length != 0)
        {
            float closestDist = float.PositiveInfinity;

            foreach (var item in GameObject.FindGameObjectsWithTag("Stag"))
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance < closestDist)
                {
                    closestStag = item.transform;
                    closestDist = distance;
                }
            }

            Vector3 dir = (transform.position - closestStag.position);
            angle = Vector3.Angle(dir, transform.forward);
            //From start intensity to final intensity based on angle
            float lightIntensity = 0.0f + (3.55f * (angle / 160.0f));
            TrackLight.intensity = lightIntensity;
        }
    }
}
