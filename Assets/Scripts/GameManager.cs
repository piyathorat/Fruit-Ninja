using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public bool isGameActive;

    private int score;
    private Blade blade;
    private Spawnwer spawner;
    public Image FadeImage;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawnwer>();
    }
    private void Start()
    {
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        NewGame();
        Time.timeScale = 1f;

    }
    private void NewGame()

    {
        score = 0;
        scoreText.text = score.ToString();

        Time.timeScale = 1f;

        ClearScene();

    }
    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }
        Bombs[] bomb = FindObjectsOfType<Bombs>();

        foreach (Bombs bombs in bomb)
        {
            Destroy(bombs.gameObject);
        }
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
    public void Explode()
    {
        
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }
    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            FadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        NewGame();
        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            FadeImage.color = Color.Lerp(Color.white, Color.clear, t);


            elapsed += Time.unscaledDeltaTime;
            GameOver();

            yield return null;
        }
    

    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
