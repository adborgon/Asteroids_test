using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHandler : MonoBehaviour
{
    public static InfoHandler infoHandler { get; private set; }

    [SerializeField] private TMPro.TMP_Text title, subtitle;
    [SerializeField] private GameObject info, instructions;

    private void Awake()
    {
        infoHandler = this;
    }

    public void ShowIntro()
    {
        title.text = GameConfiguration.titleCanvas;
        subtitle.text = GameConfiguration.subtitleCanvas;
        info.SetActive(true);
        instructions.SetActive(true);
    }

    public void ShowFinish(int score)
    {
        title.text = GameConfiguration.finishCanvas;
        subtitle.text = GameConfiguration.subFinishCanvas + score + GameConfiguration.subFinishCanvasEnd;
        info.SetActive(true);
    }

    public void HideText()
    {
        info.SetActive(false);
        instructions.SetActive(false);
    }
}