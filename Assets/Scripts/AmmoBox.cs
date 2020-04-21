using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<CharacterShooting>().ammo < 6)
            {
                other.gameObject.GetComponent<CharacterShooting>().ammo = 6;
            }
        }
    }
}
