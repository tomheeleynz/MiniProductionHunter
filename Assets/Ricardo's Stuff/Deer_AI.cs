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
    [HideInInspector] public bool alreadylooking = false;
    public float DeerMaxVision = 10f;

    SphereCollider c;

    NavMeshAgent agent;
    NavMeshPath navMeshPath;


    //public AudioClip[] DeerSounds;
    //AudioSource DeerSound;

    [HideInInspector] public bool switchActions = false;
    [HideInInspector] public float actionTimer = 0;
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

    private List<Lure> lures = new List<Lure>();
    private Lure currentLureInRange = null;
    private bool targetedLure = false;
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
        SwitchAnimationState(currentState);

        foreach (Lure item in Object.FindObjectsOfType(typeof(Lure))) //Populate lure array
        {
            lures.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, agent.destination);

        currentLureInRange = GetLureInRange();
        // Check for Closest Stag every 20 sec (and lure)
        if (checkCounter <= 0.0f)
        {
            findClosestPlayer();
        }
        else
        {
            checkCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))

        {

            StartCoroutine(Fade());


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
                SwitchAnimationState(currentState);
            }
            else
            {
                actionTimer = Random.Range(4, 12);
                Debug.Log(actionTimer);
                
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
                if (targetedLure)
                {
                    //Deactivate lure
                    currentLureInRange.DeactivateLure();
                }
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
                    //if (!animator || animator.GetCurrentAnimatorStateInfo(0).normalizedTime - Mathf.Floor(animator.GetCurrentAnimatorStateInfo(0).normalizedTime) > 0.99f)
                    //{
                    if (currentLureInRange != null && currentLureInRange.spawnedEffect)
                    {
                        agent.destination = currentLureInRange.transform.position;
                        targetedLure = true;
                    }
                    else
                    {
                        agent.destination = RandomNavSphere(transform.position, Random.Range(5, 120));
                    }

                    agent.CalculatePath(agent.destination, navMeshPath);

                    if (navMeshPath.status != NavMeshPathStatus.PathComplete)
                        {
                        if (currentLureInRange != null)
                        {
                            currentLureInRange = null;
                        }
                            PathClear = false;
                            //Debug.Log("Can't go there");
                        }
                        else
                        {
                            //Debug.Log("Path Clear");
                            PathClear = true;

                        }
                    //}
                    //else
                    //{
                    //    PathClear = false;
                    //}

                } while (!PathClear);
                currentState = AIStates.Roaming;
                SwitchAnimationState(currentState);
                /*}*/
            }
        }
        else if (currentState == AIStates.Alert)
        {

            if (switchActions)
            {
                currentState = AIStates.Idle;
                DeerMaxVision = 10f;
                alreadylooking = false;

            }else if(player && alreadylooking)
            {
                float near = (transform.position - player.position).sqrMagnitude;

                if(near < 15)
                {
                    currentState = AIStates.Running;
                    DeerMaxVision = 10f;
                    alreadylooking = false;
                }

            }
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
                        if (targetedLure)
                        {
                            //Deactivate lure
                            currentLureInRange.DeactivateLure();
                            targetedLure = false;
                        }

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
                    if (targetedLure)
                    {
                        //Deactivate lure
                        currentLureInRange.DeactivateLure();
                    }

                    actionTimer = Random.Range(1.4f, 3.4f);
                    currentState = AIStates.Standing;
                    SwitchAnimationState(AIStates.Idle);
                }
            }

            
        }
        switchActions = false;
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

    Lure GetLureInRange()
    {
        foreach (Lure item in lures)
        {
            if (Vector3.Distance(item.transform.position, transform.position) < item.effectRange)
            {
                return (item);
            }
        }

        return null;
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

    public void SwitchAnimationState(AIStates state)
    {
        //Debug.Log(animator);
        if (animator)
        {
            if(state == AIStates.Standing)
            {
                animator.SetBool("isStanding", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isRoaming", false);
                animator.SetBool("isAlert", false);

            }
            else if (state == AIStates.Running)
            {
                animator.SetBool("isStanding", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("isRoaming", false);
                animator.SetBool("isAlert", false);

            }
            else if (state == AIStates.Roaming)
            {
                animator.SetBool("isStanding", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isRoaming", true);
                animator.SetBool("isAlert", false);

            }
            else if (state == AIStates.Alert)
            {
                animator.SetBool("isStanding", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isRoaming", false);
                animator.SetBool("isAlert", true);

            }


            //animator.SetBool("isStanding", state == AIStates.Standing);
            //animator.SetBool("isRunning", state == AIStates.Running);
            //animator.SetBool("isRoaming", state == AIStates.Roaming);
            //animator.SetBool("isAlert", state == AIStates.Alert);
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

    //void AlertLookAround(bool triggered)
    //{
    //    if (!alreadylooking && triggered)
    //    {
    //        SwitchAnimationState(AIStates.Alert);
    //    }
    //    alreadylooking = triggered;
    //}

    IEnumerator Fade()

    {

        for (float ft = 1f; ft >= 0; ft -= 0.1f)

        {

            Color c = GetComponent<Light>().color;

            c.g = ft;

            c.b = ft;

            GetComponent<Light>().color = c;



            if (ft < 0.1f)

            {

                StartCoroutine(FadeOut());

            }

            yield return new WaitForSeconds(.02f);

        }

    }

    IEnumerator FadeOut()

    {

        for (float ft = 0f; ft <= 1; ft += 0.1f)

        {

            Color c = GetComponent<Light>().color;

            c.g = ft;

            c.b = ft;

            GetComponent<Light>().color = c;



            if (ft > 0.9f)

            {

                yield break;

            }

            yield return new WaitForSeconds(.02f);

        }



    }
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



            StartCoroutine(Fade());
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
