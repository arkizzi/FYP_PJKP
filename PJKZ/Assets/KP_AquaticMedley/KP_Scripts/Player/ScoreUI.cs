using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    public NewPlayerScoreTest playerScore;

    void Update()
    {
        if (playerScore != null && scoreText != null)
        {
            scoreText.text = playerScore.score.ToString();
        }
    }
}
