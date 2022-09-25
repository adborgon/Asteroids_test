using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHandler : MonoBehaviour
{
    public static GUIHandler guiHandler { get; private set; }

    public TMPro.TMP_Text txt_health, txt_score, txt_energy;
    public GameObject GUI;

    private void Awake()
    {
        guiHandler = this;
    }

    public void UpdateLife(int health)
    {
        txt_health.text = health.ToString();
    }

    public void UpdateScore(int score)
    {
        //Podríamos hacer una animación
        txt_score.text = score.ToString();
    }

    public void UpdateEnergy(int energy)
    {
        //Podríamos hacer una animación
        txt_energy.text = energy.ToString();
    }

    public void HideGUI()
    {
        GUI.SetActive(false);
    }

    public void ShowGUI()
    {
        txt_health.text = "3";
        GUI.SetActive(true);
    }
}