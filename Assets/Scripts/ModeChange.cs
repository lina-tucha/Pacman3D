using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChange : MonoBehaviour
{

    private float targetTime;

    private bool scatterMode;
    private int modeCounter;
    private int phaseCounter;

    // Start is called before the first frame update
    void Start()
    {
        targetTime = 1f;
        scatterMode = false;
        modeCounter = 0;
        phaseCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime < 0.0f)
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
            targetTime = 1f;
        }
    }

    public bool GetScatterMode()
    {
        return scatterMode;
    }
}
