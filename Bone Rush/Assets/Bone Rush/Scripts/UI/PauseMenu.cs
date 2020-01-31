using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // This allows it to be accessed by any other class.
    #region Singleton
    public static PauseMenu Instance;
    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
    }
    #endregion

    // SerializeField allows you to see a private variable in the inspector.
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject healthBar;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseGame();
    }

    public void PauseGame()
    {


        // Inverts function.
        isPaused = !isPaused;

        // If the game is paused, the timeScale is set to 0 so it actually pauses the game.
        if (isPaused)
        {
            Time.timeScale = 0f;
            // Unlocks cursor
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Activates pauseMenu GO.
        pauseMenu.SetActive(isPaused);
        healthBar.SetActive(!isPaused);
        // Cursor is visible whenever game is paused.
        Cursor.visible = isPaused;



    }
}
