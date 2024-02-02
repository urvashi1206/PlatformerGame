using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    //audio source for the start button
    AudioSource _audioPlayer;
    private bool startReady = false;
    [SerializeField]
    GameObject startButton; 
    // Start is called before the first frame update
    void Start()
    {
        _audioPlayer = startButton.GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        //check that the button has been pressed AND that the audioSource isn't running.
        if(!_audioPlayer.isPlaying && startReady)
        {
            SceneManager.LoadScene("TestScene");
        }
     
    }

    public void StartGame()
    {
        _audioPlayer.Play();
        //prevents player from spamming the button and preventing the game from starting (since the audio won't stop)
        startButton.GetComponent<Button>().onClick.RemoveAllListeners();
        //allows sound to fully play before switching scenes.
        startReady = true; 

    }
}
