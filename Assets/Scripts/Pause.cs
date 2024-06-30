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
    [SerializeField] private GameManager gameManager;
    [SerializeField] private StarterAssetsInputs inputs;
    [SerializeField] private GameObject pausemenu;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject deathmenu;
    [SerializeField] private GameObject winmenu;

    public bool IsPaused = false;

    // Update is called once per frames
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

    public void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        canvas.SetActive(false);
        pausemenu.SetActive(true);
        
        IsPaused = true;
        inputs.pause = false;
    }

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
