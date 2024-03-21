using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce;
    bool isDead = false;
    int score = 0;
    int bestScore = 0;

    public AudioClip gameOverSound;
    private AudioSource audioSource;

    public Text scoreText;
    public Text bestScoreText;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        LoadBestScore();
        UpdateUI();
    }

    void Update()
    {
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector3.up * jumpForce;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.CompareTag("ColumnTrigger"))
        {
            IncrementScore();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Pipe"))
        {
            isDead = true;
            StartCoroutine(DieAndRestartAfterDelay());
        }
    }

    IEnumerator DieAndRestartAfterDelay()
    {
        audioSource.PlayOneShot(gameOverSound);

        yield return new WaitForSeconds(2f);

        SaveBestScore();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void IncrementScore()
    {
        score++;
        UpdateUI();
    }

    void UpdateUI()
    {
        UpdateScoreText();
        UpdateBestScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = " " + score;
        }
    }

    void UpdateBestScoreText()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best Score: " + bestScore;
        }
    }

    void SaveBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
    }

    void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }
}
