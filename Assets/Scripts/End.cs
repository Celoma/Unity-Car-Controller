using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{

    public void ResetGame()
    {
        Time.timeScale = 1f; // Ensure time is reset before loading the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // If you're running in the editor, this will stop play mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Debug.Log("Game is quitting"); // Optional: Log to console for debugging
    }
}
