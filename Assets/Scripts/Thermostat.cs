using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Thermostat : MonoBehaviour
{
    
    [Range(0, 100)]
    [SerializeField] int temp = 40;
    public int timeField;
    public Countdown Timer;

    [SerializeField] MainProcess mp;
    [SerializeField] GameObject viscHandler;
    [SerializeField] GameObject viscDeafult;
    //float timeToVisc;
    public Text tText;

    public void TemperaturePlus()
    {
        temp = temp + 5;
    }

    public void TemperatureMinus()
    {
        temp = temp - 5;
    }
    private void Start() {
        timeField = 0;
    }
    private void Update()
    {
        tText.text = temp.ToString();
    }

    private void UnloadVisc()
    {
        var visc = viscHandler.transform.Find("visc");
        visc.transform.position = viscDeafult.transform.position;
    }
    public void Heat()
    {
        if(timeField == 40){
            if(viscHandler.transform.Find("visc").GetComponent<Visc>().oilTemp == temp)
            {
                Timer.timeRemaining = timeField * 60;
                Timer.secondTimerIsRunning = true;
                mp.StageFourth();
                //GameObject visc = viscHandler.transform.Find("visc").gameObject;
                //timeToVisc = visc.GetComponent<Visc>().oilV / visc.GetComponent<Visc>().viscIndex;
                //Timer.timeRemaining = timeToVisc;
                //Timer.timerIsRunning = true;
            }
        }
    }
}
