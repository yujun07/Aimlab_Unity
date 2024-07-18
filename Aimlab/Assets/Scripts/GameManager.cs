using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerCam playerCam;
    public TargetSpawner targetSpawner;

    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI accuracyTxt;
    public TextMeshProUGUI timeTxt;

    public GameObject gameOverPanel;
    public GameObject crossHair;
    public GameObject startPanel;

    public bool isGameOver;
    public bool isGameStarted;
    public float limitTime;
    public float sens;
    public Slider timerSlider;
    public TextMeshProUGUI timerValTxt;
    public Slider sensSlider;
    public TextMeshProUGUI sensValTxt;

    int score = 0;
    float accuracy = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreTxt.text = score.ToString();
        }
    }

    public float Accuracy
    {
        get
        {
            return accuracy;
        }
        set
        {
            accuracy = value;
            accuracyTxt.text = accuracy.ToString("F2") + "%";
        }
    }

    public void SetTimer()
    {
        limitTime = timerSlider.value;
        timerValTxt.text = "Timer: " + timerSlider.value.ToString();
    }

    public void SetSens()
    {
        sens = sensSlider.value;
        sensValTxt.text = "Sens: " + sensSlider.value.ToString("F2");
        playerCam.sensX = sens * 100;
        playerCam.sensY = sens * 100;
    }

    public void StartBtn()
    {
        isGameStarted = true;
        startPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        targetSpawner.StartSpawning();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        score = 0;
        accuracy = 0;
        isGameOver = false;
        isGameStarted = false;
        gameOverPanel.SetActive(false);
        crossHair.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCam = GameObject.Find("Main Camera").gameObject.GetComponent<PlayerCam>();
        targetSpawner = FindObjectOfType<TargetSpawner>();
    }

    private void Update()
    {
        if (isGameOver)
        {
            gameOverPanel.SetActive(true);
            crossHair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isGameStarted = false;
        }

        if (!isGameOver && isGameStarted)
        {
            limitTime -= Time.deltaTime;
            timeTxt.text = limitTime.ToString("F1");

            if (limitTime <= 0)
            {
                isGameOver = true;
            }
        }
    }
}
