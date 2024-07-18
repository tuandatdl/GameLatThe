using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public Slider volumeSlider;
    public AudioSource audioSource;
    public Image soundIcon;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;

    private const string VolumePrefKey = "Volume";

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("audioSource has not been assigned in AudioManager.");
            return;
        }

        // Load volume from PlayerPrefs
        if (PlayerPrefs.HasKey(VolumePrefKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey);
            audioSource.volume = savedVolume;
            volumeSlider.value = savedVolume;
        }
        else
        {
            // If no saved volume, set to max volume
            audioSource.volume = 1f;
            volumeSlider.value = 1f;
        }

        // Update sound icon based on the current volume
        UpdateSoundIcon();
    }

    public void SetVolume(float volume)
    {
        if (audioSource == null)
        {
            Debug.LogError("audioSource has not been assigned in AudioManager.");
            return;
        }

        audioSource.volume = volume;
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();

        // Update sound icon based on the current volume
        UpdateSoundIcon();
    }

    private void UpdateSoundIcon()
    {
        if (audioSource.volume == 0)
        {
            soundIcon.sprite = soundOffIcon;
        }
        else
        {
            soundIcon.sprite = soundOnIcon;
        }
    }

    public void ToggleSound()
    {
        if (audioSource.volume > 0)
        {
            SetVolume(0);
        }
        else
        {
            SetVolume(1);
        }
    }
}

