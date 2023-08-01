using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    private int _coins;
    private int _completedLevels = 1;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private GameObject MainWindow;
    [SerializeField] private GameObject WinWindow;
    [SerializeField] private GameObject FailWindow;

    private void Start()
    {
        SetLevelTitle();
        SetCoinsCount();
        print("coins : " + PlayerPrefs.GetInt("Coins"));
        print("completed levels : " + PlayerPrefs.GetInt("CompletedLevels"));
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
    
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void HelpButton()
    {
        //later
    }
    
    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LastLevelButton()
    {
        SceneManager.LoadScene(_completedLevels);
        
    }
    private void SetLevelTitle()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            
            if (PlayerPrefs.HasKey("CompletedLevels"))
                _completedLevels = PlayerPrefs.GetInt("CompletedLevels");
            else
                PlayerPrefs.SetInt("CompletedLevels", 1);

            int totalLevels = SceneManager.sceneCountInBuildSettings - 1;
            levelText.text = "Level " + _completedLevels + "/" + totalLevels;
        }
        else
        {
            levelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        }
    }

    private void SetCoinsCount()
    {
        if (PlayerPrefs.HasKey("Coins"))
            _coins = PlayerPrefs.GetInt("Coins"); 
        else
            PlayerPrefs.SetInt("Coins", 0);

        coinsText.text = _coins.ToString();
    }
    
    private void OnEnable()
    {
        GameEvents.OnLevelCompleted += LevelCompleted;
        GameEvents.OnLevelFailed += LevelFailed;
        
    }

    private void OnDisable()
    {
        GameEvents.OnLevelCompleted -= LevelCompleted;
        GameEvents.OnLevelFailed -= LevelFailed;        
    }

    

    private void LevelCompleted()
    {
        _completedLevels++;
        _coins += 10;
        
        PlayerPrefs.SetInt("CompletedLevels", PlayerPrefs.GetInt("CompletedLevels") + 1);
        PlayerPrefs.SetInt("Coins", _coins);
        
        coinsText.text = _coins.ToString();
        WinWindow.SetActive(true);

    }

    private void LevelFailed()
    {
        FailWindow.SetActive(true);
        
    }
    
}
