using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Singleton Game Manager to set respective score
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public TextMeshProUGUI scoretext;

    public TextMeshProUGUI gameOvertext;
    public GameObject gameOverBox;
    public GameObject powerBar;
    private int scoreValue;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Update()
    {
        if(scoreValue == 145)
        {
            GameOver(true);
        }
    }
    public void FivePoints()
    {
        scoreValue += 5;
        scoretext.SetText("Score: " + scoreValue);
    }
    public void TenPoints()
    {
        scoreValue += 10;
        scoretext.SetText("Score: " + scoreValue);
    }

    public void GameOver(bool win)
    {
        gameOverBox.SetActive(true);
        powerBar.SetActive(false);
        if (win)
            gameOvertext.SetText("Game Over!Congratulations You won!");
        else
            gameOvertext.SetText("Game Over!You have Lost!");
    }
}
