using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void GoToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToGame()
    {
        SceneManager.LoadScene("FinalLevelDraft");
    }


}
