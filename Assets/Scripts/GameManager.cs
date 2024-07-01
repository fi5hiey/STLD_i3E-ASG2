/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: GameManager
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

/// <summary>
/// Manages the game's state, including player health, scene changes, and UI updates.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the GameManager.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Current health of the player.
    /// </summary>
    public float health;

    /// <summary>
    /// Maximum health of the player.
    /// </summary>
    public float maxHealth;

    /// <summary>
    /// Number of metals obtained by the player.
    /// </summary>
    public int metalsObtained = 0;

    /// <summary>
    /// Flag indicating if a new scene is being loaded.
    /// </summary>
    public bool newscene = false;

    [SerializeField] private GameObject deathmenu;
    [SerializeField] private GameObject winmenu;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image playerHealthbar;
    [SerializeField] private GameObject player;

    /// <summary>
    /// The coordinates where the player will spawn.
    /// </summary>
    public Vector3 spawnCoords = Vector3.zero;

    /// <summary>
    /// Initializes the singleton instance of the GameManager.
    /// Ensures that only one instance of the GameManager exists.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Updates player position and health when a new scene is loaded.
    /// Updates the UI elements for health.
    /// </summary>
    private void FixedUpdate()
    {
        if (newscene)
        {
            player.transform.position = spawnCoords;
            player.transform.rotation = Quaternion.Euler(Vector3.zero);
            health = maxHealth;
            newscene = false;
        }
        healthText.SetText("Health: " + health.ToString() + "/" + maxHealth.ToString());
        playerHealthbar.fillAmount = health / maxHealth;
    }

    /// <summary>
    /// Changes the scene to the specified scene index.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to load.</param>
    public void ChangeScene(int sceneIndex)
    {
        newscene = true;
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Reduces the player's health by the specified amount of damage.
    /// Calls Die() if health drops to zero or below.
    /// </summary>
    /// <param name="damageTaken">The amount of damage taken.</param>
    public void PlayerDamage(int damageTaken)
    {
        health -= damageTaken;

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles player death by stopping the game and displaying the death menu.
    /// </summary>
    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        canvas.SetActive(false);
        deathmenu.SetActive(true);
    }

    /// <summary>
    /// Handles player win by stopping the game and displaying the win menu.
    /// </summary>
    public void Win()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        canvas.SetActive(false);
        winmenu.SetActive(true);
    }

    /// <summary>
    /// Heals the player by the specified amount.
    /// Ensures that health does not exceed the maximum health.
    /// </summary>
    /// <param name="healAmt">The amount of health to restore.</param>
    public void Heal(int healAmt)
    {
        health += healAmt;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
