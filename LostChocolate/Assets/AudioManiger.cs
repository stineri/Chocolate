using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer audioMixer; // Optional if using Audio Mixer
    private float currentVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load volume from saved data
            currentVolume = PlayerPrefs.GetFloat("Volume", 1f);
            SetVolume(currentVolume);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        AudioListener.volume = volume; // Applies globally
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return currentVolume;
    }
}
