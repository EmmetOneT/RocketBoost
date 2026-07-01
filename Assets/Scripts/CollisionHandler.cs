using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("looks good");
                break;

            //Load next level on landing pad
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("Refuelled");
                break;

            //Reload level on collision
            default:
                StartCrashSequence();
               
                break;
        }
    }

    private void StartSuccessSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    //Reload scene method
    void ReloadLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }

        //Load next scene method
        void LoadNextLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = currentScene + 1;
            if (nextScene == SceneManager.sceneCountInBuildSettings)
            {
                nextScene = 0;
            }
            SceneManager.LoadScene(nextScene);
        }
}

