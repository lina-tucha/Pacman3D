using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform targetObject;
    public float smoothFactor;
    private float targetX;
    private float targetZ;
    private float rotationY;
    private bool lookingNorth;
    private bool lookingEast;
    private bool lookingSouth;
    private bool lookingWest;
    public GameObject pacman;
    private PacmanMove pacmanMoveVariables;


    void Start()
    {
        pacmanMoveVariables = pacman.GetComponent<PacmanMove>();

        //smoothFactor = 0.9f;
        targetX = transform.position.x;
        targetZ = transform.position.z;
        rotationY = transform.eulerAngles.y;
    }


    void LateUpdate()
    {
        lookingNorth = pacmanMoveVariables.GetLookingNorth();
        lookingSouth = pacmanMoveVariables.GetLookingSouth();
        lookingEast = pacmanMoveVariables.GetLookingEast();
        lookingWest = pacmanMoveVariables.GetLookingWest();

        rotationY = targetObject.transform.eulerAngles.y;

        if (lookingNorth) 
        {
            targetX = targetObject.transform.position.x;
            targetZ = targetObject.transform.position.z - 5;
        }
        else if (lookingSouth)
        {
            targetX = targetObject.transform.position.x;
            targetZ = targetObject.transform.position.z + 5;
        }
        else if (lookingEast)
        {
            targetX = targetObject.transform.position.x - 5;
            targetZ = targetObject.transform.position.z;
        }
        else if (lookingWest)
        {
            targetX = targetObject.transform.position.x + 5;
            targetZ = targetObject.transform.position.z;
        }
        else
        {
            targetX = targetObject.transform.position.x;
            targetZ = targetObject.transform.position.z - 5;
        }

        Vector3 newPosition = new Vector3(targetX, 10, targetZ);
        transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * smoothFactor);

        Quaternion target = Quaternion.Euler(45, rotationY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothFactor);
    }
}
