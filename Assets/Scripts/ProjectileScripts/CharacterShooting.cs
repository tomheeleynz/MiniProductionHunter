using UnityEngine;
using UnityEngine.UI;

public class CharacterShooting : MonoBehaviour
{
    [HideInInspector] public ArcMesh arcMesh;
    [HideInInspector] public int numSkins = 0;
    [HideInInspector] public int ammo = 6;
    [HideInInspector] public bool loadedShot = false;
    [HideInInspector] public bool running = false;
    [HideInInspector] public bool shooting = false;
    [HideInInspector] public bool crouching = false;
    [HideInInspector] public bool jumping = false;
    [HideInInspector] public int CurrentHealthPoints = 0;

    public int MaxHealthPoints = 3;

    public float jumpForce;
    [Header("Aiming Stuff")]
    public GameObject crosshair;
    public bool Aiming = false;
    public bool ADS = false;
    [Header("Arrow Velocity")]
    public float forceApplied = 0.0f;
    public float forceMultiplier = 5.0f;
    public float maxForce = 15.0f;
    [Header("Setup Features")]
    public GameObject projectile;  
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public SphereCollider audioRange;
    [Header("UI Elements")]
    public GameObject ammoCounter;
    public GameObject skinCounter;
    [Header("Cameras")]
    public Camera Cam;
    public Camera fpCam;
    public Canvas canvas;

    #region setup
    private void Awake()
    {
        

        arcMesh = GetComponentInChildren<ArcMesh>();
        arcMesh.parent = this;
        audioSource = GetComponent<AudioSource>();
        audioRange = GetComponentInChildren<SphereCollider>();
        CurrentHealthPoints = MaxHealthPoints;
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        if (ADS)
        {
            crosshair.SetActive(true);
            canvas.worldCamera = fpCam;
        }
        else
        {
            crosshair.SetActive(false);
            canvas.worldCamera = Cam;
        }
        
        float distToGround = GetComponent<Collider>().bounds.extents.y;
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        {
            jumping = false;
        }
        // Shoot
        if (shooting)
        {
            Aiming = false;

            if (ammo > 0 || loadedShot)
            {
                Aiming = true;
                //arcMesh.GetComponent<MeshRenderer>().enabled = true;

                if (loadedShot == false && ammo > 0)
                {
                    audioSource.clip = audioClips[3]; //Play loading sound
                    audioSource.loop = false;
                    audioSource.Play();

                    ammo -= 1;
                    loadedShot = true;
                }

                if (forceApplied + Time.deltaTime * forceMultiplier <= maxForce)
                {
                    forceApplied += Time.deltaTime * forceMultiplier;
                }
            }
        }
        else
        {
            forceApplied = 0;
            //arcMesh.GetComponent<MeshRenderer>().enabled = false;
        }
        // UI Update
        if (loadedShot)
        {
            ammoCounter.GetComponent<TMPro.TextMeshProUGUI>().text = "1/" + ammo.ToString();
        }
        else
        {
            ammoCounter.GetComponent<TMPro.TextMeshProUGUI>().text = "0/" + ammo.ToString();
        }
        // Audio/Range
        if (running)
        {
            audioSource.clip = audioClips[1];
            audioSource.loop = true;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            audioRange.radius = 50.0f;
        }
        else if (!running && !crouching)
        {
            audioSource.clip = audioClips[0];
            audioSource.loop = true;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            audioRange.radius = 10.0f;
        }
        else if (!running && crouching)
        {
            audioSource.clip = audioClips[0];
            audioSource.loop = true;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            audioRange.radius = 5.0f;
        }
        else if (jumping)
        {
            jumping = false;
            audioSource.clip = audioClips[2];
            audioSource.loop = false;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            audioRange.radius = 50.0f;

        }
        else
        {
            audioSource.Stop();
        }
    }

    public void TakeShot()
    {
        // Shoot
        if (loadedShot == true)
        {
            // Shoot
            GameObject bullet = (GameObject)Instantiate(projectile, transform.position + transform.forward * 2, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(arcMesh.launchAngle.transform.forward * arcMesh.velocity);
            forceApplied = 0;

            audioSource.clip = audioClips[4]; //Play firing sound
            audioSource.loop = false; ;
            audioSource.Play();

            loadedShot = false;
        }
    }

    public void Jump()
    {
        float distToGround = GetComponent<Collider>().bounds.extents.y;
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        }
    }
}
