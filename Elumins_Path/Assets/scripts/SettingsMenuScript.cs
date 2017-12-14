using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuScript : MonoBehaviour {

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        if(audioMixer != null)
            audioMixer.SetFloat("volume", Mathf.Lerp(-30, 20, volume));
        if(volume == 0)
        {
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.pause = false;
        }
    }

    private void SetSFX(float volume)
    {
        // Does nothing currently
    }

    private void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
