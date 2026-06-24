using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;
    public Slider healthBar;

    [Header("Game Over")]
    public Image blackScreen;
    public TextMeshProUGUI unconqueredText;
    public TextMeshProUGUI thinkAgainText;

    private PlayerHealth playerHealth;
    private int score = 0;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        
        // Sab hide karo shuru mein
        blackScreen.gameObject.SetActive(false);
        unconqueredText.gameObject.SetActive(false);
        thinkAgainText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerHealth != null)
            healthBar.value = (float)playerHealth.currentHealth / playerHealth.maxHealth;
    }

    public void UpdateWave(int waveNumber)
    {
        waveText.text = "Wave: " + waveNumber;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void ShowGameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        // Step 1 - Black screen
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 0);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * 2f;
            blackScreen.color = new Color(0, 0, 0, Mathf.Clamp01(t));
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        // Step 2 - "UNCONQUERED?" appear karo
        unconqueredText.gameObject.SetActive(true);
        unconqueredText.color = new Color(1, 0, 0, 0);

        t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * 2f;
            unconqueredText.color = new Color(1, 0, 0, Mathf.Clamp01(t));
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        // Step 3 - "THINK AGAIN!" appear karo
        thinkAgainText.gameObject.SetActive(true);
        thinkAgainText.color = new Color(1, 0, 0, 0);

        t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * 2f;
            thinkAgainText.color = new Color(1, 0, 0, Mathf.Clamp01(t));
            yield return null;
        }
    }
}