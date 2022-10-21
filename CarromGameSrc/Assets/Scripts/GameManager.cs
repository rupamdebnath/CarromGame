using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public TextMeshProUGUI text;

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

    public void FivePoints()
    {
        scoreValue += 5;
        text.SetText("Score: " + scoreValue);
    }
    public void TenPoints()
    {
        scoreValue += 10;
        text.SetText("Score: " + scoreValue);
    }
}
