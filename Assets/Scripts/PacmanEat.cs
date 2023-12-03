using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanEat : MonoBehaviour
{

    public ModeChange mode;
    public GhostMove Red;
    public GhostMove Pink;
    public GhostMove Blue;
    public GhostMove Orange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "SuperBall")
        {
            mode.SetRunningMode(true);
            mode.SetRunningModeFinish(true);
            mode.SetIntervalTargetTime(0.25f);

            Red.SetRunningModeFinishGhosts(false);
            Pink.SetRunningModeFinishGhosts(false);
            Blue.SetRunningModeFinishGhosts(false);
            Orange.SetRunningModeFinishGhosts(false);

            Destroy(other.gameObject);            
        }

        if (other.gameObject.tag == "Ghost")
        {
            if (mode.GetRunningMode())
            {
                if(other.gameObject.name.Contains("Red"))
                {
                    if (!Red.GetDead())
                    {
                        Red.SetDead(true);
                    }
                }
                if(other.gameObject.name.Contains("Pink"))
                {
                    if (!Pink.GetDead())
                    {
                        Pink.SetDead(true);
                    }
                }
                if(other.gameObject.name.Contains("Blue"))
                {
                    if (!Blue.GetDead())
                    {
                        Blue.SetDead(true);
                    }
                }
                if(other.gameObject.name.Contains("Orange"))
                {
                    if (!Orange.GetDead())
                    {
                        Orange.SetDead(true);
                    }
                }
            }
        }
    }
}
