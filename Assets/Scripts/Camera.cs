using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public Transform targetObject;

    public float smoothFactor;

    private float target_X;
    private float target_Z;
    private float Y_rotation;

    private bool looking_North;
    private bool looking_East;
    private bool looking_South;
    private bool looking_West;

    public GameObject Pacman;

    private PacmanMove pacmanmovevariables;

    // Start is called before the first frame update
    void Start()
    {
        pacmanmovevariables = Pacman.GetComponent<PacmanMove>();

        //smoothFactor = 0.9f;
        target_X = transform.position.x;
        target_Z = transform.position.z;
        Y_rotation = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        looking_North = pacmanmovevariables.Get_looking_North();
        looking_South = pacmanmovevariables.Get_looking_South();
        looking_East = pacmanmovevariables.Get_looking_East();
        looking_West = pacmanmovevariables.Get_looking_West();

        Y_rotation = targetObject.transform.eulerAngles.y;

        if (looking_North) 
        {
            target_X = targetObject.transform.position.x;
            target_Z = targetObject.transform.position.z - 5;
        }
        else if (looking_South)
        {
            target_X = targetObject.transform.position.x;
            target_Z = targetObject.transform.position.z + 5;
        }
        else if (looking_East)
        {
            target_X = targetObject.transform.position.x - 5;
            target_Z = targetObject.transform.position.z;
        }
        else if (looking_West)
        {
            target_X = targetObject.transform.position.x + 5;
            target_Z = targetObject.transform.position.z;
        }

        Vector3 newPosition = new Vector3(target_X, 10, target_Z);
        transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * smoothFactor);

        Quaternion target = Quaternion.Euler(45, Y_rotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothFactor);
    }
}
