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
        // Load saved volume (if any)
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = volume;
        ApplyVolume(volume);
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
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
