using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI phaseText;
    
    [SerializeField]
    private TextMeshProUGUI gameTimerText;
    
    [SerializeField]
    private TextMeshProUGUI phaseTimerText;

    [SerializeField]
    private TextMeshProUGUI phaseCounter;
    
    private void OnEnable()
    {
        GameManager.OnPhaseChanged += UpdatePhaseText;
        GameManager.OnTimerUpdated += UpdateTimerTexts;
    }

    private void OnDisable()
    {
        GameManager.OnPhaseChanged -= UpdatePhaseText;
        GameManager.OnTimerUpdated -= UpdateTimerTexts;
    }

    private void UpdateTimerTexts()
    {
        gameTimerText.text = GameData.GameTimer.ToString("0");
        phaseTimerText.text = GameData.TimeSpentInCurrentPhase.ToString("0");
    }

    private void UpdatePhaseText()
    {
        phaseText.text = GameData.GamePhase.ToString();
        phaseCounter.text = GameData.RegularPhasesCounter.ToString();
    }
}
