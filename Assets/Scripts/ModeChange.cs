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
    private int running_time_counter;
    private bool running_mode_finish;
    private float interval_targetTime;

    // Start is called before the first frame update
    void Start()
    {
        targetTime = 1f;
        scatterMode = false;
        modeCounter = 0;
        phaseCounter = 0;

        runningMode = false;
        running_time_counter = 0;
        interval_targetTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime < 0.0f) 
        {
            if (runningMode)
            {
                running_time_counter++;

                if (running_time_counter == 32 || running_time_counter == 34
                    || running_time_counter == 38 || running_time_counter == 36)
                {
                    running_mode_finish = false;
                }
                else if (running_time_counter == 33 || running_time_counter == 35
                    || running_time_counter == 37 || running_time_counter == 39)
                {
                    running_mode_finish = true;
                }
                else if (running_time_counter == 40) 
                {
                    runningMode = false;
                    running_time_counter = 0;
                    running_mode_finish = false;
                    interval_targetTime = 1f;
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
            targetTime = interval_targetTime;
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
        running_mode_finish = value;
    }

    public bool GetRunningModeFinish()
    {
        return running_mode_finish;
    }

    public void SetIntervalTargetTime(float value)
    {
        interval_targetTime = value;
    }
}
