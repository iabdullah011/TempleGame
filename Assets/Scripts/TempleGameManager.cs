
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TempleGameManager : MonoBehaviour
{
    public static TempleGameManager Instance;

    public TMP_Text scoreText;
    public TMP_Text healthText;
    public Slider healthBar;
    public GameObject gameOverPanel;

    public int score = 0;
    public int health = 100;

    private bool gameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (healthBar != null)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = 100;
        }

        UpdateUI();
    }

    public void AddScore(int points)
    {
        if (gameOver)
            return;

        score += points;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        if (gameOver)
            return;

        health -= damage;
        health = Mathf.Clamp(health, 0, 100);

        UpdateUI();

        if (health <= 0)
        {
            gameOver = true;
            if (healthBar != null)
                healthBar.gameObject.SetActive(false);

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0f;

            Debug.Log("Game Over!");
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (healthText != null)
            healthText.text = "Health: " + health + " / 100";

        if (healthBar != null)
            healthBar.value = health;
    }
}
