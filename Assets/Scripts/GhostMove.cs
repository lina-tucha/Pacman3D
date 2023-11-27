using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GhostMove : MonoBehaviour
{
    private bool Red;
    private bool Pink;
    private bool Blue;
    private bool Orange;

    public float smoothFactor;
    private float Y_rotation;
    private bool start_to_move;
    private float targetTime;

    public GameObject Walls; // Maze
    public GameObject Pacman;

    private bool Ghost_Up;
    private bool Ghost_Left;
    private bool Ghost_Down;
    private bool Ghost_Right;

    private bool[] Limit_Directions;
    private bool[] New_Limit_Directions;

    public GameObject Blinky;
    public GameObject Target_;

    public ModeChange mode;

    // Start is called before the first frame update
    void Start()
    {
        Red = false;
        Pink = false;
        Blue = false;
        Orange = false;
        start_to_move = false;
        New_Limit_Directions = new bool[4] {false, false, false, false};

        if (gameObject.name.Contains("Red"))
        {
            Red = true;

            Ghost_Up = true;
            Ghost_Left = false;
            Ghost_Down = false;
            Ghost_Right = false;

            Limit_Directions = new bool[4] {false, true, true, true};
            targetTime = 1f;
        }
        else if (gameObject.name.Contains("Pink"))
        {
            Pink = true;

            Ghost_Up = true;
            Ghost_Left = false;
            Ghost_Down = false;
            Ghost_Right = false;

            Limit_Directions = new bool[4] {false, true, true, true};
            targetTime = 5f;
        }
        else if (gameObject.name.Contains("Blue"))
        {
            Blue = true;

            Ghost_Up = false;
            Ghost_Left = false;
            Ghost_Down = false;
            Ghost_Right = true;

            Limit_Directions = new bool[4] {true, true, true, false};
            targetTime = 9f;
        }
        else if (gameObject.name.Contains("Orange"))
        {
            Orange = true;

            Ghost_Up = false;
            Ghost_Left = true;
            Ghost_Down = false;
            Ghost_Right = false;

            Limit_Directions = new bool[4] {true, false, true, true};
            targetTime = 13f;
        }
        Y_rotation = transform.eulerAngles.y;

    }


    private bool Up_Limits()
    {
        bool limit = false;

        if (!limit)
        {
            for (int i = 0; i < Walls.transform.childCount; ++i)
            {
                GameObject w = Walls.transform.GetChild(i).gameObject;

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

    private bool Left_Limits()
    {
        bool limit = false;

        if (Red || Pink || Blue)
        {
            if (transform.position.x > 35 && transform.position.x < 61 &&
                transform.position.z > -5 && transform.position.z < 6)
            {
                limit = true;
            }
        }
        else if (Orange)
        {
            if (transform.position.x > 35 && transform.position.x < 50.1 &&
                transform.position.z > -5 && transform.position.z < 6)
            {
                limit = true;
            }
        }

        if (!limit)
        {
            for (int i = 0; i < Walls.transform.childCount; ++i)
            {
                GameObject w = Walls.transform.GetChild(i).gameObject;

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

    private bool Down_Limits()
    {
        bool limit = false;

        if (transform.position.x > 35 && transform.position.x < 61 &&
                transform.position.z > -5 && transform.position.z < 11)
        {
            limit = true;
        }

        if (!limit)
        {
            for (int i = 0; i < Walls.transform.childCount; ++i)
            {
                GameObject w = Walls.transform.GetChild(i).gameObject;

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

    private bool Right_Limits()
    {
        bool limit = false;

        if (Red || Pink || Orange)
        {
            if (transform.position.x > 35 && transform.position.x < 61 &&
                transform.position.z > -5 && transform.position.z < 6)
            {
                limit = true;
            }
        }
        else if (Blue)
        {
            if (transform.position.x > 44.9 && transform.position.x < 61 &&
                transform.position.z > -5 && transform.position.z < 6)
            {
                limit = true;
            }
        }

        if (!limit)
        {
            for (int i = 0; i < Walls.transform.childCount; ++i)
            {
                GameObject w = Walls.transform.GetChild(i).gameObject;

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

    public bool Ghost_Limitcontrol()
    {
        bool limit = false;

        if ((Ghost_Up && Up_Limits()) ||
            (Ghost_Left && Left_Limits()) ||
            (Ghost_Down && Down_Limits()) ||
            (Ghost_Right && Right_Limits()))
        {
            limit = true;
        } 

        return limit;
    }

    public bool Ghost_Limitcontrol_Direction(string direction)
    {
        bool limit = false;

        if ((direction == "up" && Up_Limits()) ||
            (direction == "left" && Left_Limits()) ||
            (direction == "down" && Down_Limits()) ||
            (direction == "right" && Right_Limits()))
        {
            limit = true;
        } 

        return limit;
    }

    public bool[] Ghost_Directioncontrol()
    {
        bool [] limit = new bool[4] {false, false, false, false};

        if (Up_Limits())
        {
            limit[0] = true;
        }
        if (Left_Limits())
        {
            limit[1] = true;
        }
        if (Down_Limits())
        {
            limit[2] = true;
        }
        if (Right_Limits())
        {
            limit[3] = true;
        }

        return limit;
    }

    private void Ghost_Target()
    {
        Ghost_Up = false;
        Ghost_Left = false;
        Ghost_Down = false;
        Ghost_Right = false;

        float target_X = 0;
        float target_Z = 0;

        float up_move_X = transform.position.x;
        float up_move_Z = transform.position.z + 1;
        float left_move_X = transform.position.x - 1;
        float left_move_Z = transform.position.z;
        float down_move_X = transform.position.x;
        float down_move_Z = transform.position.z - 1;
        float right_move_X = transform.position.x + 1;
        float right_move_Z = transform.position.z;

        if (mode.GetScatterMode())
        {
            if (Red)
            {
                target_X = 90;
                target_Z = 30;
            }
            else if (Pink)
            {
                target_X = 0;
                target_Z = 30;
            }
            else if (Blue) 
            {
                target_X = 90;
                target_Z = -30;
            }
            else if (Orange)
            {
                target_X = 0;
                target_Z = -30;
            }
        }
        else
        {
            if (Red)
            {
                target_X = Pacman.transform.position.x;
                target_Z = Pacman.transform.position.z;
            }

            else if (Pink)
            {
                if ((Pacman.transform.eulerAngles.y > -2 && Pacman.transform.eulerAngles.y < 2) ||
                    (Pacman.transform.eulerAngles.y > -358 && Pacman.transform.eulerAngles.y < 362))
                {
                    target_X = Pacman.transform.position.x;
                    target_Z = Pacman.transform.position.z + 10;
                }
                else if ((Pacman.transform.eulerAngles.y > 268 && Pacman.transform.eulerAngles.y < 272))
                {
                    target_X = Pacman.transform.position.x - 10;
                    target_Z = Pacman.transform.position.z;
                }
                else if ((Pacman.transform.eulerAngles.y > 178 && Pacman.transform.eulerAngles.y < 182) ||
                    (Pacman.transform.eulerAngles.y > -181 && Pacman.transform.eulerAngles.y < -178))
                {
                    target_X = Pacman.transform.position.x;
                    target_Z = Pacman.transform.position.z - 10;
                }
                else if ((Pacman.transform.eulerAngles.y > 88 && Pacman.transform.eulerAngles.y < 92))
                {
                    target_X = Pacman.transform.position.x + 10;
                    target_Z = Pacman.transform.position.z;
                }
            }
            else if (Blue)
            {
                float center_X = 0;
                float center_Z = 0;

                if ((Pacman.transform.eulerAngles.y > -2 && Pacman.transform.eulerAngles.y < 2) ||
                    (Pacman.transform.eulerAngles.y > -358 && Pacman.transform.eulerAngles.y < 362))
                {
                    center_X = Pacman.transform.position.x;
                    center_Z = Pacman.transform.position.z + 5;
                }
                else if ((Pacman.transform.eulerAngles.y > 268 && Pacman.transform.eulerAngles.y < 272))
                {
                    center_X = Pacman.transform.position.x - 5;
                    center_Z = Pacman.transform.position.z;
                }
                else if ((Pacman.transform.eulerAngles.y > 178 && Pacman.transform.eulerAngles.y < 182) ||
                    (Pacman.transform.eulerAngles.y > -181 && Pacman.transform.eulerAngles.y < -178))
                {
                    center_X = Pacman.transform.position.x;
                    center_Z = Pacman.transform.position.z - 5;
                }
                else if ((Pacman.transform.eulerAngles.y > 88 && Pacman.transform.eulerAngles.y < 92))
                {
                    center_X = Pacman.transform.position.x + 5;
                    center_Z = Pacman.transform.position.z;
                }

                float red_point_X = Blinky.transform.position.x;
                float red_point_Z = Blinky.transform.position.z;

                target_X = center_X + (center_X - red_point_X);
                target_Z = center_Z + (center_Z - red_point_Z);
            }
            else if (Orange)
            {
                double pacman_orange_distance = Math.Sqrt(Math.Pow((Pacman.transform.position.x - transform.position.x), 2) + Math.Pow((Pacman.transform.position.z - transform.position.z), 2));
                if (pacman_orange_distance > 20)
                {
                    target_X = Pacman.transform.position.x;
                    target_Z = Pacman.transform.position.z;
                }
                else 
                {
                    target_X = 0;
                    target_Z = -30;
                }
            }
        }

        Target_.transform.position = new Vector3(target_X, 7, target_Z);

        double up_distance = Math.Pow((target_X - up_move_X), 2) + Math.Pow((target_Z - up_move_Z), 2);
        double left_distance = Math.Pow((target_X - left_move_X), 2) + Math.Pow((target_Z - left_move_Z), 2);
        double down_distance = Math.Pow((target_X - down_move_X), 2) + Math.Pow((target_Z - down_move_Z), 2);
        double right_distance = Math.Pow((target_X - right_move_X), 2) + Math.Pow((target_Z - right_move_Z), 2);

        double[] distance = new double[4] {up_distance, left_distance, down_distance, right_distance};
        string[] direction = new string[4] {"up", "left", "down", "right"};

        for (int i = distance.Length - 1; i > -1; --i)
        {
            if (Ghost_Limitcontrol_Direction(direction[i]))
            {
                List<double> deleted_distance = new List<double>(distance);
                deleted_distance.RemoveAt(i);
                distance = deleted_distance.ToArray();

                List<string> deleted_direction = new List<string>(direction);
                deleted_direction.RemoveAt(i);
                direction = deleted_direction.ToArray();
            }
        }

        double distance_tmp;
        string direction_tmp;

        for (int i = 0; i < distance.Length - 1; ++i)
        {
            for (int j = 1; j < distance.Length; ++j)
            {
                if (distance[i] > distance[j])
                {
                    distance_tmp = distance[i];
                    distance[i] = distance[j];
                    distance[j] = distance_tmp;

                    direction_tmp = direction[i];
                    direction[i] = direction[j];
                    direction[j] = direction_tmp;
                }
            }
        }

        if (distance.Length > 3 && distance[0] == distance[3])
        {

            Ghost_Up = true;
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
                    Ghost_Up = true;
                }
                else 
                {
                    Ghost_Left = true;
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
                        Ghost_Up = true;
                    }
                    else 
                    {
                        if (direction.Contains("left"))
                        {
                            Ghost_Left = true;
                        }
                        else 
                        {
                            Ghost_Down = true;
                        }
                    }
                }
                else
                {
                    if (direction[0] == "up")
                    {
                        Ghost_Up = true;
                    }
                    else if (direction[0] == "left")
                    {
                        Ghost_Left = true;
                    }
                    else if (direction[0] == "down")
                    {
                        Ghost_Down = true;
                    }
                    else if (direction[0] == "right")
                    {
                        Ghost_Right = true;
                    }
                }
            }
        }
    }


    private int Convert_2_5_division(float number)
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


    // Update is called once per frame
    void Update()
    {
        if (start_to_move) 
        {
            if (Ghost_Up && !Ghost_Limitcontrol())
            {
                transform.position = new Vector3(Convert_2_5_division(transform.position.x), 3, transform.position.z + smoothFactor);
                Y_rotation = 0;
            }
            if (Ghost_Left && !Ghost_Limitcontrol())
            {
                transform.position = new Vector3(transform.position.x - smoothFactor, 3, Convert_2_5_division(transform.position.z));
                Y_rotation = -90;
            }
            if (Ghost_Down && !Ghost_Limitcontrol())
            {
                transform.position = new Vector3(Convert_2_5_division(transform.position.x), 3, transform.position.z - smoothFactor);
                Y_rotation = 180;
            }
            if (Ghost_Right && !Ghost_Limitcontrol())
            {
                transform.position = new Vector3(transform.position.x + smoothFactor, 3, Convert_2_5_division(transform.position.z));
                Y_rotation = 90;
            }

            if (transform.position.y != 3.5f)
            {
                transform.position = new Vector3(transform.position.x, 3.5f, transform.position.z);
            }
            transform.rotation = Quaternion.Euler(0, Y_rotation, 0);

            New_Limit_Directions = Ghost_Directioncontrol();

            if (!Enumerable.SequenceEqual(Limit_Directions, New_Limit_Directions)
                && Convert.ToInt32(transform.position.x) % 5 == 0
                && Convert.ToInt32(transform.position.z) % 5 == 0)
            {
                Ghost_Target();
                Limit_Directions = New_Limit_Directions;
            }
        }

        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            if (!start_to_move)
            {
                start_to_move = true;
            }
        }
    }
}
