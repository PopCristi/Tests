using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    // display Time public TextMeshProGUI timeDisplay;
    // display Day public TextMeshProGUI dayDisplay;
    public Volume ppv; //post processing volume

    public float tick;
    public float seconds;
    public int mins;
    public int hours;
    public int days = 1;

    public bool activateLights;
    public GameObject[] lights;
    public SpriteRenderer[] stars;

    private void Start()
    {
        ppv = gameObject.GetComponent<Volume>();
    }

    private void FixedUpdate()
    {
        CalcTime();
        DisplayTime();
    }

    public void CalcTime()
    {
        seconds += Time.fixedDeltaTime * tick;

        if (seconds >= 60)
        {
            seconds = 0;
            mins += 1;
        }

        if (mins >= 60)
        {
            mins = 0;
            hours += 1;
        }

        if (hours >= 24)
        {
            hours = 0;
            days += 1;
        }

        ControlPPV();
    }

    public void ControlPPV()
    {
        if(hours >=21 && hours < 22)
        {
            ppv.weight = (float)mins / 60;

            if(!activateLights)
            {
                if(mins > 45)
                {
                    for(int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(true);
                    }
                    activateLights = true;
                }
            }
        }

        if(hours >= 6 && hours < 7)
        {
            ppv.weight = 1 - (float)mins / 60;
            if (activateLights)
            {
                if(mins > 45)
                {
                    for(int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(false);
                    }
                }
            }
        }
    }

    public void DisplayTime()
    {
        Debug.Log(string.Format("{0:00}:{1:00}", hours, mins));
    }
}
