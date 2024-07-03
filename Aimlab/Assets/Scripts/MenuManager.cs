using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public GameObject menuPanel;
    public bool menuEnabled = false;

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
    void Update()
    {
        if (GameManager.instance.isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && menuEnabled == false)
            {
                menuPanel.SetActive(true);
                menuEnabled = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && menuEnabled == true)
            {
                Continue();
            }
        }
    }
    public void Exitbtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void Continue()
    {
        menuPanel.SetActive(false);
        menuEnabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
