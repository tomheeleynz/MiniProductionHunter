using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft = 120;
    public TMPro.TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        //text.text = timeLeft.ToString();

        float minutes = Mathf.Floor(timeLeft / 60);
        float seconds = timeLeft % 60;

        string inbetween = (seconds > 10) ? (":") : (":0");

        text.text = (minutes + inbetween + Mathf.RoundToInt(seconds));
        if (timeLeft <= 0.0f)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
