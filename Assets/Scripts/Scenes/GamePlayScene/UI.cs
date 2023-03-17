using System;
using NTC.Global.Cache;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoCache
{
    [SerializeField]private TextMeshProUGUI timeText;
    public GameObject winPanel;
    public GameObject losePanel;
    [SerializeField] private Image inkMeter;
    private float maxInk;
    private void Start()
    {
        GameSceneManager.instanse.onGameEnd += End;
        maxInk = InkManager.inkAmount;
    }

    protected override void Run()
    {
        timeText.text = GameSceneManager.instanse.remainingTime.ToString();
        inkMeter.fillAmount = InkManager.inkAmount / maxInk;
    }

    private void End(bool a)
    {
        if(a)
            winPanel.SetActive(true);
        else
            losePanel.SetActive(true);
    }
}
