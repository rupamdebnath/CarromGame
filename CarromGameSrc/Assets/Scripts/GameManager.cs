using System;
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
    bool winLoseSound;
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
        if(scoreValue >= 145)
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
        
        powerBar.SetActive(false);
        if (win)
        {
            gameOvertext.SetText("Game Over!Congratulations You won!");
            //SceneController.Instance.StopAllSounds();
            winLoseSound = true;
            //SceneController.Instance.PlaySound(Sounds.GameWin);
        }            
        else
        {
            gameOvertext.SetText("Game Over!You have Lost!");
            //SceneController.Instance.StopAllSounds();
            winLoseSound = false;
            //StartCoroutine(WaitForSoundStop());
            //SceneController.Instance.PlaySound(Sounds.GameLose);
        }
        gameOverBox.SetActive(true);
    }

    public bool winCondition()
    {
        return winLoseSound;
    }
}
