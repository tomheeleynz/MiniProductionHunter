using UnityEngine;

public class SkinScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Inventory>().numSkins++;
            //other.gameObject.GetComponent<CharacterShooting>().skinCounter.GetComponent<TMPro.TextMeshProUGUI>().text = other.GetComponent<CharacterShooting>().numSkins.ToString();
            Destroy(gameObject);
        }
    }
}
