using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// Handles UI in Game Scene
/// </summary>
public class GameUI : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject endgameScreen;
    [SerializeField] private TextMeshProUGUI currentWave;
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI bestScore;
    private AudioSource gameUIAudio;
    private bool isCoroutineRunning = false;

    void Awake()
    {
        gameUIAudio = GetComponent<AudioSource>();
    }

    private void PlayButtonSound()
    {
        gameUIAudio.PlayOneShot(buttonSound, 1.0f);
    }

    // Buttons from Game Scene
    public void OnEscapePressed()
    {
        if (!isGamePaused)
        {
            gameUIAudio.PlayOneShot(pauseSound, 1.0f);
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }

        isGamePaused = !isGamePaused;
    }

    public void OnContinuePressed()
    {
        PlayButtonSound();
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void QuitToMenuPressed()
    {
        if (!isCoroutineRunning)
        {
            // Make sure the whole sound is played before proceeding
            StartCoroutine(QuitAfterSound());
        }
    }

    private IEnumerator QuitAfterSound()
    {
        isCoroutineRunning = true;
        PlayButtonSound();
        yield return new WaitWhile(() => gameUIAudio.isPlaying);

        Time.timeScale = 1;
        isGamePaused = false;
        if (PlayerStatsHandler.Instance)
        {
            PlayerStatsHandler.Instance.playerName = "";
        }
        isCoroutineRunning = false;
        SceneManager.LoadScene(0);
    }

    public void SetCurrentWave()
    {
        currentWave.text = $"Current wave: {SpawnManager.WaveNumber}";
    }

    public void ShowEndgameScreen()
    {
        var name = PlayerStatsHandler.Instance.playerName;
        var waves = SpawnManager.WaveNumber - 1;
        string lastWord;

        if (waves == 1)
        {
            lastWord = "wave";
        }
        else
        {
            lastWord = "waves";
        }
        Time.timeScale = 0;
        isGamePaused = true;
        currentWave.gameObject.SetActive(false);
        endgameScreen.SetActive(true);
        finalScore.text = $"{name}, you survived {waves} {lastWord}!";
    }

    public void UpdateBestScore()
    {
        var name = PlayerStatsHandler.Instance.BestName;
        var waves = PlayerStatsHandler.Instance.BestScore;
        string lastWord;

        if (waves == 1)
        {
            lastWord = "wave";
        }
        else
        {
            lastWord = "waves";
        }
        bestScore.text = $"Best result by {name}: {waves} {lastWord}!";
    }

    public void TryAgainPressed()
    {
        if (!isCoroutineRunning)
        {
            // Make sure the whole sound is played before proceeding
            StartCoroutine(TryAgainAfterSound());
        }
    }

    private IEnumerator TryAgainAfterSound()
    {
        isCoroutineRunning = true;
        PlayButtonSound();
        yield return new WaitWhile(() => gameUIAudio.isPlaying);

        Time.timeScale = 1;
        isGamePaused = false;
        isCoroutineRunning = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
