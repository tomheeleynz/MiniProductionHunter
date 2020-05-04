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
        /*if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f) && !stopped)
        {
            Stop();
        }*/

        rb.transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!stopped)
        {
            Stop();

            // Check if hit player
            if (other.gameObject.GetComponent<CharacterShooting>() != null)
            {
                // Kill player here
                Debug.Log("Hit a player");
                bloodTrail.SetActive(true);
                Destroy(gameObject,2);

                other.gameObject.GetComponent<CharacterShooting>().CurrentHealthPoints -= 1;

                if (other.gameObject.GetComponent<CharacterShooting>().CurrentHealthPoints <= 0 )
                {
                    for (int i = 0; i < other.gameObject.GetComponent<CharacterShooting>().numSkins; i++)
                    {
                        Instantiate(skinDrop, other.gameObject.transform.position, transform.rotation);
                    }
                    other.gameObject.GetComponent<CharacterShooting>().numSkins = 0;
                    other.gameObject.GetComponent<Death>().isDead = true;
                    Destroy(gameObject,2);
                }
            }
            else if (other.gameObject.GetComponent<Deer_AI>() != null)
            {
                other.gameObject.GetComponent<Deer_AI>().deerHp--;
                Debug.Log("Hit Stag");
                bloodTrail.SetActive(true);
                Destroy(gameObject,2);

                if (other.gameObject.GetComponent<Deer_AI>().deerHp <= 0)
                {
                    Debug.Log("YOU WIN");
                    GameObject skin = (GameObject)Instantiate(skinDrop, other.gameObject.transform.position, transform.rotation);
                    Destroy(other.gameObject);
                }
            }
            else if (other.gameObject.GetComponent<Deer_Vision_Cone>() != null)
            {
                other.gameObject.GetComponent<Deer_Vision_Cone>().Deer.GetComponent<Deer_AI>().deerHp -= 3;
                Debug.Log("Headshot Stag");
                bloodTrail.SetActive(true);
                Destroy(gameObject,2);

                if (other.gameObject.GetComponent<Deer_Vision_Cone>().Deer.GetComponent<Deer_AI>().deerHp <= 0)
                {
                    Debug.Log("YOU WIN");
                    GameObject skin = (GameObject)Instantiate(skinDrop, other.gameObject.transform.position, transform.rotation);
                    Destroy(other.gameObject.GetComponent<Deer_Vision_Cone>().Deer.gameObject);
                }
            }
        }
        else if (canCollect && other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterShooting>().ammo++;
            Destroy(gameObject);
        }
    }
}
