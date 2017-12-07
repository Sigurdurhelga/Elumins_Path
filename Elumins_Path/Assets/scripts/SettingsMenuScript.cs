using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuScript : MonoBehaviour {

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        if(audioMixer != null)
            audioMixer.SetFloat("volume", volume*20+(1 - volume)*-80);
    }

    public void SetSFX(float volume)
    {
        // Does nothing currently
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
