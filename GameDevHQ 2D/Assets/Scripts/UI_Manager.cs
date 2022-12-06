using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartLevelText;
    [SerializeField] private Image _LivesImage;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private GameManager _gameManager;
    // handle to text
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);

        if (_gameManager == null)
        {
            Debug.LogError("Game_Manger is NULL");
        }

    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score" + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImage.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }


    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.text = "GAME OVER";
        _restartLevelText.text = "Press the 'R' button ro restart";
        StartCoroutine(Flicker(0.2f));
    }


    private IEnumerator Flicker(float _flickerTime)
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(_flickerTime);
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(_flickerTime);
        }

    }



}