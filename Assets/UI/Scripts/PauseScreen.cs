using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public PlayerHUD playerHUD;
    public GameObject controlsScreen;

    public void ResumeGame()
    {
        playerHUD.TogglePauseGame();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void SeeControls()
    {
        controlsScreen.SetActive(true);
    }

    public void BacktoPauseMenu()
    {
        controlsScreen.SetActive(false);
    }

    //make sure control screen is disabled as well when the game is resumed
    private void OnDisable()
    {
        controlsScreen.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

   
}


