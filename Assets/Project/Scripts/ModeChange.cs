using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChange : MonoBehaviour
{

    private float targetTime;
    private bool scatterMode;
    private int modeCounter;
    private int phaseCounter;
    private bool runningMode;
    private int runningTimeCounter;
    private bool runningModeFinish;
    private float intervalTargetTime;


    void Start()
    {
        targetTime = 1f;
        scatterMode = false;
        modeCounter = 0;
        phaseCounter = 0;

        runningMode = false;
        runningTimeCounter = 0;
        intervalTargetTime = 1f;
    }


    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime < 0.0f) 
        {
            if (runningMode)
            {
                runningTimeCounter++;

                if (runningTimeCounter == 32 || runningTimeCounter == 34
                    || runningTimeCounter == 38 || runningTimeCounter == 36)
                {
                    runningModeFinish = false;
                }
                else if (runningTimeCounter == 33 || runningTimeCounter == 35
                    || runningTimeCounter == 37 || runningTimeCounter == 39)
                {
                    runningModeFinish = true;
                }
                else if (runningTimeCounter == 40) 
                {
                    runningMode = false;
                    runningTimeCounter = 0;
                    runningModeFinish = false;
                    intervalTargetTime = 1f;
                }
            }
            else
            {
                modeCounter++;

                if (modeCounter == 20 && !scatterMode && (phaseCounter == 1 || phaseCounter == 3 || phaseCounter == 5))
                {
                    scatterMode = true;
                    modeCounter = 0;
                    phaseCounter++;
                }

                if (modeCounter == 7 && scatterMode && (phaseCounter == 2 || phaseCounter == 4))
                {
                    scatterMode = false;
                    modeCounter = 0;
                    phaseCounter++;
                }

                if (modeCounter == 5 && scatterMode && (phaseCounter == 6))
                {
                    scatterMode = false;
                    modeCounter = 0;
                    phaseCounter++;
                }
            }
            targetTime = intervalTargetTime;
        }
    }

    public bool GetScatterMode()
    {
        return scatterMode;
    }

    public bool GetRunningMode()
    {
        return runningMode;
    }

    public void SetRunningMode(bool value)
    {
        runningMode = value;
    }

    public void SetRunningModeFinish(bool value)
    {
        runningModeFinish = value;
    }

    public bool GetRunningModeFinish()
    {
        return runningModeFinish;
    }

    public void SetIntervalTargetTime(float value)
    {
        intervalTargetTime = value;
    }
}
