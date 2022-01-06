using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score;
    public Text ScoreDisplay;

    private void Update()
    {
        ScoreDisplay.text = "Score: " + Score.ToString();

    }
    public void Plus()
    {
        Score++;
    }
}
