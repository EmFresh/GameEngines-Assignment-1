using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text text;

    TimeSpan tim;
    DateTime start, lastTime;
    bool endb = true;
    private void OnEnable()
    {
        begin();
    }


    TimeSpan getTime()
    {
        if (endb)
            return lastTime - start;
        else
            return (lastTime = DateTime.Now) - start;
    }

    private void Update()
    {
        var timer = getTime();
        text.text = text.text.Substring(0, text.text.IndexOf(":") + 1) + " " + timer.Minutes + " : " + timer.Seconds;
    }

    public void begin()
    {
        start = DateTime.Now;
        endb = false;
    }

    public void end()
    {
        endb = true;
    }
}
