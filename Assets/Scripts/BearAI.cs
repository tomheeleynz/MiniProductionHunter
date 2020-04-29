using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearAI : MonoBehaviour
{    enum bearMovement
    {
        Idle,
        Walking,
        Running
    }

    // Variables    
    public int bearHp = 5;

    public GameObject player;

    public float distanceWander = 4.0f;
    public float radiusWander = 3.0f;
    public float walkingSpeed = 3.5f;
    public float runningSpeed = 7f;
    public float totalFOV = 70.0f;
    public float attackRange = 2.0f;
    public float attackDuration = 2.0f;

    Vector3 moveTo = new Vector3(0.0f, 0.0f, 0.0f);

    bearMovement currentMovement = bearMovement.Walking;
    NavMeshAgent agent;
    
    bool attack = false;
    float timerAttack = 0;
    float tempSpeed = 7f;

    float degreeMove;
    float degreeAdd;

    float timerIdle = 0;
    float idleDuration;

    

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        SwitchMovement(bearMovement.Walking);
    }

    void Update()
    {
        if (attack)
        {
            timerAttack += Time.deltaTime;
            if (timerAttack > attackDuration)
            {
                runningSpeed = tempSpeed;
                attack = false;
                timerAttack = 0;
            }
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
            {
                // attack
                print("attack");
                attack = true;
                tempSpeed = runningSpeed;
                runningSpeed /= 10;
            }
        }
        if (Vector3.Distance(moveTo, transform.position) < 1 && currentMovement == bearMovement.Walking)
        {
            SwitchMovement(bearMovement.Walking);
        }
        if (agent.remainingDistance == 0 && currentMovement == bearMovement.Running)
        {
            SwitchMovement(bearMovement.Idle);
        }
        if (currentMovement == bearMovement.Idle)
        {
            gameObject.transform.Rotate(0, degreeAdd, 0, Space.Self);
            degreeMove -= degreeAdd;
            if (degreeMove == 0)
            {
                degreeAdd *= -1f;
                degreeMove = (Random.Range(80, 110) * degreeAdd);
            }
            timerIdle += Time.deltaTime;
            if (timerIdle > idleDuration)
            {
                SwitchMovement(bearMovement.Walking);
                timerIdle = 0;
            }
        }

        if (Vector3.Angle(transform.forward, player.transform.position - transform.position) < totalFOV / 2 &&
            Vector3.Distance(player.transform.position, transform.position) < 20)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, player.transform.position - transform.position);
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.blue);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    //print("hit");
                    moveTo = player.transform.position;
                    SwitchMovement(bearMovement.Running);
                }
            }
        }
        print("test");
       //NavMeshHit hitt;
       //if (NavMesh.FindClosestEdge(transform.position, out hitt, NavMesh.AllAreas) && Vector3.Distance(hitt.position, transform.position) < 4)
       //{
       //    moveTo += (transform.position - hitt.position)*0.01f;
       //    agent.SetDestination(moveTo);
       //}
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
                radius += 0.3f;
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
                    if(Random.Range(0, 2) == 0)
                    {
                        degreeAdd = 0.5f;
                    }
                    else
                    {
                        degreeAdd = -0.5f;
                    }
                    degreeMove = (Random.Range(80, 110) * degreeAdd);
                    idleDuration = (Random.Range(3, 5));
                    break;
                }
            case bearMovement.Running:
                {
                    agent.speed = runningSpeed;
                    agent.SetDestination(moveTo);
                    break;
                }
            case bearMovement.Walking:
                {
                    agent.speed = walkingSpeed;
                    moveTo = RandomNavSphere(transform.position + (transform.forward * distanceWander), radiusWander);
                    
                    agent.SetDestination(moveTo);
                    break;
                }
        }
    }
    /*

    public float speed = 3;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 30;

    float heading;
    Vector3 targetRotation;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Awake()
    {

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeading());
    }

    void Update()
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        var forward = transform.forward;
        agent.Move(forward * Time.deltaTime * speed);
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeadingRoutine()
    {
        NavMeshHit hit;
        float floor = heading;
        float ceil = heading;
        if (NavMesh.FindClosestEdge(transform.position, out hit, NavMesh.AllAreas) && Vector3.Distance(hit.position, transform.position) < 8)
        {
            cyc.transform.position = hit.position;
            float x = Quaternion.LookRotation(hit.position - transform.position, Vector3.up).eulerAngles.y;
            print(x-heading);
            if(x-heading == 0)
            {
                floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
                ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
                if (Random.Range(0, 2) == 0)
                {
                    heading = floor;
                }
                else
                {
                    heading = ceil;
                }
            }
            else
            {
                if(x-heading > 0)
                {
                    floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
                }
                else
                {
                    ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
                }
                
                
                heading = Random.Range(floor, ceil);
            }    
        }
        else
        {
            floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
            ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
            heading = Random.Range(floor, ceil);
        }

        
        
        targetRotation = new Vector3(0, heading, 0);
    }

    */
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position + (transform.forward * distanceWander), radiusWander);
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
