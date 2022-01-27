using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FPS : MonoBehaviour
{
    public Text fpsDisplay;
    public Text averageFPSDisplay;
    int framesPassed = 0;
    float fpsTotal = 0f;
    public Text minFPSDisplay, maxFPSDisplay;
    float minFPS = Mathf.Infinity;
    float maxFPS = 0f;

    float timeRefresh = 10f;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        timeRefresh -= Time.deltaTime;
        if(timeRefresh < 0 )
        {
            Debug.Log("TimeRefresh");
            timeRefresh = 10f;
            Refresh();
        }

        float fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = "" + fps;

        fpsTotal += fps;
        framesPassed++;
        averageFPSDisplay.text = "" + (fpsTotal / framesPassed);

        if (fps > maxFPS && framesPassed > 10)
        {
            maxFPS = fps;
            maxFPSDisplay.text = "Max: " + maxFPS;
        }
        if (fps < minFPS && framesPassed > 10)
        {
            minFPS = fps;
            minFPSDisplay.text = "Min: " + minFPS;
        }
    }

    private void Refresh()
    {
        maxFPS = 0;
        minFPS = 9999;
    }
}
