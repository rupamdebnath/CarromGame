using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    Button thisButton;
    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(ClickFunction);
    }

    void ClickFunction()
    {
        SceneController.Instance.LoadCorrectScene(1);
    }
}
