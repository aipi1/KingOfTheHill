using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// Handles UI in MainMenu Scene
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject inputScreen;
    [SerializeField] private GameObject howToScreen;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private AudioClip buttonSound;
    private int maxNameLength = 10;
    private AudioSource menuUIAudio;
    private bool isCoroutineRunning = false;

    void Awake()
    {
        inputField.characterLimit = maxNameLength;
        menuUIAudio = GetComponent<AudioSource>();
    }

    private void PlayButtonSound()
    {
        menuUIAudio.PlayOneShot(buttonSound, 1.0f);
    }

    // Title Screen Buttons
    public void OnPlayPressed()
    {
        PlayButtonSound();
        titleScreen.SetActive(false);
        inputScreen.SetActive(true);
    }

    public void OnHowToPressed()
    {
        PlayButtonSound();
        titleScreen.SetActive(false);
        howToScreen.SetActive(true);
    }

    public void OnQuitPressed()
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
        yield return new WaitWhile(() => menuUIAudio.isPlaying);

        isCoroutineRunning = false;
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Start Button
    public void OnStartPressed()
    {
        if (!isCoroutineRunning)
        {
            // Make sure the whole sound is played before proceeding
            StartCoroutine(StartAfterSound());
        }
    }

    private IEnumerator StartAfterSound()
    {
        isCoroutineRunning = true;
        PlayButtonSound();
        yield return new WaitWhile(() => menuUIAudio.isPlaying);

        if (inputField.text.Replace(" ", "") != "")
        {
            PlayerStatsHandler.Instance.playerName = inputField.text.Replace(" ", "");
        }
        else
        {
            PlayerStatsHandler.Instance.playerName = "Player";
        }
        isCoroutineRunning = false;
        SceneManager.LoadScene(1);
    }

    // Return Button (this method is called from ReturnButton.cs in order to
    // have one OnReturnPressed method for different return buttons
    public void OnReturnPressed(GameObject currentScreen)
    {
        PlayButtonSound();
        currentScreen.SetActive(false);
        titleScreen.SetActive(true);
    }
}
