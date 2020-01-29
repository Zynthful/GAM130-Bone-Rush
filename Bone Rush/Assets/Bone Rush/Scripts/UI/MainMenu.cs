using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void PlayGame()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // Loads the next scene in the build order.
        // Ensure that the game scene is immediately after the MainMenu scene in the build order.
        SceneManager.LoadScene("SCN_Level_1");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("SCN_Menu_Title");
    }

    


    public void QuitGame()
    {
        Debug.Log("Game was quit.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
