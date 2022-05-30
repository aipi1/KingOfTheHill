using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Manages behaviour of all Return buttons
/// </summary>
public class ReturnButton : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainMenuUI;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ReturnPressed);
    }
    // Basically this whole script was created so every Return button could work
    // by passing it's current screen as a parameter so it could be closed in
    // a single method OnReturnButtonPressed

    private void ReturnPressed()
    {
        mainMenuUI.OnReturnPressed(transform.parent.gameObject);
    }
}
