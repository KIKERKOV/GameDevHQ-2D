using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    // handle to text
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score" + playerScore.ToString();
    }
}