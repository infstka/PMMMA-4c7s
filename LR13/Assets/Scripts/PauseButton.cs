using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public AudioSource audioSource; 
    public AudioClip clickSound;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        audioSource.PlayOneShot(clickSound);    
    }

    public void Resume()
    {
        StartCoroutine(ResumeAfterSound());        
    }
    
    IEnumerator ResumeAfterSound()
    {
        audioSource.PlayOneShot(clickSound);
        yield return new WaitForSecondsRealtime(clickSound.length);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    
    public void Exit()
    {
        audioSource.PlayOneShot(clickSound);    
        Application.Quit();
    }
}
