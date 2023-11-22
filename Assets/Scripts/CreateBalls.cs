using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBalls : MonoBehaviour
{
    private GameObject ball;

    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; ++i)
        {
            for (int j = 0; j < 18; ++j)
            {
                if (!((i == 0 && j == 8) || (i == 3 && (j == 8  || j == 9)) || (i == 4 && (j >= 7 && j <= 10))))
                {
                    ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    ball.gameObject.tag = "Ball";
                    ball.transform.position = new Vector3(5 + (j * 5), 3, 20 - (i * 5));

                    Material newMat = Resources.Load("Ball", typeof(Material)) as Material;
                    ball.GetComponent<Renderer>().material = newMat;

                    if ((i == 0 || i == 8) && (j == 0 || j == 17))
                    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
