using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class uiManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text diamondText;
    public TMP_Text GameOverScoreText;
    public TMP_Text GameOverDiamondsText;
    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateDiamonds(int diamonds)
    {
        diamondText.text = ": " + diamonds.ToString();
    }

    public void UpdateGameOverScore(int score, int diamonds)
    {
        GameOverScoreText.text = score.ToString();
        GameOverDiamondsText.text = diamonds.ToString();
    }
}
