using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBalls : MonoBehaviour
{
    private GameObject ball;
    public GameObject parent;

    void Start()
    {
        for (int i = 0; i < 9; ++i)
        {
            for (int j = 0; j < 18; ++j)
            {
                if
                ((j == 0 || j == 5 || j == 12 || j == 17) ||
                ((j == 1 || j == 16) && (i == 0 || i == 4 || i == 8)) ||
                ((j == 2 || j == 15) && i != 1 && i != 7) ||
                ((j == 3 || j == 14) && i != 3 && i != 5) ||
                ((j == 4 || j == 13) && (i == 2 || i == 4 || i == 6)) ||
                ((j == 6 || j == 7 || j == 8 || j == 9 || j == 10 || j ==11)
                && (i == 0 || i == 2 || i == 6 || i == 8)))
                {
                    ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    ball.gameObject.tag = "Ball";
                    ball.transform.position = new Vector3(5 + (j * 5), 3, 20 - (i * 5));

                    Material newMat = Resources.Load("Ball", typeof(Material)) as Material;
                    ball.GetComponent<Renderer>().material = newMat;

                    if ((i == 0 || i == 8) && (j == 0 || j == 17))
                    {
                        ball.gameObject.tag = "SuperBall";
                        ball.transform.localScale = new Vector3(2, 2, 2);
                        newMat = Resources.Load("SuperBall", typeof(Material)) as Material;
                        ball.GetComponent<Renderer>().material = newMat;
                    }

                    ball.GetComponent<SphereCollider>().isTrigger = true;
                    ball.transform.parent = parent.transform;
                }
            }
        }
    }

    void Update()
    {
        
    }
}
