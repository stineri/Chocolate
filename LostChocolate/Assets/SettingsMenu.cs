using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioManager.Instance.GetVolume();
            volumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetVolume);
        }
    }
}
