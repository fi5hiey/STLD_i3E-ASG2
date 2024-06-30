
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

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float health;
    public float maxHealth;
    public int metalsObtained = 0;

    public bool newscene = false;

    [SerializeField] private GameObject deathmenu;
    [SerializeField] private GameObject winmenu;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image playerHealthbar;
    [SerializeField] private GameObject player;

    public Vector3 spawnCoords = Vector3.zero;

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

    public void ChangeScene(int sceneIndex)
    {
        newscene = true;
        SceneManager.LoadScene(sceneIndex);
    }

    public void PlayerDamage(int damageTaken)
    {
        health -= damageTaken;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        canvas.SetActive(false);
        deathmenu.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        canvas.SetActive(false);
        winmenu.SetActive(true);
    }

    public void Heal(int healAmt)
    {
        health += healAmt;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
