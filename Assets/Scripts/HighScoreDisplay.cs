using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    void Start()
    {
        textDisplay.text = $"Highscore: {GameManager.instance.Highscore}";
    }

}
