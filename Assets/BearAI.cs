
using UnityEngine;
using UnityEngine.AI;

public class BearAI : MonoBehaviour
{
    // Variables    
    public GameObject cyc;

    public float distanceWander = 0.0f;
    public float radiusWander = 5.0f;

    Vector3 moveTo = new Vector3(0.0f, 0.0f, 0.0f);

    bearMovement currentMovement = bearMovement.Idle;
    NavMeshAgent agent;

    enum bearMovement
    {
        Idle,
        Walking,
        Running
    }

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {   
        if(Input.GetMouseButtonDown(0))
        {
            SwitchMovement(bearMovement.Walking);
        }
        if(Vector3.Distance(moveTo,transform.position) <1)
        {
            SwitchMovement(bearMovement.Walking);
        }
        
    }

    Vector3 RandomNavSphere(Vector3 origin, float radius)
    {
        while(true)
        {
            Vector3 randomDirection = Random.onUnitSphere * radius;

            randomDirection += origin;

            NavMeshHit navHit;

            if (NavMesh.SamplePosition(randomDirection, out navHit, radius, NavMesh.AllAreas))
            {
                return navHit.position;
            }
            else
            {
                radius += 0.1f;
            }
        }
        
        
    }

    void SwitchMovement(bearMovement _movement)
    {
        currentMovement = _movement;
        switch (currentMovement)
        {
            case bearMovement.Idle:
                {

                    break;
                }
            case bearMovement.Running:
                {

                    break;
                }
            case bearMovement.Walking:
                {
                    moveTo = RandomNavSphere(transform.position + (transform.forward * distanceWander), radiusWander);
                    cyc.transform.position = moveTo;
                    agent.SetDestination(moveTo);
                    break;
                }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position + (transform.forward * distanceWander), radiusWander);
        float totalFOV = 70.0f;
        float rayRange = 20.0f;
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
        Gizmos.DrawRay(transform.position + rightRayDirection * rayRange, (transform.position + leftRayDirection * rayRange) - (transform.position + rightRayDirection * rayRange));
        
    }
}
