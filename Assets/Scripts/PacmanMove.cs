using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    public float smoothFactor;
    private float targetX;
    private float targetZ;
    private float rotationY;
    private bool lookingNorth;
    private bool lookingEast;
    private bool lookingSouth;
    private bool lookingWest;
    private bool timeToDo;
    private float targetTime;


    public void Start()
    {
        targetX = transform.position.x;
        targetZ = transform.position.z;
        rotationY = transform.eulerAngles.y;

        lookingNorth = false;
        lookingEast = false;
        lookingSouth = false;
        lookingWest = false;

        timeToDo = true;
        targetTime = 0.5f;
    }


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            if (lookingNorth)
            {
                LookingTarget(ref lookingNorth, ref lookingEast, ref lookingSouth, ref lookingWest);
            }
            else if (lookingEast)
            {
                LookingTarget(ref lookingEast, ref lookingNorth, ref lookingSouth, ref lookingWest);
            }
            else if (lookingSouth)
            {
                LookingTarget(ref lookingSouth, ref lookingEast, ref lookingNorth, ref lookingWest);
            }
            else if (lookingWest)
            {
                LookingTarget(ref lookingWest, ref lookingEast, ref lookingSouth, ref lookingNorth);
            }
            else
            {
                LookingTarget(ref lookingNorth, ref lookingEast, ref lookingSouth, ref lookingWest);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            if (lookingNorth)
            {
                LookingTarget(ref lookingEast, ref lookingNorth, ref lookingSouth, ref lookingWest);
            }
            else if (lookingEast)
            {
                LookingTarget(ref lookingSouth, ref lookingNorth, ref lookingEast, ref lookingWest);
            }
            else if (lookingSouth)
            {
                LookingTarget(ref lookingWest, ref lookingEast, ref lookingNorth, ref lookingSouth);
            }
            else if (lookingWest)
            {
                LookingTarget(ref lookingNorth, ref lookingEast, ref lookingSouth, ref lookingWest);
            }
            else
            {
                LookingTarget(ref lookingEast, ref lookingNorth, ref lookingSouth, ref lookingWest);
            }
        }

        if (Input.GetKey(KeyCode.DownArrow)) 
        {
            if (lookingNorth)
            {
                LookingTarget(ref lookingSouth, ref lookingEast, ref lookingNorth, ref lookingWest);
            }
            else if (lookingEast)
            {
                LookingTarget(ref lookingWest, ref lookingNorth, ref lookingSouth, ref lookingEast);
            }
            else if (lookingSouth)
            {
                LookingTarget(ref lookingNorth, ref lookingEast, ref lookingSouth, ref lookingWest);
            }
            else if (lookingWest)
            {
                LookingTarget(ref lookingEast, ref lookingWest, ref lookingSouth, ref lookingNorth);
            }
            else
            {
                LookingTarget(ref lookingSouth, ref lookingEast, ref lookingNorth, ref lookingWest);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            if (lookingNorth)
            {
                LookingTarget(ref lookingWest, ref lookingNorth, ref lookingSouth, ref lookingEast);
            }
            else if (lookingEast)
            {
                LookingTarget(ref lookingNorth, ref lookingSouth, ref lookingEast, ref lookingWest);
            }
            else if (lookingSouth)
            {
                LookingTarget(ref lookingEast, ref lookingWest, ref lookingNorth, ref lookingSouth);
            }
            else if (lookingWest)
            {
                LookingTarget(ref lookingSouth, ref lookingEast, ref lookingNorth, ref lookingWest);
            }
            else
            {
                LookingTarget(ref lookingWest, ref lookingNorth, ref lookingSouth, ref lookingEast);
            }
        }

        if (transform.position.y != 2.5f)
        {
            transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
        }

        SetTarget();

        Vector3 toLocation = new Vector3(targetX, 2.5f, targetZ);
        transform.position = Vector3.MoveTowards(transform.position, toLocation, smoothFactor);
        transform.rotation = Quaternion.Euler(0, rotationY, 0);

        targetTime -= Time.deltaTime;

        if(targetTime <= 0.0f)
        {
            if (!timeToDo)
            {
                timeToDo = true;
                targetTime = 0.5f;
            }
        }
    }

    void LookingTarget(ref bool looking, ref bool notlooking1, ref bool notlooking2, ref bool notlooking3) 
    {
        if (timeToDo) {
            notlooking1 = notlooking2 = notlooking3 = false;
            looking = true;
            timeToDo = false;
            targetTime = 0.5f;
        }
    }

    void SetTarget()
    {
        if (lookingNorth)
        {
            targetX = transform.position.x;
            targetZ = 25;
            rotationY = 0;
        }
        else if (lookingEast)
        {
            targetX = 100;
            targetZ = transform.position.z;
            rotationY = 90;
        }
        else if (lookingSouth)
        {
            targetX = transform.position.x;
            targetZ = -25;
            rotationY = 180;
        }
        else if (lookingWest)
        {
            targetX = 0;
            targetZ = transform.position.z;
            rotationY = -90;
        }
        
    }

    public bool GetLookingNorth() 
    {
        return lookingNorth;
    }

    public bool GetLookingSouth() 
    {
        return lookingSouth;
    }

    public bool GetLookingEast() 
    {
        return lookingEast;
    }

    public bool GetLookingWest() 
    {
        return lookingWest;
    }

    public float GetRotationY() 
    {
        return rotationY;
    }
}
