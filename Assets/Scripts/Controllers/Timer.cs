using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool timerStopped = false;
    public GameObject textObject;
    public string timerString;
    private TMP_Text timerText;
    public float timer;
    public int hours;
    public int minutes;
    public int seconds;
    public int miliseconds;

    void Start() 
    {
        timerText = textObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(timerStopped) {
            return;
        }
        timer = Time.timeSinceLevelLoad * 1000;
        timer = Mathf.Round(timer);
        miliseconds = (int)timer % 1000;
        timer /= 1000;
        hours = (int)timer / 360;
        timer = (int)timer % 360;
        minutes = (int)timer / 60;
        timer = (int)timer % 60;
        seconds = (int)timer / 1;
        if(hours != 0) {
            timerString = hours + ":" + minutes + ":" + seconds + "." + miliseconds;
        } else {
            timerString = minutes + ":" + seconds + "." + miliseconds;
        }
        timerText.SetText(timerString);
    }
    
    public void StopTimer() {
        timerStopped = true;
    }
}
