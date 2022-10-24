using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSound : MonoBehaviour
{
    private void OnEnable()
    {
        SceneController.Instance.StopAllSounds();
        if (GameManager.Instance.winCondition())
        {
            SceneController.Instance.PlaySound(Sounds.GameWin);
        }
        else
        {
            SceneController.Instance.PlaySound(Sounds.GameLose);
        }
    }
}
