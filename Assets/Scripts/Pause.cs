/*
 * Author: Lim Wee Han
 * Date: 29/06/2024
 * Description: Pause Menu
 */

using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// The GameManager instance used to handle game state changes.
    /// </summary>
    [SerializeField] private GameManager gameManager;

    /// <summary>
    /// The player input handler for detecting pause input.
    /// </summary>
    [SerializeField] private StarterAssetsInputs inputs;

    /// <summary>
    /// The GameObject representing the pause menu UI.
    /// </summary>
    [SerializeField] private GameObject pausemenu;

    /// <summary>
    /// The GameObject representing the main game canvas.
    /// </summary>
    [SerializeField] private GameObject canvas;

    /// <summary>
    /// The GameObject representing the death menu UI.
    /// </summary>
    [SerializeField] private GameObject deathmenu;

    /// <summary>
    /// The GameObject representing the win menu UI.
    /// </summary>
    [SerializeField] private GameObject winmenu;

    /// <summary>
    /// A boolean indicating if the game is currently paused.
    /// </summary>
    public bool IsPaused = false;

    // Update is called once per frame
    /// <summary>
    /// Checks for pause input and toggles between pause and resume.
    /// </summary>
    private void Update()
    {
        if (inputs.pause)
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Resumes the game, hides the pause menu, and restores game time and cursor lock state.
    /// </summary>
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canvas.SetActive(true);
        Time.timeScale = 1f;
        pausemenu.SetActive(false);
        deathmenu.SetActive(false);
        winmenu.SetActive(false);

        IsPaused = false;
        inputs.pause = false;
    }

    /// <summary>
    /// Pauses the game, shows the pause menu, and adjusts game time and cursor lock state.
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        canvas.SetActive(false);
        pausemenu.SetActive(true);

        IsPaused = true;
        inputs.pause = false;
    }

    /// <summary>
    /// Restarts the current scene, resets the game state, and resumes the game.
    /// </summary>
    public void Restart()
    {
        Resume();
        Cursor.lockState = CursorLockMode.Locked;

        gameManager.newscene = true;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        gameManager.health = gameManager.maxHealth;
    }
}
