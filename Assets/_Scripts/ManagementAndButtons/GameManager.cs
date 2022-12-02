using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;



public class GameManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject startPrompt;
    public GameObject pauseMenu;

    public GameObject progressBar;
    public GameObject gameButtons;
    
    public int gemCount = 0;
    public TMP_Text gemCountText;
    public GameObject gemCounter;
    public GameObject mutedButton;

    void Start()
    {
        AudioListener.volume = 0.5f;
        Time.timeScale = 1;
    }

    void Update()
    {
        gemCountText.text = (gemCount.ToString());

        if (startPrompt.activeInHierarchy == false)
        { return;}

        if (Input.GetMouseButtonDown(0))
        {
            startPrompt.SetActive(false);
            gemCounter.SetActive(true);
            progressBar.SetActive(true);
            gameButtons.SetActive(true);
        }
    }

    public void StartGame()
    {   Destroy(mainMenu);
        startPrompt.SetActive(true); }
    
    public void RestartGame()
    {
        StartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true); 
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    bool audioEnabled = true;


    public void MutedButton(bool i)
    {
        mutedButton.SetActive(i);
    }

    public void ManageAudio()
    {
        if (audioEnabled)
        {
            AudioListener.volume = 0;
            audioEnabled = false;
            MutedButton(true);
        }
        else if (!audioEnabled)
        {
            AudioListener.volume = 0.5f;
            audioEnabled = true;
            MutedButton(false);
        }
    }

}
