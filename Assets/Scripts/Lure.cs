using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour
{
    public float effectRange = 30.0f;

    private GameObject currentInstantiate = null;
    public GameObject fireFlyEffect;
    public bool spawnedEffect = false;
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Interactable>().bActive && !spawnedEffect)
        {
            currentInstantiate = Instantiate(fireFlyEffect, transform.position, transform.rotation, transform.parent);
            spawnedEffect = true;
        }
    }

    public void DeactivateLure()
    {
        Destroy(currentInstantiate);
        GetComponent<Interactable>().bActive = false;
        spawnedEffect = false;
    }
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, effectRange);
    }
}
