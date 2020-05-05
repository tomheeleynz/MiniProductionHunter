using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    float distToGround = 0;
    Rigidbody rb;
    bool stopped = false;
    bool canCollect = false;
    public GameObject skinDrop;
    public GameObject shootTrail;
    public GameObject groundTrail;
    public GameObject bloodTrail;
    [SerializeField] public float timeLeft;

    private void Stop()
    {
        shootTrail.SetActive(false);
        groundTrail.SetActive(true);
        stopped = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        //GetComponent<SphereCollider>().enabled = false;
        Invoke("Collectable", 0.1f);
        //Destroy(gameObject, 5);
    }

    private void Start()
    {
        shootTrail.SetActive(true);
        groundTrail.SetActive(false);
        bloodTrail.SetActive(false);
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void Collectable()
    {
        canCollect = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped)
        {
            Timer();
        }

        rb.transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!stopped)
        {
            if (other.gameObject.tag == "Stag") {
                Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "Ground") {
                Debug.Log("Ground Hit");
            }
            Stop();
        }
    }

    void Timer()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
