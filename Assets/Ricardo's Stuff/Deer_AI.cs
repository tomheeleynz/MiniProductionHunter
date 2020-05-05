using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer_AI : MonoBehaviour
{
    public int deerHp = 3;
    public enum AIStates { Idle, Roaming, Standing, Running, Alert }
    public AIStates currentState = AIStates.Idle;

    public int RadiusOfAlert = 15;

    public float RoamingSpeed = 3.5f;
    public float RunningSpeed = 20.0f;
    public Animator animator;

    // Finding Closest Player
    public Transform player;
    public Transform VisionConePlayer;
    float checkCounter = 0.0f;
    bool alreadylooking = false;

    SphereCollider c;

    NavMeshAgent agent;
    NavMeshPath navMeshPath;


    //public AudioClip[] DeerSounds;
    //AudioSource DeerSound;

    bool switchActions = false;
    float actionTimer = 0;
    float AlertTimer = 0;

    float howfardeerhastorun = 20f;
    float multiplier = 1;
    bool reverseFlee = false;

    Vector3 closestEdge;
    float distanceToEdge;
    float distance;

    float timeStuck = 0;

    List<Vector3> previousIdlePoints = new List<Vector3>();


    public bool PlayerChecked = false;

    Vector3 runTo = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0;
        agent.autoBraking = true;

        animator = gameObject.GetComponent<Animator>();
        //DeerSound = gameObject.GetComponent<AudioSource>();

        c = gameObject.AddComponent<SphereCollider>();
        c.isTrigger = true;
        c.radius = RadiusOfAlert;

        navMeshPath = new NavMeshPath();

        currentState = AIStates.Idle;
        actionTimer = Random.Range(0.1f, 2.0f);
        //SwitchAnimationState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Closest Stag every 20 sec
        if (checkCounter <= 0.0f)
        {
            findClosestPlayer();
        }
        else
        {
            checkCounter -= Time.deltaTime;
        }

        //Debug.Log(currentState);
        if (actionTimer > 0)
        {
            actionTimer -= Time.deltaTime;
        }
        else
        {
            switchActions = true;
        }

        if (currentState == AIStates.Idle)
        {
            //DeerSound.clip = DeerSounds[1];
            if (player)
            {
                agent.SetDestination(RandomNavSphere(transform.position, Random.Range(1, 2.4f)));
                currentState = AIStates.Running;
                //SwitchAnimationState(currentState);
            }
            else
            {
                actionTimer = Random.Range(7, 12);
                PlayerChecked = false;
                currentState = AIStates.Standing;
                SwitchAnimationState(currentState);


                previousIdlePoints.Add(transform.position);
                if (previousIdlePoints.Count > 5)
                {
                    previousIdlePoints.RemoveAt(0);
                }
            }


        }
        else if (currentState == AIStates.Roaming)
        {
            agent.speed = RoamingSpeed;
            //DeerSound.clip = DeerSounds[1];

            if (DoneReachingDestination())
            {
                currentState = AIStates.Idle;
            }

        }
        else if (currentState == AIStates.Standing)
        {
            bool PathClear;
            if (switchActions)
            {
                //Debug.Log("True");
                /*if (!animator || animator.GetCurrentAnimatorStateInfo(0).normalizedTime - Mathf.Floor(animator.GetCurrentAnimatorStateInfo(0).normalizedTime) > 0.99f)
                {*/
                //Debug.Log("Here");
                do
                {
                    agent.destination = RandomNavSphere(transform.position, Random.Range(5, 120));
                    agent.CalculatePath(agent.destination, navMeshPath);
                    if (navMeshPath.status != NavMeshPathStatus.PathComplete)
                    {
                        PathClear = false;
                        //Debug.Log("Can't go there");
                    }
                    else
                    {
                        //Debug.Log("Path Clear");
                        PathClear = true;

                    }

                    //SwitchAnimationState(currentState);
                    /*}*/
                } while (!PathClear);
                currentState = AIStates.Roaming;

            }
        }
        else if (currentState == AIStates.Alert)
        {

            //DeerSound.clip = DeerSounds[0];

            AlertLookAround(true);
            switchActions = false;
            StartCoroutine(Countdown(4));
            currentState = AIStates.Idle;
            AlertLookAround(false);


            switchActions = true;

        }
        else if (currentState == AIStates.Running)
        {
            agent.speed = RunningSpeed;
            //DeerSound.clip = DeerSounds[0];

            if (player)
            {
                if (reverseFlee)
                {
                    if (DoneReachingDestination() && timeStuck < 0)
                    {
                        reverseFlee = false;
                    }
                    else
                    {
                        timeStuck -= Time.deltaTime;
                    }
                }
                else
                {
                    Vector3 runTo = transform.position + ((transform.position - player.position) * multiplier);
                    distance = (transform.position - player.position).sqrMagnitude;

                    //Find the closest NavMesh edge
                    NavMeshHit hit;
                    if (NavMesh.FindClosestEdge(transform.position, out hit, NavMesh.AllAreas))
                    {
                        closestEdge = hit.position;
                        distanceToEdge = hit.distance;
                        //Debug.DrawLine(transform.position, closestEdge, Color.red);
                    }

                    if (distanceToEdge < 1f)
                    {
                        if (timeStuck > 1.5f)
                        {
                            if (previousIdlePoints.Count > 0)
                            {
                                runTo = previousIdlePoints[Random.Range(0, previousIdlePoints.Count - 1)];
                                reverseFlee = true;
                            }
                        }
                        else
                        {
                            timeStuck += Time.deltaTime;
                        }
                    }

                    if (distance < howfardeerhastorun * howfardeerhastorun)
                    {
                        agent.SetDestination(runTo);
                    }
                    else
                    {
                        player = null;
                        PlayerChecked = false;
                    }
                }

                //Temporarily switch to Idle if the Agent stopped
                if (agent.velocity.sqrMagnitude < 0.1f * 0.1f)
                {
                    SwitchAnimationState(AIStates.Idle);
                }
                else
                {
                    SwitchAnimationState(AIStates.Running);
                }
            }
            else
            {
                //Check if we've reached the destination then stop running
                if (DoneReachingDestination())
                {
                    actionTimer = Random.Range(1.4f, 3.4f);
                    currentState = AIStates.Standing;
                    //SwitchAnimationState(AIStates.Idle);
                }
            }

            switchActions = false;
        }

    }

    public void findClosestPlayer()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
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
        VisionConePlayer = closestEnemy.transform;

        checkCounter = 20.0f;
    }

    bool DoneReachingDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void SwitchAnimationState(AIStates state)
    {
        //Debug.Log(animator);
        if (animator)
        {
            //animator.SetBool("isStanding", state == AIStates.Standing);
            //animator.SetBool("isRunning", state == AIStates.Running);
            //animator.SetBool("isRoaming", state == AIStates.Roaming);
        }

    }

    Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);

        return navHit.position;



    }

    void AlertLookAround(bool triggered)
    {
        if (!alreadylooking && triggered)
        {
            //SwitchAnimationState(AIState.Alert);
        }
        alreadylooking = triggered;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (player)
    //    {
    //        if (other.gameObject.tag == player.tag)
    //        {
    //            player = other.transform;
    //        }
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        /*if(other.CompareTag("Player"))
        {

            player = other.transform;

            actionTimer = Random.Range(0.24f, 0.8f);
            currentState = AIStates.Idle;
            //SwitchAnimationState(currentState);
        }
        else*/
        if (other.CompareTag("Arrow"))
        {
            Transform arrow = other.transform;

            //DeerSound.clip = DeerSounds[2];

            actionTimer = Random.Range(0.24f, 0.8f);
            currentState = AIStates.Idle;
            SwitchAnimationState(currentState);


            if (deerHp <= 0)
            {
                //DeerSound.clip = DeerSounds[3];
                StartCoroutine(Countdown(2));
                Destroy(gameObject);
            }

        }

    }

    private IEnumerator Countdown(float duration)
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }
}
