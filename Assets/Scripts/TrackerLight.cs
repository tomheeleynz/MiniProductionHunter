using UnityEngine;

public class TrackerLight : MonoBehaviour
{
    [HideInInspector] public Transform parent;
    [HideInInspector] public Transform target;
    [HideInInspector] public Light lightSource;
    [HideInInspector] public float checkCounter = 0.0f;
    public float distance = 0.0f;

    [Header("Colour Pulses")]
    public Color atTarget = new Color(0,0.75f,1);
    public Color awayTarget = new Color(1,1,1);
    bool pulse = false;

    #region setup
    private void Awake()
    {
        lightSource = GetComponent<Light>();
        parent = GetComponentInParent<Transform>();
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        // Check for Closest Stag every 20 sec
        if (checkCounter <= 0.0f)
        {
            findClosestEnemy();
        }
        else
        {
            checkCounter -= Time.deltaTime;
        }
        // Pulse if theres a Stag in Map
        if (target != null)
        {
            // Get Distance
            distance = (target.transform.position - parent.transform.position).magnitude;
            // Color change over time
            lightSource.color = Color.Lerp(atTarget, awayTarget, distance / 100);

            float pulseRate = distance / 100;

            if (pulse)
            {
                if (lightSource.intensity - Time.deltaTime / 2 / pulseRate <= 1.0f)
                {
                    pulse = false;
                }
                else
                {
                    lightSource.intensity -= Time.deltaTime / 2 / pulseRate;
                }
            }
            else
            {
                if (lightSource.intensity - Time.deltaTime / 2 / pulseRate >= 2.0f)
                {
                    pulse = true;
                }
                else
                {
                    lightSource.intensity += Time.deltaTime / 2 / pulseRate;
                }
            }
        }
        else
        {
            lightSource.enabled = false;
        }
    }

    public void findClosestEnemy()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Stag");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var obj in objs)
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = obj;
                closestDistance = distance;
            }
        }
        if (target != null)
        {
            target = closestEnemy.transform;
        }
        checkCounter = 20.0f;
    }
}
