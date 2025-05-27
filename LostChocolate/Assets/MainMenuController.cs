using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public AudioSource musicSource; // optional, can use AudioListener.volume

    private void Start()
    {
        // Debug null checks
        if (settingsPanel == null)
            Debug.LogError("Settings Panel is not assigned!");
        if (volumeSlider == null)
            Debug.LogError("Volume Slider is not assigned!");
        if (musicSource == null)
            Debug.LogWarning("Music Source is not assigned! Audio will only use AudioListener.volume.");

        // Load saved volume (if any)
        float volume = PlayerPrefs.GetFloat("Volume", 1f);

        // Protect against null volumeSlider
        if (volumeSlider != null)
            volumeSlider.value = volume;

        ApplyVolume(volume);
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void OnVolumeChange(float value)
    {
        ApplyVolume(value);
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

    private void ApplyVolume(float value)
    {
        AudioListener.volume = value;

        if (musicSource != null)
            musicSource.volume = value;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
