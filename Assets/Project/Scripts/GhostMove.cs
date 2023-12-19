using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GhostMove : MonoBehaviour
{
    private bool red;
    private bool pink;
    private bool blue;
    private bool orange;
    public float smoothFactor;
    private float rotationY;
    private bool startToMove;
    private float targetTime;
    public GameObject maze;
    public GameObject pacman;
    private bool ghostUp;
    private bool ghostLeft;
    private bool ghostDown;
    private bool ghostRight;
    private bool[] limitDirections;
    private bool[] newLimitDirections;
    public GameObject targetRed;
    public GameObject target;
    public ModeChange mode;
    private bool dead;
    public GameObject body;
    private int panicModeMaterial;
    private float panicModeTargetTime;
    private bool panicModeFinishGhosts;
    public bool gameGoOn;


    public void Start()
    {
        red = false;
        pink = false;
        blue = false;
        orange = false;
        startToMove = false;
        newLimitDirections = new bool[4] {false, false, false, false};

        if (gameObject.name.Contains("Red"))
        {
            red = true;

            ghostUp = true;
            ghostLeft = false;
            ghostDown = false;
            ghostRight = false;

            limitDirections = new bool[4] {false, true, true, true};
            targetTime = 1f;
        }
        else if (gameObject.name.Contains("Pink"))
        {
            pink = true;

            ghostUp = true;
            ghostLeft = false;
            ghostDown = false;
            ghostRight = false;

            limitDirections = new bool[4] {false, true, true, true};
            targetTime = 5f;
        }
        else if (gameObject.name.Contains("Blue"))
        {
            blue = true;

            ghostUp = false;
            ghostLeft = false;
            ghostDown = false;
            ghostRight = true;

            limitDirections = new bool[4] {true, true, true, false};
            targetTime = 9f;
        }
        else if (gameObject.name.Contains("Orange"))
        {
            orange = true;

            ghostUp = false;
            ghostLeft = true;
            ghostDown = false;
            ghostRight = false;

            limitDirections = new bool[4] {true, false, true, true};
            targetTime = 13f;
        }
        rotationY = transform.eulerAngles.y;

        dead = false;
        panicModeMaterial = 0;
        panicModeTargetTime = 0.25f;
        panicModeFinishGhosts = true;
        gameGoOn = true;
    }

    private bool UpLimits()
    {
        bool limit = false;

        if (!limit)
        {
            for (int i = 0; i < maze.transform.childCount; ++i)
            {
                GameObject w = maze.transform.GetChild(i).gameObject;

                if (Math.Round(transform.position.x) < w.transform.position.x + ((transform.localScale.x + w.transform.localScale.x) / 2)
                    && Math.Round(transform.position.x) > w.transform.position.x - ((transform.localScale.x + w.transform.localScale.x) / 2)
                    && Math.Round(transform.position.z) == w.transform.position.z - ((transform.localScale.z + w.transform.localScale.z) / 2)) 
                {
                    limit = true;
                }
            }
        }

        return limit;
    }

    private bool LeftLimits()
    {
        bool limit = false;

        if (red || pink || blue)
        {
            if (transform.position.x > 35 && transform.position.x < 61 &&
                transform.position.z > -5 && transform.position.z < 6 && !dead)
            {
                limit = true;
            }
        }
        else if (orange)
        {
            if (transform.position.x > 35 && transform.position.x < 50.1 &&
                transform.position.z > -5 && transform.position.z < 6 && !dead)
            {
                limit = true;
            }
        }

        if (!limit)
        {
            for (int i = 0; i < maze.transform.childCount; ++i)
            {
                GameObject w = maze.transform.GetChild(i).gameObject;

                if (Math.Round(transform.position.z) < w.transform.position.z + ((transform.localScale.z + w.transform.localScale.z) / 2)
                    && Math.Round(transform.position.z) > w.transform.position.z - ((transform.localScale.z + w.transform.localScale.z) / 2)
                    && Math.Round(transform.position.x) == w.transform.position.x + ((transform.localScale.x + w.transform.localScale.x) / 2)) 
                {
                    limit = true;
                }
            }
        }

        return limit;
    }

    private bool DownLimits()
    {
        bool limit = false;

        if (transform.position.x > 35 && transform.position.x < 61 &&
                transform.position.z > -5 && transform.position.z < 11 && !dead)
        {
            limit = true;
        }

        if (!limit)
        {
            for (int i = 0; i < maze.transform.childCount; ++i)
            {
                GameObject w = maze.transform.GetChild(i).gameObject;

                if (Math.Round(transform.position.x) < w.transform.position.x + ((transform.localScale.x + w.transform.localScale.x) / 2)
                    && Math.Round(transform.position.x) > w.transform.position.x - ((transform.localScale.x + w.transform.localScale.x) / 2)
                    && Math.Round(transform.position.z) == w.transform.position.z + ((transform.localScale.z + w.transform.localScale.z) / 2)) 
                {
                    limit = true;
                }
            }
        }

        return limit;
    }

    private bool RightLimits()
    {
        bool limit = false;

        if (red || pink || orange)
        {
            if (transform.position.x > 35 && transform.position.x < 61 &&
                transform.position.z > -5 && transform.position.z < 6 && !dead)
            {
                limit = true;
            }
        }
        else if (blue)
        {
            if (transform.position.x > 44.9 && transform.position.x < 61 &&
                transform.position.z > -5 && transform.position.z < 6 && !dead)
            {
                limit = true;
            }
        }

        if (!limit)
        {
            for (int i = 0; i < maze.transform.childCount; ++i)
            {
                GameObject w = maze.transform.GetChild(i).gameObject;

                if (Math.Round(transform.position.z) < w.transform.position.z + ((transform.localScale.z + w.transform.localScale.z) / 2)
                    && Math.Round(transform.position.z) > w.transform.position.z - ((transform.localScale.z + w.transform.localScale.z) / 2)
                    && Math.Round(transform.position.x) == w.transform.position.x - ((transform.localScale.x + w.transform.localScale.x) / 2)) 
                {
                    limit = true;
                }
            }
        }

        return limit;
    }

    public bool GhostLimitControl()
    {
        bool limit = false;

        if ((ghostUp && UpLimits()) ||
            (ghostLeft && LeftLimits()) ||
            (ghostDown && DownLimits()) ||
            (ghostRight && RightLimits()))
        {
            limit = true;
        } 

        return limit;
    }

    public bool GhostLimitControlDirection(string direction)
    {
        bool limit = false;

        if ((direction == "up" && UpLimits()) ||
            (direction == "left" && LeftLimits()) ||
            (direction == "down" && DownLimits()) ||
            (direction == "right" && RightLimits()))
        {
            limit = true;
        } 

        return limit;
    }

    public bool[] GhostDirectionControl()
    {
        bool [] limit = new bool[4] {false, false, false, false};

        if (UpLimits())
        {
            limit[0] = true;
        }
        if (LeftLimits())
        {
            limit[1] = true;
        }
        if (DownLimits())
        {
            limit[2] = true;
        }
        if (RightLimits())
        {
            limit[3] = true;
        }

        return limit;
    }

    private void GhostTarget()
    {
        ghostUp = false;
        ghostLeft = false;
        ghostDown = false;
        ghostRight = false;

        float targetX = 0;
        float targetZ = 0;

        float upMoveX = transform.position.x;
        float upMoveZ = transform.position.z + 1;
        float leftMoveX = transform.position.x - 1;
        float leftMoveZ = transform.position.z;
        float downMoveX = transform.position.x;
        float downMoveZ = transform.position.z - 1;
        float rightMoveX = transform.position.x + 1;
        float rightMoveZ = transform.position.z;

        if (mode.GetScatterMode())
        {
            if (red)
            {
                targetX = 90;
                targetZ = 30;
            }
            else if (pink)
            {
                targetX = 0;
                targetZ = 30;
            }
            else if (blue) 
            {
                targetX = 90;
                targetZ = -30;
            }
            else if (orange)
            {
                targetX = 0;
                targetZ = -30;
            }
        }
        else
        {
            if (red)
            {
                targetX = pacman.transform.position.x;
                targetZ = pacman.transform.position.z;
            }

            else if (pink)
            {
                if ((pacman.transform.eulerAngles.y > -2 && pacman.transform.eulerAngles.y < 2) ||
                    (pacman.transform.eulerAngles.y > -358 && pacman.transform.eulerAngles.y < 362))
                {
                    targetX = pacman.transform.position.x;
                    targetZ = pacman.transform.position.z + 10;
                }
                else if ((pacman.transform.eulerAngles.y > 268 && pacman.transform.eulerAngles.y < 272))
                {
                    targetX = pacman.transform.position.x - 10;
                    targetZ = pacman.transform.position.z;
                }
                else if ((pacman.transform.eulerAngles.y > 178 && pacman.transform.eulerAngles.y < 182) ||
                    (pacman.transform.eulerAngles.y > -181 && pacman.transform.eulerAngles.y < -178))
                {
                    targetX = pacman.transform.position.x;
                    targetZ = pacman.transform.position.z - 10;
                }
                else if ((pacman.transform.eulerAngles.y > 88 && pacman.transform.eulerAngles.y < 92))
                {
                    targetX = pacman.transform.position.x + 10;
                    targetZ = pacman.transform.position.z;
                }
            }
            else if (blue)
            {
                float centerX = 0;
                float centerZ = 0;

                if ((pacman.transform.eulerAngles.y > -2 && pacman.transform.eulerAngles.y < 2) ||
                    (pacman.transform.eulerAngles.y > -358 && pacman.transform.eulerAngles.y < 362))
                {
                    centerX = pacman.transform.position.x;
                    centerZ = pacman.transform.position.z + 5;
                }
                else if ((pacman.transform.eulerAngles.y > 268 && pacman.transform.eulerAngles.y < 272))
                {
                    centerX = pacman.transform.position.x - 5;
                    centerZ = pacman.transform.position.z;
                }
                else if ((pacman.transform.eulerAngles.y > 178 && pacman.transform.eulerAngles.y < 182) ||
                    (pacman.transform.eulerAngles.y > -181 && pacman.transform.eulerAngles.y < -178))
                {
                    centerX = pacman.transform.position.x;
                    centerZ = pacman.transform.position.z - 5;
                }
                else if ((pacman.transform.eulerAngles.y > 88 && pacman.transform.eulerAngles.y < 92))
                {
                    centerX = pacman.transform.position.x + 5;
                    centerZ = pacman.transform.position.z;
                }

                float redPointX = targetRed.transform.position.x;
                float redPointZ = targetRed.transform.position.z;

                targetX = centerX + (centerX - redPointX);
                targetZ = centerZ + (centerZ - redPointZ);
            }
            else if (orange)
            {
                double pacmanOrangeDistance = Math.Sqrt(Math.Pow((pacman.transform.position.x - transform.position.x), 2) + Math.Pow((pacman.transform.position.z - transform.position.z), 2));
                if (pacmanOrangeDistance > 20) // 20
                {
                    targetX = pacman.transform.position.x;
                    targetZ = pacman.transform.position.z;
                }
                else 
                {
                    targetX = 0;
                    targetZ = -30;
                }
            }
        }

        if (mode.GetRunningMode())
        {
            targetX = UnityEngine.Random.Range(0, 99);
            targetZ = UnityEngine.Random.Range(30, -30);
        }

        if (dead)
        {
            if(transform.position.x > 34.9f && transform.position.x < 60.1f
                && transform.position.z < 10.1f && transform.position.z > -0.1f)
                {
                    if (red)
                    {
                        targetX = 45;
                        targetZ = 0;
                    }
                    else if (pink)
                    {
                        targetX = 50;
                        targetZ = 0;
                    }
                    else if (blue)
                    {
                        targetX = 40;
                        targetZ = 0;
                    }
                    else if (orange)
                    {
                        targetX = 55;
                        targetZ = 0;
                    }
                }
                else 
                {
                    if (transform.position.x > 50)
                    {
                        targetX = 60;
                        targetZ = 10;
                    }
                    else 
                    {
                        targetX = 35;
                        targetZ = 10;
                    }
                }
        }

        target.transform.position = new Vector3(targetX, 7, targetZ);

        double upDistance = Math.Pow((targetX - upMoveX), 2) + Math.Pow((targetZ - upMoveZ), 2);
        double leftDistance = Math.Pow((targetX - leftMoveX), 2) + Math.Pow((targetZ - leftMoveZ), 2);
        double downDistance = Math.Pow((targetX - downMoveX), 2) + Math.Pow((targetZ - downMoveZ), 2);
        double rightDistance = Math.Pow((targetX - rightMoveX), 2) + Math.Pow((targetZ - rightMoveZ), 2);

        double[] distance = new double[4] {upDistance, leftDistance, downDistance, rightDistance};
        string[] direction = new string[4] {"up", "left", "down", "right"};

        for (int i = distance.Length - 1; i > -1; --i)
        {
            if (GhostLimitControlDirection(direction[i]))
            {
                List<double> deletedDistance = new List<double>(distance);
                deletedDistance.RemoveAt(i);
                distance = deletedDistance.ToArray();

                List<string> deletedDirection = new List<string>(direction);
                deletedDirection.RemoveAt(i);
                direction = deletedDirection.ToArray();
            }
        }

        double distanceTmp;
        string directionTmp;

        for (int i = 0; i < distance.Length - 1; ++i)
        {
            for (int j = 1; j < distance.Length; ++j)
            {
                if (distance[i] > distance[j])
                {
                    distanceTmp = distance[i];
                    distance[i] = distance[j];
                    distance[j] = distanceTmp;

                    directionTmp = direction[i];
                    direction[i] = direction[j];
                    direction[j] = directionTmp;
                }
            }
        }

        if (distance.Length > 3 && distance[0] == distance[3])
        {

            ghostUp = true;
        }
        else
        {
            if (distance.Length > 2 && distance[0] == distance[2])
            {
                if (distance.Length > 3)
                {
                    direction = direction.Take(direction.Count() - 1).ToArray();
                }

                if (direction.Contains("up"))
                {
                    ghostUp = true;
                }
                else 
                {
                    ghostLeft = true;
                }
            }
            else
            {
                if (distance.Length > 1 && distance[0] == distance[1])
                {
                    if (distance.Length > 3)
                    {
                        direction = direction.Take(direction.Count() - 2).ToArray();
                    }
                    else
                    {
                        if (distance.Length > 2)
                        {
                            direction = direction.Take(direction.Count() - 1).ToArray();
                        }
                    }

                    if (direction.Contains("up"))
                    {
                        ghostUp = true;
                    }
                    else 
                    {
                        if (direction.Contains("left"))
                        {
                            ghostLeft = true;
                        }
                        else 
                        {
                            ghostDown = true;
                        }
                    }
                }
                else
                {
                    if (direction[0] == "up")
                    {
                        ghostUp = true;
                    }
                    else if (direction[0] == "left")
                    {
                        ghostLeft = true;
                    }
                    else if (direction[0] == "down")
                    {
                        ghostDown = true;
                    }
                    else if (direction[0] == "right")
                    {
                        ghostRight = true;
                    }
                }
            }
        }
    }


    private int Converted(float number)
    {
        int converted = Convert.ToInt32(number);

        if ((converted + 1) % 5 == 0)
        {
            converted = converted + 1;
        }
        else if ((converted - 1) % 5 == 0)
        {
            converted = converted - 1;
        }

        return converted;
    }


    public void SetRunningModeFinishGhosts(bool value)
    {
        panicModeFinishGhosts = value;
    }

    public void SetDead(bool value)
    {
        dead = value;
    }

    public bool GetDead()
    {
        return dead;
    }

    void Update()
    {
        if (dead)
        {
            Material newMat = Resources.Load("Dead", typeof(Material)) as Material;
            GetComponent<Renderer>().material = body.GetComponent<Renderer>().material = newMat;
            smoothFactor = 0.09f;

            if ((red && (transform.position.x > 44 && transform.position.x < 46 && 
                transform.position.z < 0.01f && transform.position.z > -0.1f)) ||
                (pink && (transform.position.x > 49 && transform.position.x < 51 && 
                transform.position.z < 0.01f && transform.position.z > -0.1f)) ||
                (blue && (transform.position.x > 39 && transform.position.x < 41 && 
                transform.position.z < 0.01f && transform.position.z > -0.1f)) ||
                (orange && (transform.position.x > 54 && transform.position.x < 56 && 
                transform.position.z < 0.01f && transform.position.z > -0.1f)))
            {
                dead = false;

                if (red)
                {
                    transform.position = new Vector3(45, 2.5f, 0);
                }
                else if (pink)
                {
                    transform.position = new Vector3(50, 2.5f, 0);
                }
                else if (blue)
                {
                    transform.position = new Vector3(40, 2.5f, 0);
                }
                else if (orange)
                {
                    transform.position = new Vector3(55, 2.5f, 0);
                }

                transform.rotation = Quaternion.Euler(0, 0, 0);
                panicModeFinishGhosts = true;
            }
        }
        else 
        {
            if (mode.GetRunningModeFinish() && !panicModeFinishGhosts)
            {
                panicModeTargetTime -= Time.deltaTime;

                if (panicModeTargetTime <= 0.0f)
                {
                    panicModeMaterial++;
                    panicModeTargetTime = 0.25f;
                }

                if (panicModeMaterial == 1)
                {
                    Material newMat = Resources.Load("RunningBlue", typeof(Material)) as Material;
                    GetComponent<Renderer>().material = body.GetComponent<Renderer>().material = newMat;
                }
                else if (panicModeMaterial == 2)
                {
                    Material newMat = Resources.Load("RunningWhite", typeof(Material)) as Material;
                    GetComponent<Renderer>().material = body.GetComponent<Renderer>().material = newMat;
                    panicModeMaterial = 0;
                }
            }
            else 
            {
                if (red)
                {
                    Material newMat = Resources.Load("RedGhost", typeof(Material)) as Material;
                    GetComponent<Renderer>().material = body.GetComponent<Renderer>().material = newMat;
                }
                else if (pink)
                {
                    Material newMat = Resources.Load("PinkGhost", typeof(Material)) as Material;
                    GetComponent<Renderer>().material = body.GetComponent<Renderer>().material = newMat;
                }
                else if (blue)
                {
                    Material newMat = Resources.Load("BlueGhost", typeof(Material)) as Material;
                    GetComponent<Renderer>().material = body.GetComponent<Renderer>().material = newMat;
                }
                else if (orange)
                {
                    Material newMat = Resources.Load("OrangeGhost", typeof(Material)) as Material;
                    GetComponent<Renderer>().material = body.GetComponent<Renderer>().material = newMat;
                }
            }
            smoothFactor = 0.03f;
        }

        if (startToMove && gameGoOn) 
        {
            if (ghostUp && !GhostLimitControl())
            {
                transform.position = new Vector3(Converted(transform.position.x), 3, transform.position.z + smoothFactor);
                rotationY = 0;
            }
            if (ghostLeft && !GhostLimitControl())
            {
                transform.position = new Vector3(transform.position.x - smoothFactor, 3, Converted(transform.position.z));
                rotationY = -90;
            }
            if (ghostDown && !GhostLimitControl())
            {
                transform.position = new Vector3(Converted(transform.position.x), 3, transform.position.z - smoothFactor);
                rotationY = 180;
            }
            if (ghostRight && !GhostLimitControl())
            {
                transform.position = new Vector3(transform.position.x + smoothFactor, 3, Converted(transform.position.z));
                rotationY = 90;
            }

            if (transform.position.y != 3.5f)
            {
                transform.position = new Vector3(transform.position.x, 3.5f, transform.position.z);
            }
            transform.rotation = Quaternion.Euler(0, rotationY, 0);

            newLimitDirections = GhostDirectionControl();

            if (!Enumerable.SequenceEqual(limitDirections, newLimitDirections)
                && Convert.ToInt32(transform.position.x) % 5 == 0
                && Convert.ToInt32(transform.position.z) % 5 == 0)
            {
                GhostTarget();
                limitDirections = newLimitDirections;
            }
        }

        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            if (!startToMove)
            {
                startToMove = true;
            }
        }
    }
}
