using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance { get { return instance; } }

    private int scoreValue;
    public List<AudioSource> SoundsList;
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
        DontDestroyOnLoad(this);
    }

    public void LoadCorrectScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void PlaySound(Sounds sound)
    {
        SoundsList[(int)(sound)].Play();
    }

    public void StopAllSounds()
    {
        for(int i = 0; i < SoundsList.Count; i++)
        {
            if(SoundsList[i].isPlaying)
                SoundsList[i].Stop();
        }
        
    }
}

public enum Sounds
{
    GameStart,
    BG,
    GameWin,
    GameLose,
    Striker
}
