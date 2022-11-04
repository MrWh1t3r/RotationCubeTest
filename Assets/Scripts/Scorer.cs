using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scorer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private TextMeshProUGUI victoryScoreText;
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject scoreChangePrefab;
    [SerializeField] private Transform scoreChangeParent;
    [SerializeField] private Animator animatorNextButton;
    [SerializeField] private Animator animatorVictoryScoreText;
    [SerializeField] private Animator animatorVictoryText;
    
    private int _multiplier = 1;
    private float _score = 0;

    public static Scorer Instance;

    private void Start()
    {
        UpdateScore();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseScore(float amount)
    {
        _score += amount * _multiplier;
        UpdateScore();
        ScoreChange(amount * _multiplier);
    }

    public void IncreaseScoreWithoutMultiplier(float amount)
    {
        _score += amount;
        UpdateScore();
        ScoreChange(amount);
    }

    public void DecreaseScore(float amount)
    {
        _score -= amount;
        UpdateScore();
        ScoreChange(-amount);
    }

    private void UpdateScore()
    {
        scoreText.text = "" + (int)_score;
    }

    public void IncreaseMultiplier()
    {
        _multiplier++;
        multiplierText.enabled = true;
        multiplierText.text = "x" + _multiplier;
    }

    public void ResetMultiplier()
    {
        multiplierText.enabled = false;
        _multiplier = 1;
    }

    private void ScoreChange(float amount)
    {
        var obj = Instantiate(scoreChangePrefab, scoreChangeParent.position, Quaternion.identity);
        obj.transform.SetParent(scoreChangeParent, true);
        obj.GetComponent<TextMeshProUGUI>().text = amount > 0 ? "+" + (int)amount : "" + (int)amount;
        obj.GetComponent<TextMeshProUGUI>().color = amount > 0 ? Color.green : Color.red;
    }

    public void Finish()
    {
        scoreText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        multiplierText.gameObject.SetActive(false);
        Invoke("ShowVictoryText", 1f);
        Invoke("ShowVictoryScore", 1.5f);
        Invoke("ShowNextButton", 2f);
    }

    private void ShowVictoryText()
    {
        victoryText.gameObject.SetActive(true);
        animatorVictoryText.SetTrigger("Finish");
    }

    private void ShowVictoryScore()
    {
        victoryScoreText.gameObject.SetActive(true);
        animatorVictoryScoreText.SetTrigger("Finish");
    }

    private void ShowNextButton()
    {
        nextButton.gameObject.SetActive(true);
        animatorNextButton.SetTrigger("Finish");
    }

    public void NextLevel()
    {
        // SceneManager.LoadScene(NextScene);
    }
}
