using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instant;

    [SerializeField] private Canvas menuUI;
    [SerializeField] private Canvas pauseUI;
    [SerializeField] private Canvas settingsUI;
    [SerializeField] private Canvas gameOverUI;
    [SerializeField] private Canvas inGameUI;

    public Toggle soundToggle;
    public Toggle whiteToggle;

    public TextMeshProUGUI starLabel;
    public TextMeshProUGUI scoreLabel;

    public Color32 whiteMod;
    public Color32 darkMode;

    private void Start()
    {
        menuUI.enabled = true;
        PlayerController.enabled = false;
        Time.timeScale = 0;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void PauseGameButton(bool active)
    {
        Time.timeScale = active ? 0 : 1;
    }
    public void SetPauseUIButton(bool active)
    {
        pauseUI.enabled = active;
    }
    public void SetInGameUIButton(bool active)
    {
        inGameUI.enabled = active;
    }
    public void SetMenuUIButton(bool active)
    {
        menuUI.enabled = active;
        PlayerController.enabled = !active;
    }
    public void SetGameOverUIButton(bool active)
    {
        gameOverUI.enabled = active;
    }
    public void SetSettingsUIButton(bool active)
    {
        settingsUI.enabled = active;
    }
    public void SetColorMode()
    {
        Camera.main.backgroundColor = whiteToggle.isOn ? darkMode : whiteMod;
    }
    public void SetSoundMode()
    {
        AudioListener.pause = soundToggle.isOn;
    }
}
