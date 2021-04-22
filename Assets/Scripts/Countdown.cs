using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float timeRemaining = 10;
    public float curTime = 0;
    public bool timerIsRunning = false;
    public bool secondTimerIsRunning = false;
    public Text timeText;
    [SerializeField] GameObject viscHandler;

    [SerializeField] MainProcess mp;

    private void Start()
    {
        //Начало отсчёта
        //timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (curTime < timeRemaining)
            {
                curTime += Time.deltaTime;
                DisplayTime(curTime);
            }
            else
            {
                Debug.Log("Время вышло!");
                timeRemaining = 0;
                curTime = 0;
                timerIsRunning = false;
                mp.lastHint();
            }
        }
        if (secondTimerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime*50f;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Время вышло!");
                timeRemaining = 0;
                curTime = 0;
                secondTimerIsRunning = false;
                DisplayTime(timeRemaining);
                viscHandler.transform.Find("visc").GetComponent<Visc>().StartMesButton.SetActive(true);
                viscHandler.transform.Find("visc").GetComponent<Visc>().Syringe.SetActive(true);
                mp.StageFifth();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }
}