using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class clockScript : MonoBehaviour
{
    [System.Serializable] public struct time
    {
        [Range(1, 12)] public int hour;
        [Range(0,60)] public int minute;
        public bool AM;
    }
    public time startTime;
    public float timeScale;

    private TMP_Text text;
    private float oldTime;
    // Start is called before the first frame update
    void Start()
    {
        oldTime = 0;
        text = transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = startTime.hour.ToString("00") + ":" + startTime.minute.ToString("00") + "\n" + (startTime.AM ? "AM" : "PM");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - oldTime > 1f * timeScale)
        {
            UpdateTime(0, 1);
            oldTime = Time.time;
        }
    }

    void UpdateTime(int hours, int minutes)
    {
        int hour = Int32.Parse(text.text.Split(':')[0]) + hours;
        int minute = Int32.Parse(text.text.Split(':')[1].Split('\n')[0]) + minutes;
        bool isAM = text.text.Split(':')[1].Split('\n')[1] == "AM";

        if (minute >= 60)
        {
            minute %= 60;
            hour++;
        }
        if (hour > 12)
        {
            hour %= 12;
            isAM = !isAM;
        }
        text.text = hour.ToString("00") + ":" + minute.ToString("00") + "\n" + (isAM ? "AM" : "PM");
    }
}
