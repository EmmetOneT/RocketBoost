using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("looks good");
                break;
            case "Finish":
                Debug.Log("Finished");
                break;
            case "Fuel":
                Debug.Log("Refuelled");
             break;
                default:
                ReloadLevel();
                break;
        }

        void ReloadLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }
}
