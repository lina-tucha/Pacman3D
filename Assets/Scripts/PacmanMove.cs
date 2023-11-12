using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{

    public float smoothFactor;

    private float target_X;
    private float target_Z;
    private float Y_rotation;

    private bool looking_North;
    private bool looking_East;
    private bool looking_South;
    private bool looking_West;

    private bool time_to_do;
    private float targetTime;



    // Start is called before the first frame update
    void Start()
    {
        target_X = transform.position.x;
        target_Z = transform.position.z;
        Y_rotation = transform.eulerAngles.y;

        looking_North = false;
        looking_East = false;
        looking_South = false;
        looking_West = false;

        time_to_do = true;
        targetTime = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            if (looking_North)
            {
                LookingTarget(ref looking_North, ref looking_East, ref looking_South, ref looking_West);
            }
            else if (looking_East)
            {
                LookingTarget(ref looking_East, ref looking_North, ref looking_South, ref looking_West);
            }
            else if (looking_South)
            {
                LookingTarget(ref looking_South, ref looking_East, ref looking_North, ref looking_West);
            }
            else if (looking_West)
            {
                LookingTarget(ref looking_West, ref looking_East, ref looking_South, ref looking_North);
            }
            else
            {
                LookingTarget(ref looking_North, ref looking_East, ref looking_South, ref looking_West);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            if (looking_North)
            {
                LookingTarget(ref looking_East, ref looking_North, ref looking_South, ref looking_West);
            }
            else if (looking_East)
            {
                LookingTarget(ref looking_South, ref looking_North, ref looking_East, ref looking_West);
            }
            else if (looking_South)
            {
                LookingTarget(ref looking_West, ref looking_East, ref looking_North, ref looking_South);
            }
            else if (looking_West)
            {
                LookingTarget(ref looking_North, ref looking_East, ref looking_South, ref looking_West);
            }
            else
            {
                LookingTarget(ref looking_East, ref looking_North, ref looking_South, ref looking_West);
            }
        }

        if (Input.GetKey(KeyCode.DownArrow)) 
        {
            if (looking_North)
            {
                LookingTarget(ref looking_South, ref looking_East, ref looking_North, ref looking_West);
            }
            else if (looking_East)
            {
                LookingTarget(ref looking_West, ref looking_North, ref looking_South, ref looking_East);
            }
            else if (looking_South)
            {
                LookingTarget(ref looking_North, ref looking_East, ref looking_South, ref looking_West);
            }
            else if (looking_West)
            {
                LookingTarget(ref looking_East, ref looking_West, ref looking_South, ref looking_North);
            }
            else
            {
                LookingTarget(ref looking_South, ref looking_East, ref looking_North, ref looking_West);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            if (looking_North)
            {
                LookingTarget(ref looking_West, ref looking_North, ref looking_South, ref looking_East);
            }
            else if (looking_East)
            {
                LookingTarget(ref looking_North, ref looking_South, ref looking_East, ref looking_West);
            }
            else if (looking_South)
            {
                LookingTarget(ref looking_East, ref looking_West, ref looking_North, ref looking_South);
            }
            else if (looking_West)
            {
                LookingTarget(ref looking_South, ref looking_East, ref looking_North, ref looking_West);
            }
            else
            {
                LookingTarget(ref looking_West, ref looking_North, ref looking_South, ref looking_East);
            }
        }

        SetTarget();

        Vector3 toLocation = new Vector3(target_X, 2.5f, target_Z);
        transform.position = Vector3.MoveTowards(transform.position, toLocation, smoothFactor);
        transform.rotation = Quaternion.Euler(0, Y_rotation, 0);

        targetTime -= Time.deltaTime;

        if(targetTime <= 0.0f)
        {
            if (!time_to_do)
            {
                time_to_do = true;
                targetTime = 0.5f;
            }
        }
    }

    void LookingTarget(ref bool looking, ref bool notlooking1, ref bool notlooking2, ref bool notlooking3) 
    {
        if (time_to_do) {
            notlooking1 = notlooking2 = notlooking3 = false;
            looking = true;
            time_to_do = false;
            targetTime = 0.5f;
        }
    }

    void SetTarget()
    {
        if (looking_North)
        {
            target_X = transform.position.x;
            target_Z = 25;
            Y_rotation = 0;
        }
        else if (looking_East)
        {
            target_X = 100;
            target_Z = transform.position.z;
            Y_rotation = 90;
        }
        else if (looking_South)
        {
            target_X = transform.position.x;
            target_Z = -25;
            Y_rotation = 180;
        }
        else if (looking_West)
        {
            target_X = 0;
            target_Z = transform.position.z;
            Y_rotation = -90;
        }
        
    }
}
