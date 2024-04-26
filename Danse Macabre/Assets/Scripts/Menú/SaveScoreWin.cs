using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScoreWin : MonoBehaviour
{
    #region references
    private ScoreManager _scoreManager;
    private SliderController _sliderController;
    #endregion
    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MovementComponent _player = collision.GetComponent<MovementComponent>();
        if (_player != null)
        {
            _scoreManager.SaveFinalScore();
            _sliderController.SaveProgess();
            MaxScoreCalculator.Instance.SaveSceneMaxScore();
            //Cambiar escena de Victoria
            GameManager.Instance.ResetCheckpoint();
            PlayerPrefs.SetInt("PreviousScene", SceneManager.GetActiveScene().buildIndex); // For restarting the same level after victory
            SceneManager.LoadScene("Victoria");
        }
    }
    #endregion
    void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
        _sliderController = FindObjectOfType<SliderController>();
    }
}
