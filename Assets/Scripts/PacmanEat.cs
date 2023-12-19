using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacmanEat : MonoBehaviour
{

    public ModeChange mode;
    public GhostMove red;
    public GhostMove pink;
    public GhostMove blue;
    public GhostMove orange;
    public PacmanMove pacman;
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;
    private int catchCount;
    private bool gameGoOn;
    private Vector3 scaleChange;
    public GameOver gameOver;
    private int score;
    private int panicGhostScore;
    public Text pointText;
    private int baitcount;
    public GameObject balls;
    private float rotationY;


    public void Start()
    {
        catchCount = 0;
        gameGoOn = true;
        scaleChange = new Vector3(-0.008f, -0.008f, -0.008f);
        score = 0;
        panicGhostScore = 200;
    }


    void Update()
    {
        rotationY = pacman.GetComponent<PacmanMove>().GetRotationY();
        
        if(!gameGoOn)
        {
            pacman.transform.localScale += scaleChange;
            pacman.transform.rotation = Quaternion.Euler(0, rotationY, 0);

            if(pacman.transform.localScale.x < 0)
            {
                gameGoOn = true;
                pacman.transform.position = new Vector3(45, 2.5f, -20);
                red.transform.position = new Vector3(45.5f, 3, 0);
                pink.transform.position = new Vector3(50.5f, 3, 0);
                blue.transform.position = new Vector3(40.5f, 3, 0);
                orange.transform.position = new Vector3(55.5f, 3, 0);

                pacman.transform.rotation = Quaternion.Euler(0, 0, 0);
                red.transform.rotation = Quaternion.Euler(0, 0, 0);
                pink.transform.rotation = Quaternion.Euler(0, 0, 0);
                blue.transform.rotation = Quaternion.Euler(0, 0, 0);
                orange.transform.rotation = Quaternion.Euler(0, 0, 0);

                while (pacman.transform.localScale.x < 5)
                {
                    pacman.transform.localScale -= scaleChange;
                }
                pacman.transform.localScale = new Vector3(5, 5, 5);

                red.GetComponent<GhostMove>().enabled = true;
                pink.GetComponent<GhostMove>().enabled = true;
                blue.GetComponent<GhostMove>().enabled = true;
                orange.GetComponent<GhostMove>().enabled = true;
                pacman.GetComponent<PacmanMove>().enabled = true;

                red.Start();
                pink.Start();
                blue.Start();
                orange.Start();
                pacman.Start();
            }
        }

        pointText.text = "Score: " + score;
        if (!mode.GetRunningMode())
        {
            panicGhostScore = 200;
        }

        baitcount = balls.transform.childCount;

        if (baitcount == 0)
        {
            GameOverScreen(true);
            red.GetComponent<GhostMove>().enabled = false;
            pink.GetComponent<GhostMove>().enabled = false;
            blue.GetComponent<GhostMove>().enabled = false;
            orange.GetComponent<GhostMove>().enabled = false;
            pacman.GetComponent<PacmanMove>().enabled = false;
        }
    }


    private void GameOverScreen(bool gamewin)
    {
        if (gamewin)
        {
            gameOver.Setup("CONGRATULATIONS", score);
        }
        else
        {
            gameOver.Setup("GAME OVER", score);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Destroy(other.gameObject);
            score += 10;
        }

        if (other.gameObject.tag == "SuperBall")
        {
            Destroy(other.gameObject); 

            mode.SetRunningMode(true);
            mode.SetRunningModeFinish(true);
            mode.SetIntervalTargetTime(0.25f);

            red.SetRunningModeFinishGhosts(false);
            pink.SetRunningModeFinishGhosts(false);
            blue.SetRunningModeFinishGhosts(false);
            orange.SetRunningModeFinishGhosts(false);

            score += 50;
        }

        if (other.gameObject.tag == "Ghost")
        {
            if (mode.GetRunningMode())
            {
                if(other.gameObject.name.Contains("Red"))
                {
                    if (!red.GetDead())
                    {
                        red.SetDead(true);
                        score += panicGhostScore;
                        panicGhostScore = panicGhostScore * 2;
                    }
                }
                if(other.gameObject.name.Contains("Pink"))
                {
                    if (!pink.GetDead())
                    {
                        pink.SetDead(true);
                        score += panicGhostScore;
                        panicGhostScore = panicGhostScore * 2;
                    }
                }
                if(other.gameObject.name.Contains("Blue"))
                {
                    if (!blue.GetDead())
                    {
                        blue.SetDead(true);
                        score += panicGhostScore;
                        panicGhostScore = panicGhostScore * 2;
                    }
                }
                if(other.gameObject.name.Contains("Orange"))
                {
                    if (!orange.GetDead())
                    {
                        orange.SetDead(true);
                        score += panicGhostScore;
                        panicGhostScore = panicGhostScore * 2;
                    }
                }
            }
            else 
            {
                if ((other.gameObject.name.Contains("Red") && !red.GetDead()) || 
                    (other.gameObject.name.Contains("Pink") && !pink.GetDead()) ||
                    (other.gameObject.name.Contains("Orange") && !orange.GetDead()) ||
                    (other.gameObject.name.Contains("Blue") && !blue.GetDead()))

                {
                    if (gameGoOn)
                    {
                        catchCount++;
                    }

                    if (catchCount == 1 && gameGoOn)
                    {
                        hp3.SetActive(false);
                        gameGoOn = false;
                    }

                    if (catchCount == 2 && gameGoOn)
                    {
                        hp2.SetActive(false);
                        gameGoOn = false;
                    }

                    if (catchCount == 3 && gameGoOn)
                    {
                        hp1.SetActive(false);
                        gameGoOn = false;
                        GameOverScreen(false);
                    }

                    red.GetComponent<GhostMove>().enabled = false;
                    pink.GetComponent<GhostMove>().enabled = false;
                    blue.GetComponent<GhostMove>().enabled = false;
                    orange.GetComponent<GhostMove>().enabled = false;
                    pacman.GetComponent<PacmanMove>().enabled = false;
                }
            }
        }
    }
}
