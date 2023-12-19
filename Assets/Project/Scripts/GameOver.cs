using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text goText;
    public Text scoreText;


    public void Setup(string text, int score)
    {
        gameObject.SetActive(true);
        goText.text = text;
        scoreText.text = "Score: " + score;
    }


    public void NewGameButton()
    {
        SceneManager.LoadScene("SampleScene");
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
