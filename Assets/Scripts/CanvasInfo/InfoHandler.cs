using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHandler : MonoBehaviour
{
    public static InfoHandler infoHandler { get; private set; }

    public TMPro.TMP_Text title, subtitle;
    public GameObject Info;

    private void Awake()
    {
        infoHandler = this;
    }

    public void ShowIntro()
    {
        title.text = GameConfiguration.titleCanvas;
        subtitle.text = GameConfiguration.subtitleCanvas;
        Info.SetActive(true);
    }

    public void ShowFinish(int score)
    {
        title.text = GameConfiguration.finishCanvas;
        subtitle.text = GameConfiguration.subFinishCanvas + score + GameConfiguration.subFinishCanvasEnd;
        Info.SetActive(true);
    }

    public void HideText()
    {
        Info.SetActive(false);
    }
}